using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Subscribe<GameEvents.TopHitEvent>(OnTopHit);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<GameEvents.DownHitEvent>(OnDownHit);
        EventBus.Unsubscribe<GameEvents.TopHitEvent>(OnTopHit);
    }

    private void OnDownHit(GameEvents.DownHitEvent @event)
    {
        int randomIndex = UnityEngine.Random.Range(1, 4);
        switch(randomIndex)
        {
            case 1:
                animator.SetTrigger("DownHit"+randomIndex.ToString());
                break;
            case 2:
                animator.SetTrigger("DownHit"+randomIndex.ToString());
                break;
            case 3:
                animator.SetTrigger("DownHit"+randomIndex.ToString());
                break;
        }

    }

    private void OnTopHit(GameEvents.TopHitEvent @event)
    {
        int randomIndex = UnityEngine.Random.Range(1, 4);
        switch (randomIndex)
        {
            case 1:
                animator.SetTrigger("TopHit" + randomIndex.ToString());
                break;
            case 2:
                animator.SetTrigger("TopHit" + randomIndex.ToString());
                break;
            case 3:
                animator.SetTrigger("TopHit" + randomIndex.ToString());
                break;
        }
    }

}
