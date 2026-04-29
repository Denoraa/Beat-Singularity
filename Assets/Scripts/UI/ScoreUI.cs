using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ScoreUI : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.ScoreUpdateEvent>(OnScoreUpdate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.ScoreUpdateEvent>(OnScoreUpdate);
    }

    private void OnScoreUpdate(GameEvents.ScoreUpdateEvent eventData)
    {
        scoreText.text = eventData.currentScore.ToString();
    }

}
