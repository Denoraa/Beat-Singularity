using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    private float time = 0f;
    private bool isTiming = false;

    [SerializeField] private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnGameStart);
        EventBus.Subscribe<GameEvents.LevelEndEvent>(OnGameEnd);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnGameStart);
        EventBus.Unsubscribe<GameEvents.LevelEndEvent>(OnGameEnd);
    }

    private void Update()
    {
        if (!isTiming) return;

        time = MusicManager.Instance.MusicSource.time;

        if (timerText != null)
        {
            timerText.text = "Time: "+time.ToString("F2");
        }
    }

    private void OnGameStart(GameEvents.LevelStartEvent eventData)
    {
        time = 0f;
        isTiming = true;
    }

    private void OnGameEnd(GameEvents.LevelEndEvent eventData)
    {
        isTiming = false;
    }
}
