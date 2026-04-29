using Unity.VisualScripting;
using UnityEngine;

public class EventBusVisualScriptingBridge : MonoBehaviour
{
    [SerializeField] private string levelStartEventName = "LevelStartEvent";
    [SerializeField] private string noteJudgeEventName = "NoteJudgeEvent";

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
        CustomEvent.Trigger(gameObject, levelStartEventName);
    }

    private void OnNoteJudge(GameEvents.NoteJudgeEvent eventData)
    {
        CustomEvent.Trigger(gameObject, noteJudgeEventName, eventData.hitResult);
    }
}
