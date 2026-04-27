using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitManager : MonoBehaviour
{

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.LeftHitEvent>(OnLeftHit);
        EventBus.Subscribe<GameEvents.RightHitEvent>(OnRightHit);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.LeftHitEvent>(OnLeftHit);
        EventBus.Unsubscribe<GameEvents.RightHitEvent>(OnRightHit);
    }
    private void OnLeftHit(GameEvents.LeftHitEvent @event)
    {
        Debug.Log("Left Hit");
    }

    private void OnRightHit(GameEvents.RightHitEvent @event)
    {
        Debug.Log("Right Hit");
    }


}


