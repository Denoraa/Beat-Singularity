using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlowManager : MonoSingleton<LevelFlowManager>
{
    private AudioClip currentSong = null;
    private float SongDuration = 0;
    public IEnumerator StartLevelFlow(LevelConfigSO levelConfig)
    {
        InitSongData(levelConfig);

        EventBus.Publish(new GameEvents.LevelStartEvent(levelConfig));

        yield return new WaitForSeconds(SongDuration);

        EventBus.Publish(new GameEvents.LevelEndEvent());

        yield return null;
    }
    private void InitSongData(LevelConfigSO levelConfig)
    {
        currentSong = levelConfig.SongAudioClip;

        if (currentSong != null)
            SongDuration = currentSong.length;

    }

}


