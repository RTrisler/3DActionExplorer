using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    InputManager inputManager;
    AnimatorManager animatorManager;
    STEPManager stepManager;
    PlayerCombat playerCombat;
    Transform myTransform;
    

    Vector3 moveDirection;
    Transform cameraObject;
    public Rigidbody playerRigidBody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;
    public float minimumDistanceNeededToBeginFall = 1f;
    public bool falling;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    

    public bool _extendedJump = false;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15f;

    [Header("Movement State")]
    public MovementState currentMovementState;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        animatorManager = GetComponent<AnimatorManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        stepManager = GetComponent<STEPManager>();
        playerCombat = GetComponent<PlayerCombat>();
        cameraObject = Camera.main.transform;
        myTransform = transform;

    }

    public void HandleAllMovement()
    {
        
        HandleFallingAndLanding();
        
        Debug.Log(playerCombat.isAttacking + " : isAttacking");
        if (playerManager.isInteracting)
            return;
        HandleRolling();
        HandleMovement();
        HandleRotation();
        
    }

    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }
        Vector3 movementVelocity = moveDirection;
        if(moveDirection == Vector3.zero) //changes movement state based on if the player is move or not
        {
            currentMovementState = MovementState.Uncharging;
        }
        else
        {
            currentMovementState = MovementState.Charging;
        }
        playerRigidBody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Vector3 targetPosition;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;
        targetPosition = transform.position;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                falling = true;
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        Debug.DrawRay(rayCastOrigin, -Vector3.up* minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (Physics.Raycast(rayCastOrigin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                falling = false;
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
            //playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded && !isJumping)
        {
            if (playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }

    }

    public void HandleJumping(float jumpCharge)
    {
        Debug.Log(_extendedJump);
        if(jumpCharge > 5)
        {
            jumpCharge = 5;
        }
        if (isGrounded && !_extendedJump)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
        else if(isGrounded && _extendedJump && jumpCharge > 1)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * (jumpHeight + jumpCharge));
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
        else if (isGrounded && _extendedJump && jumpCharge < 1)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;
        }
    }

    private void HandleRolling()
    {
        if (animatorManager.animator.GetBool("isInteracting"))
            return;

        if (inputManager.roll_flag)
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection += cameraObject.right * inputManager.horizontalInput;

            if(inputManager.moveAmount > 0)
            {
                animatorManager.PlayTargetAnimation("Roll", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            else
            {
                animatorManager.PlayTargetAnimation("Backstep", true);
            }
            
        }
    }

    public void SpeedUpMovement()
    {
        walkingSpeed += 1f;
        runningSpeed += 1f;
        sprintingSpeed += 1f;
    }

    public void SlowDownMovement()
    {
        walkingSpeed -= 3f;
        runningSpeed -= 3f;
        sprintingSpeed -= 3f;
    }
}
