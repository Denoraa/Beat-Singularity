using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig", menuName = "Scriptable Objects/DifficultyConfig")]
public class DifficultyConfigSO : ScriptableObject
{
    [SerializeField] public float perfectThreshold;
    [SerializeField] public float goodThreshold;
    [SerializeField] public float badThreshold;
}
