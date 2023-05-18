using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public event EventHandler OnInteractPressed;

    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    PlayerCombat playerCombat;
    public JumpChargeBar jumpChargeBar;
    public PauseMenu pauseMenu;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    private float _jumpCharge;

    public bool sprint_input;
    public bool jump_input;
    private bool _jumpCharging;
    public bool _extendedJump = false;

    public bool attack_input;
    public bool _canSprint = false;
    
    public bool roll_input;
    public bool roll_flag;
    
    public bool pausedInput = false;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerCombat = GetComponent<PlayerCombat>();
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
            playerControls.PlayerActions.Jump.performed += StartJump;
            playerControls.PlayerActions.Jump.canceled += StopJump;
            playerControls.PlayerActions.Interact.performed += Interact_performed;

            playerControls.PlayerActions.Attack.performed += i => attack_input = true;
            playerControls.PlayerActions.Roll.performed += i => roll_input = true;
            playerControls.PlayerActions.Pause.performed += i => pausedInput = true;
        }

        playerControls.Enable();
    }

    private void Update()
    {
        jumpChargeBar.SetCharge(0f);
        if (_jumpCharging)
        {
            _jumpCharge += Time.deltaTime;
            if (_extendedJump)
                jumpChargeBar.SetCharge(_jumpCharge);
        }
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
        HandleAttackInput();
        HandleRollInput();
        HandlePauseInput();
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
        if (sprint_input && moveAmount > 0.5f && _canSprint)
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
            playerLocomotion.HandleJumping(0f);
        }
    }

    private void HandleAttackInput()
    {
        if (attack_input)
        {
            attack_input = false;
            playerCombat.HandleAttack();
        }
    }

    private void HandleRollInput()
    {

        if (roll_input)
        {
            roll_input = false;
            Debug.LogWarning("Roll ");
            roll_flag = true;
        }
    }

    private void HandlePauseInput()
    {
        if(pausedInput)
        {
            Debug.Log("Pause ");
            pauseMenu.PauseGame();
            pausedInput = false;
        }
    }

    private void StartJump(InputAction.CallbackContext value)
    {
        _jumpCharging = true;
    }

    private void StopJump(InputAction.CallbackContext value)
    {
        _jumpCharging = false;
        playerLocomotion.HandleJumping(_jumpCharge);
        _jumpCharge = 0f;
    }

}

