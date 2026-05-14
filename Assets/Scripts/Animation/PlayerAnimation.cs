using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float transitionDuration = 0.03f;
    [SerializeField] private float returnToIdleDelay = 0.22f;
    [SerializeField] private float duplicateInputGuardTime = 0.04f;

    private readonly string[] downHitStates = { "ComboAttackC", "SwordAttack", "ComboAttackA" };
    private readonly string[] topHitStates = { "AirSlashUp", "AirSlash", "AirSlashDown" };
    private readonly string[] triggerNames = { "DownHit1", "DownHit2", "DownHit3", "TopHit1", "TopHit2", "TopHit3", "Idle" };

    private Coroutine returnToIdleCoroutine;
    private float lastPlayTime = -1f;
    private string lastStateName;

    private void Awake()
    {
        if (animator == null)
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

        if (returnToIdleCoroutine != null)
        {
            StopCoroutine(returnToIdleCoroutine);
            returnToIdleCoroutine = null;
        }
    }

    private void OnDownHit(GameEvents.DownHitEvent @event)
    {
        PlayHitAnimation(downHitStates);
    }

    private void OnTopHit(GameEvents.TopHitEvent @event)
    {
        PlayHitAnimation(topHitStates);
    }

    private void PlayHitAnimation(string[] stateNames)
    {
        if (animator == null || stateNames == null || stateNames.Length == 0)
            return;

        if (Time.time - lastPlayTime < duplicateInputGuardTime)
            return;

        string stateName = PickState(stateNames);
        ClearTriggers();

        animator.CrossFadeInFixedTime(stateName, transitionDuration, 0, 0f);

        lastPlayTime = Time.time;
        lastStateName = stateName;

        if (returnToIdleCoroutine != null)
            StopCoroutine(returnToIdleCoroutine);

        returnToIdleCoroutine = StartCoroutine(ReturnToIdleAfterDelay());
    }

    private string PickState(string[] stateNames)
    {
        if (stateNames.Length == 1)
            return stateNames[0];

        string stateName = stateNames[Random.Range(0, stateNames.Length)];
        if (stateName == lastStateName)
            stateName = stateNames[(System.Array.IndexOf(stateNames, stateName) + 1) % stateNames.Length];

        return stateName;
    }

    private IEnumerator ReturnToIdleAfterDelay()
    {
        yield return new WaitForSeconds(returnToIdleDelay);

        ClearTriggers();
        animator.CrossFadeInFixedTime("SwordIdle", transitionDuration, 0, 0f);
        returnToIdleCoroutine = null;
    }

    private void ClearTriggers()
    {
        foreach (string triggerName in triggerNames)
            animator.ResetTrigger(triggerName);
    }

}
