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
    private bool isLevelActive;
    private int lastDownHitFrame = -1;
    private int lastTopHitFrame = -1;

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

        EventBus.Subscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Subscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
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

        EventBus.Unsubscribe<GameEvents.LevelStartEvent>(OnLevelStart);
        EventBus.Unsubscribe<GameEvents.LevelEndEvent>(OnLevelEnd);
    }

    private void OnLeftHitPerformed(InputAction.CallbackContext ctx)
    {
        if (!isLevelActive)
            return;

        if (lastDownHitFrame == Time.frameCount)
            return;

        lastDownHitFrame = Time.frameCount;
        EventBus.Publish(new GameEvents.DownHitEvent());
    }

    private void OnRightHitPerformed(InputAction.CallbackContext ctx)
    {
        if (!isLevelActive)
            return;

        if (lastTopHitFrame == Time.frameCount)
            return;

        lastTopHitFrame = Time.frameCount;
        EventBus.Publish(new GameEvents.TopHitEvent());
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
            SetInputEnabled(false);
        }

    }

    private void OnLevelStart(GameEvents.LevelStartEvent eventData)
    {
        isLevelActive = true;
        lastDownHitFrame = -1;
        lastTopHitFrame = -1;
        SetInputEnabled(true);
    }

    private void OnLevelEnd(GameEvents.LevelEndEvent eventData)
    {
        isLevelActive = false;
        SetInputEnabled(false);
    }

    private void SetInputEnabled(bool isEnabled)
    {
        if (playerInput == null || playerInput.actions == null)
            return;

        if (isEnabled)
            playerInput.actions.Enable();
        else
            playerInput.actions.Disable();
    }
}
