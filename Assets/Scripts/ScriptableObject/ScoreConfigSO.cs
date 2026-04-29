using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreConfig", menuName = "Scriptable Objects/ScoreConfig")]
public class ScoreConfigSO : ScriptableObject
{
    [SerializeField] public int perfectScore;
    [SerializeField] public int goodScore;
    [SerializeField] public int badScore;
    [SerializeField] public int missScore;
}

