using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HitButtonUI : MonoBehaviour
{
    [SerializeField] public HitBut button;

    private Image image;
    private TextMeshProUGUI label;

    public void Init<T>()
    {
        EventBus.Subscribe<T>(OnHit);

        VisualInit();
        LabelInit();
    }

    private void OnHit<T>(T eventData)
    {
        StartCoroutine(VisualHit());
    }

    private void VisualInit()
    {
        image = GetComponent<Image>();

        Color color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        image.color = color;
    }

    private void LabelInit()
    {
        if (button == HitBut.None)
            return;

        Transform labelTransform = transform.Find("KeyLabel");
        GameObject labelObject;

        if (labelTransform == null)
        {
            labelObject = new GameObject("KeyLabel", typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            labelObject.transform.SetParent(transform, false);
        }
        else
        {
            labelObject = labelTransform.gameObject;
        }

        RectTransform labelRect = labelObject.GetComponent<RectTransform>();
        labelRect.anchorMin = Vector2.zero;
        labelRect.anchorMax = Vector2.one;
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        label = labelObject.GetComponent<TextMeshProUGUI>();
        label.text = button.ToString();
        label.alignment = TextAlignmentOptions.Center;
        label.color = Color.white;
        label.fontSize = 36f;
        label.enableAutoSizing = true;
        label.fontSizeMin = 18f;
        label.fontSizeMax = 36f;
        label.raycastTarget = false;
    }

    private IEnumerator VisualHit()
    {
        Color color = new Color(1f, 1f, 1f, 1f);
        Color oldColor = image.color;

        image.color = color;
        yield return new WaitForSeconds(0.1f);
        image.color = oldColor;
    }

}
