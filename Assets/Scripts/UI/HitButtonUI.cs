using System.Collections;
using UnityEngine;
using UnityEngine.UI;   


public class HitButtonUI : MonoBehaviour
{
    [SerializeField] public HitBut button;

    private Image image;

    public void Init<T>()
    {
        EventBus.Subscribe<T>(OnHit);

        VisualInit();

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

    private IEnumerator VisualHit()
    {
        Color color = new Color(1f, 1f, 1f, 1f);
        Color oldColor = image.color;

        image.color = color;
        yield return new WaitForSeconds(0.1f);
        image.color = oldColor;
    }

}
