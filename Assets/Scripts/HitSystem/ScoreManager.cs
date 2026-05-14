using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    [Header("Score Config")]
    [SerializeField] ScoreConfigSO scoreConfig;
    public int CurrentScore { get; private set; }
    public int PerfectCount { get; private set; }
    public int GoodCount { get; private set; }
    public int BadCount { get; private set; }
    public int MissCount { get; private set; }

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Subscribe<GameEvents.NoteJudgeEvent>(OnNoteJudge);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Unsubscribe<GameEvents.NoteJudgeEvent>(OnNoteJudge);
    }

    private void OnLevelStart(GameEvents.LevelStartEvent eventData)
    {
        ResetScore();
    }

    private void OnNoteJudge(GameEvents.NoteJudgeEvent eventData)
    {
        int baseScore = 0;

        switch (eventData.hitResult)
        {
            case HitResult.Perfect:
                PerfectCount++;
                baseScore = scoreConfig.perfectScore;
                break;
            case HitResult.Good:
                GoodCount++;
                baseScore = scoreConfig.goodScore;
                break;
            case HitResult.Bad:
                BadCount++;
                baseScore = scoreConfig.badScore;
                break;
            case HitResult.Miss:
                MissCount++;
                baseScore = scoreConfig.missScore;
                break;
        }

        CurrentScore += Mathf.RoundToInt(baseScore * FeverManager.Instance.ScoreMultiplier);
        EventBus.Publish(new GameEvents.ScoreUpdateEvent(CurrentScore));
    }

    private void ResetScore()
    {
        CurrentScore = 0;
        PerfectCount = 0;
        GoodCount = 0;
        BadCount = 0;
        MissCount = 0;
    }
}
