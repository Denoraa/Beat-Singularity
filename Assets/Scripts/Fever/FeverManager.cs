using System.Collections;
using UnityEngine;

public class FeverManager : MonoSingleton<FeverManager>
{
    [SerializeField] private FeverConfigSO defaultConfig;
    [SerializeField] private float fallbackDuration = 8f;
    [SerializeField] private float fallbackScoreMultiplier = 2f;
    [SerializeField] private float fallbackJudgementWindowMultiplier = 1.5f;
    [SerializeField] private Color fallbackFeverBackgroundColor = new Color(0.05f, 0f, 0.12f, 1f);

    private FeverConfigSO currentConfig;
    private Coroutine feverRoutine;
    private Camera mainCamera;
    private Color normalBackgroundColor;
    private bool hasStoredBackgroundColor;

    public bool IsFeverActive { get; private set; }

    public float ScoreMultiplier
    {
        get
        {
            if (!IsFeverActive)
                return 1f;

            return CurrentConfig != null ? CurrentConfig.scoreMultiplier : fallbackScoreMultiplier;
        }
    }

    public float JudgementWindowMultiplier
    {
        get
        {
            if (!IsFeverActive)
                return 1f;

            return CurrentConfig != null ? CurrentConfig.judgementWindowMultiplier : fallbackJudgementWindowMultiplier;
        }
    }

    private FeverConfigSO CurrentConfig
    {
        get { return currentConfig != null ? currentConfig : defaultConfig; }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Subscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Unsubscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
    }

    private void OnLevelStart(GameEvents.LevelStartEvent eventData)
    {
        SetConfig(eventData.levelConfig != null ? eventData.levelConfig.feverConfig : null);
        StopFever(false);
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        StopFever(true);
    }

    public void StartFever()
    {
        StartFever(CurrentConfig != null ? CurrentConfig.duration : fallbackDuration);
    }

    public void SetConfig(FeverConfigSO config)
    {
        currentConfig = config;
    }

    public void StartFever(float duration)
    {
        if (feverRoutine != null)
            StopCoroutine(feverRoutine);

        feverRoutine = StartCoroutine(FeverRoutine(duration));
    }

    private IEnumerator FeverRoutine(float duration)
    {
        IsFeverActive = true;
        ApplyFeverVisual();
        EventBus.Publish(new GameEvents.FeverStartEvent(duration));

        yield return new WaitForSeconds(duration);

        StopFever(true);
    }

    private void StopFever(bool publishEvent)
    {
        if (feverRoutine != null)
        {
            StopCoroutine(feverRoutine);
            feverRoutine = null;
        }

        bool wasActive = IsFeverActive;
        IsFeverActive = false;
        RestoreFeverVisual();

        if (wasActive && publishEvent)
            EventBus.Publish(new GameEvents.FeverEndEvent());
    }

    private void ApplyFeverVisual()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
            return;

        if (!hasStoredBackgroundColor)
        {
            normalBackgroundColor = mainCamera.backgroundColor;
            hasStoredBackgroundColor = true;
        }

        mainCamera.backgroundColor = CurrentConfig != null ? CurrentConfig.feverBackgroundColor : fallbackFeverBackgroundColor;
    }

    private void RestoreFeverVisual()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null && hasStoredBackgroundColor)
            mainCamera.backgroundColor = normalBackgroundColor;
    }
}
