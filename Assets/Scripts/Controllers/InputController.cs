using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction leftHitAction;
    private InputAction rightHitAction;


    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        if (leftHitAction != null)
            leftHitAction.performed += OnLeftHitPerformed;

        if (rightHitAction != null)
            rightHitAction.performed += OnRightHitPerformed;
    }

    private void OnDisable()
    {
        if (leftHitAction != null)
            leftHitAction.performed -= OnLeftHitPerformed;

        if (rightHitAction != null)
            rightHitAction.performed -= OnRightHitPerformed;
    }

    private void OnLeftHitPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.control == Keyboard.current.sKey)
            EventBus.Publish(new GameEvents.SHitEvent());
        else if (ctx.control == Keyboard.current.dKey)
            EventBus.Publish(new GameEvents.DHitEvent());
        else if (ctx.control == Keyboard.current.fKey)
            EventBus.Publish(new GameEvents.FHitEvent());
    }

    private void OnRightHitPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.control == Keyboard.current.jKey)
            EventBus.Publish(new GameEvents.JHitEvent());
        else if (ctx.control == Keyboard.current.kKey)
            EventBus.Publish(new GameEvents.KHitEvent());
        else if (ctx.control == Keyboard.current.lKey)
            EventBus.Publish(new GameEvents.LHitEvent());
    }

    private void Init()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            leftHitAction = playerInput.actions["NoteHitLeft"];
            rightHitAction = playerInput.actions["NoteHitRight"];
        }

    }
}
