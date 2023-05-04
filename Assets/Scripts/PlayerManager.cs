using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{

    public static event Action<IInteractable> OnInteractionHit;
    public static event Action<SpecialDoor> OnSpecialInteractionHit;
    public static event Action OnInteractionNotHit;

    Animator animator;
    InputManager inputManager;
    public CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;

    public HealthBar healthBar;

    public int score { get; set; }

    [SerializeField]
    public int health;

    [SerializeField]
    private LayerMask InteractMask;

    public GameObject collectPoint;
    public bool isInteractingWithNPC;
    public bool isInteracting;
    public bool _canInteractSpecial = false;

    private RaycastHit hit;

    private void Start()
    {
        inputManager.OnInteractPressed += InputManager_OnInteractPressed;
        healthBar.SetMaxHealth(health);
    }

    private void InputManager_OnInteractPressed(object sender, System.EventArgs e)
    {
        float interactDistance = 2f;
        var interactPoint = this.transform.position + Vector3.up * .9f;
        var rayLength = this.transform.forward * 2f;
        if (Physics.Raycast(interactPoint, rayLength, out hit, interactDistance, InteractMask))
        {
            if(hit.transform.TryGetComponent<IInteractable>(out IInteractable interactor))
            {
                interactor.Interact();
            }
            if(hit.transform.TryGetComponent<ISpecialInteractable>(out ISpecialInteractable specialInteract) && _canInteractSpecial)
            {
                specialInteract.Interact();
            }
            if(hit.transform.TryGetComponent<INPCInteractable>(out INPCInteractable npcInteract))
            {
                if (!isInteractingWithNPC)
                {
                    npcInteract.InteractNPC(this.transform);
                    cameraManager.HandleAllCameraMovement();
                    isInteractingWithNPC = true;
                }
                else
                {
                    npcInteract.InteractNPC(this.transform);
                    isInteractingWithNPC = false;
                }
            }
        }
        else
        {
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        if (!isInteractingWithNPC)
        {
            inputManager.HandleAllInputs();
        }
        float interactDistance = 2f;
        var interactPoint = this.transform.position + Vector3.up * .9f;
        var rayLength = this.transform.forward * 2f;
        if (Physics.Raycast(interactPoint, rayLength, out hit, interactDistance, InteractMask))
        {
            if (hit.transform.TryGetComponent<IInteractable>(out IInteractable interactor))
            {
                OnInteractionHit?.Invoke(interactor);
            }
            if(hit.transform.TryGetComponent<SpecialDoor>(out SpecialDoor specialDoor))
            {
                OnSpecialInteractionHit?.Invoke(specialDoor);
            }
        }
        else
        {
            OnInteractionNotHit?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (!isInteractingWithNPC)
        {
            playerLocomotion.HandleAllMovement();
        }
    }

    private void LateUpdate()
    {
        if (!isInteractingWithNPC)
        {
            cameraManager.HandleAllCameraMovement();
        }

        isInteracting = animator.GetBool("isInteracting");

        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IDamageAble>(out IDamageAble damage))
        {
            damage.Damage(this);
            healthBar.SetHealth(health);
        }
    }
}
