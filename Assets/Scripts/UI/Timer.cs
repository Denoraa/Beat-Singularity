using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time = 0f;
    private bool isTiming = false;

    [SerializeField] private TextMeshProUGUI timerText;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnGameStart);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnGameStart);
    }

    private void Update()
    {
        if (!isTiming) return;

        time = MusicManager.Instance.MusicSource.time;

        if (timerText != null)
        {
            timerText.text = time.ToString("F2");
        }
    }

    private void OnGameStart(GameEvents.LevelStartEvent eventData)
    {
        time = 0f;
        isTiming = true;
    }
}