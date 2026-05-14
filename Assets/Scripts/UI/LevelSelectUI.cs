using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private Button firstLevelButton;
    [SerializeField] private string firstLevelSceneName = "VSScene";
    [SerializeField] private string firstLevelLabel = "Level 1";

    private void OnEnable()
    {
        EnsureReferences();

        if (firstLevelButton != null)
            firstLevelButton.onClick.AddListener(OnFirstLevelClicked);
    }

    private void OnDisable()
    {
        if (firstLevelButton != null)
            firstLevelButton.onClick.RemoveListener(OnFirstLevelClicked);
    }

    private void EnsureReferences()
    {
        Button[] buttons = GetComponentsInChildren<Button>(true);

        if (firstLevelButton == null && buttons.Length > 0)
            firstLevelButton = buttons[0];

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(i == 0);

        if (firstLevelButton == null)
            return;

        firstLevelButton.gameObject.name = "Level1Button";
        TMP_Text label = firstLevelButton.GetComponentInChildren<TMP_Text>(true);
        if (label != null)
            label.text = firstLevelLabel;
    }

    private void OnFirstLevelClicked()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }
}
