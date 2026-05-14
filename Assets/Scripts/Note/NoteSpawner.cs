using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tapNotePrefab;
    [SerializeField] private GameObject blackHoleNotePrefab;
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
        GameObject prefab = GetPrefabByType(noteData.type);
        if (prefab == null)
        {
            Debug.LogError($"没有找到对应的 Note Prefab, type = {noteData.type}");
            return;
        }

        Transform spawnPoint = laneSpawnPoints[((int)noteData.lane)];
        GameObject noteObj = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Note noteComponent = noteObj.GetComponent<Note>();
        if (noteComponent != null)
        {
            noteComponent.Initialize(noteData,eventData.difficultyConfig);
        }
    }

    private GameObject GetPrefabByType(NoteType noteType)
    {
        switch (noteType)
        {
            case NoteType.Tap:
                return tapNotePrefab;
            case NoteType.BlackHole:
                return blackHoleNotePrefab != null ? blackHoleNotePrefab : tapNotePrefab;
            default:
                Debug.LogWarning($"未知 note type: {noteType}");
                return null;
        }
    }
}
