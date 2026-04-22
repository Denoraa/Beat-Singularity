using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] public string SongName = "Default";
    [SerializeField] public int BPM = 0;
    [SerializeField] public AudioClip SongAudioClip;
    [SerializeField] public List<NoteData> noteDataList = new List<NoteData>();

}


[System.Serializable]
public struct NoteData
{
    public float hitTime;
    public NoteType noteType;
}
