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
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnLevelStart);
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

    public AudioSource MusicSource
    {
        get { return musicSource; }
        private set { musicSource = value; }
    }
}
