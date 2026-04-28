using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButtonUIManager : MonoBehaviour
{
    private HitButtonUI[] hitButtonUIs;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        hitButtonUIs = GetComponentsInChildren<HitButtonUI>();

        foreach (var hitButtonUI in hitButtonUIs)
        {
            switch (hitButtonUI.button)
            {
                case HitBut.None:
                    Debug.LogWarning("Not Assigned ButtonUI");
                    break;

                case HitBut.S:
                case HitBut.D:
                case HitBut.F:
                    hitButtonUI.Init<GameEvents.DownHitEvent>();
                    break;

                case HitBut.J:
                case HitBut.K:
                case HitBut.L:
                    hitButtonUI.Init<GameEvents.TopHitEvent>();
                    break;
            }
        }
    }



}
