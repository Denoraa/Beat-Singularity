using UnityEngine;

[CreateAssetMenu(fileName = "FeverConfig", menuName = "Scriptable Objects/FeverConfig")]
public class FeverConfigSO : ScriptableObject
{
    [SerializeField] public float duration = 8f;
    [SerializeField] public float scoreMultiplier = 2f;
    [SerializeField] public float judgementWindowMultiplier = 1.5f;
    [SerializeField] public Color feverBackgroundColor = new Color(0.05f, 0f, 0.12f, 1f);
}
