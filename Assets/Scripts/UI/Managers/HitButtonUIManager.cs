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
                    hitButtonUI.Init<GameEvents.SHitEvent>();
                    break;

                case HitBut.D:
                    hitButtonUI.Init<GameEvents.DHitEvent>();
                    break;

                case HitBut.F:
                    hitButtonUI.Init<GameEvents.FHitEvent>();
                    break;

                case HitBut.J:
                    hitButtonUI.Init<GameEvents.JHitEvent>();
                    break;

                case HitBut.K:
                    hitButtonUI.Init<GameEvents.KHitEvent>();
                    break;

                case HitBut.L:
                    hitButtonUI.Init<GameEvents.LHitEvent>();
                    break;
            }
        }
    }



}
