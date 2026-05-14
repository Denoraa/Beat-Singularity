using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    class UIElement
    {
        public string Resources;
        public bool Cache;
        public GameObject Instance;

    }
    private Dictionary<Type, UIElement> UIResources = new Dictionary<Type, UIElement>();

    private Canvas uiCanvas;

    public UIManager()
    {
        uiCanvas = FindUICanvas();
        UIResources.Add(typeof(MainMenuUI), new UIElement() { Resources = "UI/MainMenuUI", Cache = false });
        UIResources.Add(typeof(LevelSelectUI), new UIElement() { Resources = "UI/LevelSelectUI", Cache = false });
    }

    public T Show<T>()
    {
        Type type = typeof(T);

        if (this.UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];

            if (info.Instance != null)
            {
                info.Instance.SetActive(true);
            }
            else
            {
                Component existingUI = UnityEngine.Object.FindObjectOfType(type) as Component;
                if (existingUI != null)
                {
                    info.Instance = existingUI.gameObject;
                    info.Instance.SetActive(true);
                    return existingUI.GetComponent<T>();
                }

                UnityEngine.Object prefab = Resources.Load(info.Resources);
                if (prefab == null)
                {
                    return default(T);
                }
                info.Instance = (GameObject)GameObject.Instantiate(prefab);

                info.Instance.transform.SetParent(uiCanvas.transform, false);
            }
            return info.Instance.GetComponent<T>();
        }

        return default(T);
    }

    private Canvas FindUICanvas()
    {
        GameObject canvasObject = GameObject.Find("UICanvas");
        if (canvasObject == null)
            canvasObject = GameObject.Find("Canvas");

        Canvas canvas = canvasObject != null ? canvasObject.GetComponent<Canvas>() : null;
        if (canvas == null)
            canvas = UnityEngine.Object.FindObjectOfType<Canvas>();

        return canvas;
    }

    public void Close(Type type)
    {
        if (UIResources.ContainsKey(type))
        {
            UIElement info = this.UIResources[type];

            if (info.Cache)
            {
                info.Instance.SetActive(false);
            }
            else
            {
                GameObject.Destroy(info.Instance);
                info.Instance = null;
            }
        }
    }

    public void Close<T>()
    {
        this.Close(typeof(T));
    }
}
