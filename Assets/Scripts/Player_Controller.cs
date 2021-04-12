using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Player Stats")]
    public float maxHealth;
    public float currentHealth;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float airMoveSpeed = 10f;
    private float moveButton;
    private bool facingRight = true;
    private bool isMoving;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 16f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckSize;
    private bool grounded;
    private bool canJump;

    [Header("WallSliding")]
    [SerializeField] float wallSlideSpeed;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("WallJumping")]
    [SerializeField] float walljumpforce;
    [SerializeField] Vector2 walljumpAngle;
    [SerializeField] float walljumpDirection = -1;

    [Header("Transform")]
    [SerializeField] BoxCollider2D playerCollider_A;
    [SerializeField] CircleCollider2D playerCollider_B;
    [SerializeField] CircleCollider2D ballCollider;
    public bool isBall = false;

    [Header("Other")]
    [SerializeField] Animator anim;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        walljumpAngle.Normalize();

        currentHealth = maxHealth;
    }

    private void Update()
    {
        PlayerInputs();
        GWChecks();
        AnimationsController();
    }

    private void FixedUpdate()
    {
        Movement();
        Jump();
        PlayerTransform();
        BallWallBounce();
    }

    void PlayerInputs()
    {
        //  Player Inputs
        moveButton = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            canJump = true;
        }
        if (Input.GetButtonDown("Transform"))
        {
            isBall = !isBall;
        }
    }

    void GWChecks()
    {
        //  Ground Checks
        if (!isBall) grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayer);
        else grounded = Physics2D.OverlapCircle(this.transform.position, 4.2f, groundLayer);

        //  Wall Checks
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    void Movement()
    {
        //  Animation
        if (moveButton != 0) isMoving = true;
        else isMoving = false;
        
        //  Movement
        if (grounded)
        {
            rb2d.velocity = new Vector2(moveButton * moveSpeed, rb2d.velocity.y);
        }
        else if (!grounded && (!isWallSliding || !isTouchingWall) && moveButton != 0)
        {
            rb2d.AddForce(new Vector2(airMoveSpeed * moveButton, 0));
            if (Mathf.Abs(rb2d.velocity.x) > moveSpeed)
            {
                rb2d.velocity = new Vector2(moveButton * moveSpeed, rb2d.velocity.y);
            }
        }

        //for fliping
        if (moveButton < 0 && facingRight) Flip();
        else if (moveButton > 0 && !facingRight) Flip();
    }

    void Flip()
    {
        if (!isWallSliding)
        {
            walljumpDirection *= -1;
            facingRight = !facingRight;
        }

        if (facingRight) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        //  Normal Jump
        if (canJump && grounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            canJump = false;
        }

        if (!isBall)
        {
            //  Wall Jump
            if ((isWallSliding || isTouchingWall) && canJump)
            {
                rb2d.AddForce(new Vector2(walljumpforce * walljumpAngle.x * walljumpDirection, walljumpforce * walljumpAngle.y), ForceMode2D.Impulse);
                Flip();
                canJump = false;
            }

            //  Wall Slide
            if (isTouchingWall && !grounded && rb2d.velocity.y < 0) isWallSliding = true;
            else isWallSliding = false;
            if (isWallSliding) rb2d.velocity = new Vector2(rb2d.velocity.x, -wallSlideSpeed);
        }
    }

    void PlayerTransform()
    {
        if (isBall)
        {
            playerCollider_A.enabled = false;
            playerCollider_B.enabled = false;
            ballCollider.enabled = true;

            rb2d.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            playerCollider_A.enabled = true;
            playerCollider_B.enabled = true;
            ballCollider.enabled = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void BallWallBounce()
    {

    }

    void AnimationsController()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isSliding", isTouchingWall);
        anim.SetBool("isBall", isBall);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.1f);
    }
}
