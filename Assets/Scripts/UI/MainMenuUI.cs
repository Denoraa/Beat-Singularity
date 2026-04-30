using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button quitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingButton.onClick.AddListener(OnSettingButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }
    private void OnStartButtonClicked()
    {
        LevelManager.Instance.LoadLevel(1);
    }
    private void OnSettingButtonClicked()
    {
        throw new NotImplementedException();
    }
    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }




}
