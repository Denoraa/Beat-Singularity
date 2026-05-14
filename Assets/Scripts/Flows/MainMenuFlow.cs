using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFlow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.Show<MainMenuUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
