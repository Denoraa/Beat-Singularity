using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction SHitAction;
    private InputAction DHitAction;
    private InputAction FHitAction;
    private InputAction JHitAction;
    private InputAction KHitAction;
    private InputAction LHitAction;



    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        if (SHitAction != null)
            SHitAction.performed += OnLeftHitPerformed;

        if (DHitAction != null)
            DHitAction.performed += OnLeftHitPerformed;

        if (FHitAction != null)
            FHitAction.performed += OnLeftHitPerformed;

        if (JHitAction != null)
            JHitAction.performed += OnRightHitPerformed;

        if (KHitAction != null)
            KHitAction.performed += OnRightHitPerformed;

        if (LHitAction != null)
            LHitAction.performed += OnRightHitPerformed;
    }

    private void OnDisable()
    {
        if (SHitAction != null)
            SHitAction.performed -= OnLeftHitPerformed;

        if (DHitAction != null)
            DHitAction.performed -= OnLeftHitPerformed;

        if (FHitAction != null)
            FHitAction.performed -= OnLeftHitPerformed;

        if (JHitAction != null)
            JHitAction.performed -= OnRightHitPerformed;

        if (KHitAction != null)
            KHitAction.performed -= OnRightHitPerformed;

        if (LHitAction != null)
            LHitAction.performed -= OnRightHitPerformed;
    }

    private void OnLeftHitPerformed(InputAction.CallbackContext ctx)
    {
            EventBus.Publish(new GameEvents.LeftHitEvent());
    }

    private void OnRightHitPerformed(InputAction.CallbackContext ctx)
    {
            EventBus.Publish(new GameEvents.RightHitEvent());
    }

    private void Init()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            SHitAction = playerInput.actions["NoteHitS"];
            DHitAction = playerInput.actions["NoteHitD"];
            FHitAction = playerInput.actions["NoteHitF"];
            JHitAction = playerInput.actions["NoteHitJ"];
            KHitAction = playerInput.actions["NoteHitK"];
            LHitAction = playerInput.actions["NoteHitL"];
        }

    }
}
