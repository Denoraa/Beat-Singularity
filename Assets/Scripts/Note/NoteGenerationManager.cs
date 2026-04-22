using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerationManager : MonoBehaviour
{

    private float spawnLeadTime = 2.0f;

    private AudioSource musicSource;

    public void Awake()
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
        StartCoroutine(StartGenerating(eventData.levelConfig));
    }
    private IEnumerator StartGenerating(LevelConfigSO levelConfig)
    {
        if (levelConfig == null || levelConfig.noteDataList == null || levelConfig.noteDataList.Count == 0)
            yield break;

        List<NoteData> noteDatas = levelConfig.noteDataList;
        int i = 0;

        while (i < noteDatas.Count)
        {
            NoteData noteData = noteDatas[i];

            float spawnTime = noteData.hitTime - spawnLeadTime;

            while (musicSource.time < spawnTime)
            {
                yield return null;
            }

            SpawnNote(noteData);

            i++;
        }

        Debug.Log("All notes generated.");
    }

    private void SpawnNote(NoteData noteData)
    {
        throw new NotImplementedException();
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        throw new NotImplementedException();
    }




}


