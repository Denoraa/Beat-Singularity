using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerationManager : MonoSingleton<NoteGenerationManager>
{

    private AudioSource musicSource;
    private Coroutine generatingCoroutine;
    private bool isGenerating;

    protected override void OnAwake()
    {
        musicSource = MusicManager.Instance.MusicSource;
    }

    public void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Subscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Unsubscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
    }
    private void OnLevelStart(GameEvents.LevelStartEvent eventData)
    {
        StopGenerating();
        isGenerating = true;
        generatingCoroutine = StartCoroutine(StartGenerating(eventData.levelConfig));
    }
    private IEnumerator StartGenerating(LevelConfigSO levelConfig)
    {
        if (levelConfig == null || levelConfig.noteDataList == null || levelConfig.noteDataList.Count == 0)
            yield break;

        if (musicSource == null && MusicManager.Instance != null)
            musicSource = MusicManager.Instance.MusicSource;

        if (musicSource == null)
            yield break;

        List<NoteData> noteDatas = levelConfig.noteDataList;
        int i = 0;

        while (isGenerating && i < noteDatas.Count)
        {
            NoteData noteData = noteDatas[i];

            while (isGenerating && musicSource.time < noteData.spawnTime)
                yield return null;

            if (!isGenerating)
                yield break;

            if (noteData.type != NoteType.Blank)
                EventBus.Publish(new GameEvents.NoteSpawnEvent(noteData,levelConfig.difficultyConfig));

            i++;
        }

        generatingCoroutine = null;
        Debug.Log("All notes generated.");
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        StopGenerating();
        ActiveNoteManager.Instance.ClearAllNotes();
    }

    private void StopGenerating()
    {
        isGenerating = false;

        if (generatingCoroutine != null)
        {
            StopCoroutine(generatingCoroutine);
            generatingCoroutine = null;
        }
    }


}


