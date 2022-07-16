using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float defaultGravityScale = 1.5f;
    public Camera mainCamera;
    public float timeSinceGrounded = 0;
    public float timeSinceLastJumpPress = 0;
    public float jumpGracePeriod = 0.3F;
    public float groundedGracePeriod = 0.3F;
    public float upwardJumpHeldModifier;
    public float downwardVelocityMultiplier;
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip runSound;
    

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = defaultGravityScale;
        facingRight = t.localScale.x > 0;
        mainCamera = MainCameraBehaviour.theMainCamera.GetComponent<Camera>();
        Debug.Log(mainCamera);

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if (Input.GetAxis("Horizontal") != 0)
        {
            moveDirection = Input.GetAxis("Horizontal");
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        if (isGrounded)
        {
            timeSinceGrounded = 0;
        }



        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeSinceLastJumpPress = 0;
            if (timeSinceGrounded < groundedGracePeriod)
            {
                jump();
            }
        }

        r2d.gravityScale = Input.GetKey(KeyCode.Space) ? upwardJumpHeldModifier : 1.5F;

        if (r2d.velocity.y < 0)
        {
            r2d.gravityScale = downwardVelocityMultiplier;
            
        }

        if (isGrounded && timeSinceLastJumpPress < jumpGracePeriod)
        {
            jump();
        }

        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }
        
        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, t.position.y, cameraPos.z);
        }

        timeSinceGrounded += Time.deltaTime;
        timeSinceLastJumpPress += Time.deltaTime;
        
        handleAnimation();
        handleSound();
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    void jump()
    {
        audioSource.clip = jumpSound;
        audioSource.Play();
        r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        timeSinceLastJumpPress = jumpGracePeriod + 1;
    }


    private void handleSound()
    {
        if (moveDirection != 0 && isGrounded && !audioSource.isPlaying)
        {
            audioSource.clip = runSound;
            audioSource.Play();
        }
    }
    private void handleAnimation()
    {
        var currentAnimationState = animator.GetCurrentAnimatorStateInfo(0);
        if (moveDirection != 0 && isGrounded)
        {
            if (!currentAnimationState.IsName("DinoRun"))
            {
                animator.Play("DinoRun");
            }
        }
        else
        {
            animator.Play("DinoStand");
        }
    }
}