using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoSingleton<HitManager> 
{

    private DifficultyConfigSO difficultyConfig;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(Init);
        EventBus.Subscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Subscribe<GameEvents.TopHitEvent>(OnTopHit);
    }

    private void Init(GameEvents.LevelStartEvent @event)
    {
        this.difficultyConfig = @event.levelConfig.difficultyConfig;
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(Init);
        EventBus.Unsubscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Unsubscribe<GameEvents.TopHitEvent>(OnTopHit);
    }
    private void OnDownHit(GameEvents.DownHitEvent eventData)
    {
        IReadOnlyList<Note> activeNotes = ActiveNoteManager.Instance.GetActiveNotes(LaneType.Down);
        ProcessHit(activeNotes);
    }
    private void OnTopHit(GameEvents.TopHitEvent eventData)
    {
        IReadOnlyList<Note> activeNotes = ActiveNoteManager.Instance.GetActiveNotes(LaneType.Top);
        ProcessHit(activeNotes);
    }

    private void ProcessHit(IReadOnlyList<Note> notes)
    {
        float time = MusicManager.Instance.MusicSource.time;

        foreach (Note note in notes)
        {
            float hitTiming = note.NoteData.hitTime;
            float timingDifference = Mathf.Abs(hitTiming - time);
            if (timingDifference <= difficultyConfig.perfectThreshold)
            {
                Debug.Log("Perfect Hit!");
                return;
            }
            else if (timingDifference <= difficultyConfig.goodThreshold)
            {
                Debug.Log("Good Hit.");
                return;
            }
            else if (timingDifference <= difficultyConfig.badThreshold)
            {
                Debug.Log("Bad");
                return;
            }
            else
            {
                Debug.Log("Miss");
            }
        }

    }

    public void SetDifficultyConfig(DifficultyConfigSO config)
    {
        difficultyConfig = config;
    }   


}


