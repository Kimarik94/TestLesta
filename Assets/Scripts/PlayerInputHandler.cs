using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private InputActionAsset inputAsset;

    private string actionMapName = "PlayerActionMap";

    private string moveName = "Move";
    private string lookName = "Look";
    private string jumpName = "Jump";


    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    public Vector2 moveInput { get; private set; }

    public Vector2 lookInput { get; private set; }

    public bool jumpInput { get; private set; }

    private void Awake()
    {
        inputAsset = Resources.Load<InputActionAsset>("Input/PlayerInputActions");

        moveAction = inputAsset.FindActionMap(actionMapName).FindAction(moveName);
        lookAction = inputAsset.FindActionMap(actionMapName).FindAction(lookName);
        jumpAction = inputAsset.FindActionMap(actionMapName).FindAction(jumpName);

        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        jumpAction.performed += context => jumpInput = true;
        jumpAction.canceled += context => jumpInput = false;

    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
    }
}

