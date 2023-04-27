using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CameraManager cameraManager;
    PlayerLocomotion playerLocomotion;

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
                    isInteractingWithNPC = true;
                    npcInteract.Interact();
                }
                else
                {
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
        }
    }
}
