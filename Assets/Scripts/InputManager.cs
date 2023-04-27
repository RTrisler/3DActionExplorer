using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnInteractPressed;

    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprint_input;
    public bool jump_input;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprint_input = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprint_input = false;
            playerControls.PlayerActions.Jump.performed += i => jump_input = true;
            playerControls.PlayerActions.Interact.performed += Interact_performed;
        }

        playerControls.Enable();
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        // Handle Action input ...
    }


    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;



        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (sprint_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }

}

