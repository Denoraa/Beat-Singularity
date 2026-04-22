using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tapNotePrefab;
    [SerializeField] private Transform[] laneSpawnPoints;

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.NoteSpawnEvent>(OnNoteSpawn);
    }
    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.NoteSpawnEvent>(OnNoteSpawn);
    }

    public void OnNoteSpawn(GameEvents.NoteSpawnEvent eventData)
    {
        NoteData noteData = eventData.noteData;
        if (noteData == null)
        {
            Debug.LogWarning("noteData is null");
            return;
        }

        if (laneSpawnPoints == null || laneSpawnPoints.Length == 0)
        {
            Debug.LogError("laneSpawnPoints 没有设置");
            return;
        }

        if (noteData.lane < 0 || noteData.lane >= laneSpawnPoints.Length)
        {
            Debug.LogError($"lane 索引越界: {noteData.lane}");
            return;
        }

        GameObject prefab = GetPrefabByType(noteData.type);
        if (prefab == null)
        {
            Debug.LogError($"没有找到对应的 Note Prefab, type = {noteData.type}");
            return;
        }

        Transform spawnPoint = laneSpawnPoints[noteData.lane];
        GameObject noteObj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Note noteComponent = noteObj.GetComponent<Note>();
        if (noteComponent != null)
        {
            noteComponent.Initialize(noteData);
        }
    }

    private GameObject GetPrefabByType(string noteType)
    {
        switch (noteType)
        {
            case "Tap":
                return tapNotePrefab;
            default:
                Debug.LogWarning($"未知 note type: {noteType}");
                return null;
        }
    }
}