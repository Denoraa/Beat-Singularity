using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoSingleton<MusicManager>
{


    [SerializeField] private AudioSource musicSource;

    protected override void OnAwake()
    {
        Init();
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


    private void Init()
    {
        musicSource = GetComponent<AudioSource>();
    }

    private void OnLevelStart(GameEvents.LevelStartEvent eventData)
    {
        musicSource.clip = eventData.levelConfig.SongAudioClip;

        musicSource.Play();
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public AudioSource MusicSource
    {
        get { return musicSource; }
        private set { musicSource = value; }
    }
}
