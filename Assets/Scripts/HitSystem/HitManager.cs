using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoSingleton<HitManager>
{

    private DifficultyConfigSO difficultyConfig;
    private bool isLevelActive;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(Init);
        EventBus.Subscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
        EventBus.Subscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Subscribe<GameEvents.TopHitEvent>(OnTopHit);
    }

    private void Init(GameEvents.LevelStartEvent @event)
    {
        this.difficultyConfig = @event.levelConfig.difficultyConfig;
        isLevelActive = true;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(Init);
        EventBus.Unsubscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
        EventBus.Unsubscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Unsubscribe<GameEvents.TopHitEvent>(OnTopHit);
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        isLevelActive = false;
    }

    private void OnDownHit(GameEvents.DownHitEvent eventData)
    {
        if (!isLevelActive)
            return;

        IReadOnlyList<Note> activeNotes = ActiveNoteManager.Instance.GetActiveNotes(LaneType.Down);
        ProcessHit(activeNotes);
    }
    private void OnTopHit(GameEvents.TopHitEvent eventData)
    {
        if (!isLevelActive)
            return;

        IReadOnlyList<Note> activeNotes = ActiveNoteManager.Instance.GetActiveNotes(LaneType.Top);
        ProcessHit(activeNotes);
    }

    private void ProcessHit(IReadOnlyList<Note> notes)
    {
        float time = MusicManager.Instance.MusicSource.time;
        if (difficultyConfig == null || notes == null || notes.Count == 0)
            return;

        Note candidate = null;
        float candidateAbsDelta = float.MaxValue;

        foreach (Note note in notes)
        {
            if (note == null || note.NoteData == null)
                continue;

            float hitTiming = note.NoteData.hitTime;

            float delta = time - hitTiming;

            // 只允许在 hitTime 前后 badThreshold 的窗口中判定
            if (delta < -difficultyConfig.badThreshold || delta > difficultyConfig.badThreshold)
                continue;

            float absDelta = Mathf.Abs(delta);
            if (absDelta < candidateAbsDelta)
            {
                candidate = note;
                candidateAbsDelta = absDelta;
            }
        }
        // 不在可判定窗口内：本次输入不算击中，不给判定
        if (candidate == null)
        {
            Debug.Log("Empty Hit");
            return;
        }

        if (candidateAbsDelta <= difficultyConfig.perfectThreshold)
        {
            Debug.Log("Perfect Hit!");
            EventBus.Publish(new GameEvents.NoteJudgeEvent(HitResult.Perfect));
        }
        else if (candidateAbsDelta <= difficultyConfig.goodThreshold)
        {
            Debug.Log("Good Hit.");
            EventBus.Publish(new GameEvents.NoteJudgeEvent(HitResult.Good));
        }
        else
        {
            Debug.Log("Bad");
            EventBus.Publish(new GameEvents.NoteJudgeEvent(HitResult.Bad));
        }

        candidate.Consume();
    }

    public void SetDifficultyConfig(DifficultyConfigSO config)
    {
        difficultyConfig = config;
    }


}


