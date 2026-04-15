using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Scriptable Objects/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] public string SongName = "Default";
    [SerializeField] public int SongDuration = 0;

}
