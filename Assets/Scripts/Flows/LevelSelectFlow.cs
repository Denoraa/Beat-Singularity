using UnityEngine;

public class LevelSelectFlow : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.Show<LevelSelectUI>();
    }
}
