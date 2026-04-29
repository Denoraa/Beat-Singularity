using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelFlowManager : MonoSingleton<LevelFlowManager>
{
    private AudioClip currentSong = null;
    private float SongDuration = 0;
    private GameObject gameOverPanel;
    private TextMeshProUGUI gameOverText;
    private bool levelEnded;

    //Test File
    [SerializeField] private LevelConfigSO testLevelConfig;
    [SerializeField] private string gameOverMessage = "Game Over";

    private void Start()
    {
        CreateGameOverUI();
        HideGameOverUI();
        StartCoroutine(StartLevelFlow(testLevelConfig));
    }

    public IEnumerator StartLevelFlow(LevelConfigSO levelConfig)
    {
        if (levelConfig == null)
        {
            Debug.LogError("LevelConfig is missing.");
            yield break;
        }

        Time.timeScale = 1f;
        levelEnded = false;
        HideGameOverUI();
        InitSongData(levelConfig);

        EventBus.Publish(new GameEvents.LevelStartEvent(levelConfig));

        yield return new WaitForSeconds(SongDuration);

        EndLevel();

        yield return null;
    }

    private void InitSongData(LevelConfigSO levelConfig)
    {
        currentSong = levelConfig.SongAudioClip;
        SongDuration = 0f;

        if (currentSong != null)
            SongDuration = currentSong.length;

    }

    private void EndLevel()
    {
        if (levelEnded)
            return;

        levelEnded = true;
        EventBus.Publish(new GameEvents.LevelEndEvent());
        ShowGameOverUI();
        Time.timeScale = 0f;
    }

    private void CreateGameOverUI()
    {
        if (gameOverPanel != null)
            return;

        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("GameOverCanvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas = canvasObject.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = canvasObject.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
        }

        gameOverPanel = new GameObject("GameOverPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        gameOverPanel.transform.SetParent(canvas.transform, false);

        RectTransform panelRect = gameOverPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        Image panelImage = gameOverPanel.GetComponent<Image>();
        panelImage.color = new Color(0f, 0f, 0f, 0.75f);

        GameObject textObject = new GameObject("GameOverText", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(gameOverPanel.transform, false);

        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.5f, 0.5f);
        textRect.anchorMax = new Vector2(0.5f, 0.5f);
        textRect.pivot = new Vector2(0.5f, 0.5f);
        textRect.anchoredPosition = Vector2.zero;
        textRect.sizeDelta = new Vector2(800f, 240f);

        gameOverText = textObject.GetComponent<TextMeshProUGUI>();
        gameOverText.alignment = TextAlignmentOptions.Center;
        gameOverText.color = Color.white;
        gameOverText.fontSize = 64f;
    }

    private void HideGameOverUI()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void ShowGameOverUI()
    {
        CreateGameOverUI();

        ScoreManager scoreManager = FindAnyObjectByType<ScoreManager>();
        int finalScore = scoreManager != null ? scoreManager.CurrentScore : 0;

        if (gameOverText != null)
            gameOverText.text = $"{gameOverMessage}\nScore: {finalScore}";

        gameOverPanel.SetActive(true);
    }
}


