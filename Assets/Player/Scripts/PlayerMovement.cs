using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] int movementSpeed = 400;
    [SerializeField] int jumpSpeed = 4500;
    [SerializeField] bool isDead = false;
    [SerializeField] AudioClip walkSound;

    bool isWalking;
    bool isPlayingWalkingSound = false;

    Vector2 movementInput;

    //Components
    Rigidbody2D rigidBody;
    Animator animator;
    Collider2D playerCollider;
    SceneFunctionsManager sceneFunctionsManager;
    RayGun rayGun;
    AudioSource audioSource;
    Settings settings;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<PolygonCollider2D>();
        sceneFunctionsManager = FindObjectOfType<SceneFunctionsManager>();
        rayGun = FindObjectOfType<RayGun>();
        audioSource = GetComponent<AudioSource>();
        settings = FindObjectOfType<Settings>();
    }

    private void Start()
    {
        audioSource.volume = settings.GetSoundEffectSetting();
        audioSource.clip = walkSound;
        audioSource.Play();
        audioSource.Pause();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void JumpInput(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            JumpPlayer();
        }
    }

    void MovePlayer()
    {
        if (isDead) { return;  }

        float movement = movementSpeed * Time.fixedDeltaTime * movementInput.x;
        Vector2 rigidBodyInput = new(movement, rigidBody.velocity.y);

        rigidBody.velocity = rigidBodyInput;
        isWalking = !Mathf.Approximately(Mathf.Abs(rigidBody.velocity.x), Mathf.Epsilon);
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            if (!isPlayingWalkingSound)
            {
                audioSource.UnPause();
                isPlayingWalkingSound = true;
            }

            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x), transform.localScale.y);
            if (Mathf.Approximately(Mathf.Sign(transform.localScale.x), 1))
            {
                rayGun.RotateGunRight();
            }
            else if (Mathf.Approximately(Mathf.Sign(transform.localScale.x), -1))
            {
                rayGun.RotateGunLeft();
            }
        }
        else
        {
            audioSource.Pause();
            isPlayingWalkingSound = false;
        }
    }

    void JumpPlayer()
    {
        if (isDead) { return; }

        float jump = jumpSpeed * Time.fixedDeltaTime;
        Vector2 rigidBodyInput = new(rigidBody.velocity.x, jump);
        bool isTouchingGround = playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (isTouchingGround)
        {
            rigidBody.velocity = rigidBodyInput;
        }
    }

    public void TakeHit()
    {
        rigidBody.AddForce(new Vector2(0, 10));
    }

    public void DieSequence()
    {
        rigidBody.AddForce(new Vector2(0, 10));
        isDead = true;
        animator.SetBool("isDead", true);
        playerCollider.isTrigger = true;
        sceneFunctionsManager.RestartLevel();
    }
}