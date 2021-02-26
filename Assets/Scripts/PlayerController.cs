using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //  Player Variables
    private Rigidbody2D rb2d;
    private float gravityStore;

    //  Movement Variables
    [Header("Player Movement Variables")]
    public float playerSpeed = 10.0f;
    private float moveButton;
    private bool faceingLeft;

    //  Jump Variables
    [Header("Player Jump Variables")]
    public float jumpForce = 15.0f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private bool jumpButton, grounded;

    //  Wall Jump Variables
    [Header("Player Wall Jump Variables")]
    public Transform wallCheck;
    private bool isWall, isGrabbing;

    //  Transform Variables
    [Header("Transform Variables")]
    public GameObject _cube;
    public GameObject _sphere; // Test Variables to show the Mechanic can work
    public bool isBall = false;


    private void Awake()
    {
        rb2d = this.transform.GetComponent<Rigidbody2D>();
        groundCheck = this.transform.GetChild(0).GetComponent<Transform>();
        wallCheck = this.transform.GetChild(1).GetComponent<Transform>();
    }

    private void Start()
    {
        gravityStore = rb2d.gravityScale;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
        PlayerTransform();
    }

    void PlayerMovement()
    {
        //  Player Movement
        moveButton = Input.GetAxis("Horizontal") * playerSpeed;

        if (moveButton > 0)
        {
            rb2d.velocity = new Vector2(moveButton, rb2d.velocity.y);
            faceingLeft = false;
        }
        if (moveButton < 0)
        {
            rb2d.velocity = new Vector2(moveButton, rb2d.velocity.y);
            faceingLeft = true;
        }

        // Player Direction
        if (faceingLeft) this.transform.localScale = new Vector3(-1, 1, 1);
        else this.transform.localScale = new Vector3(1, 1, 1);
    }

    void PlayerJump()
    {
        //  Player Jump
        jumpButton = Input.GetButton("Jump");
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        if (jumpButton && grounded) rb2d.AddForce(Vector2.up * jumpForce * 10);

        //  Wall Jump
        isWall = Physics2D.OverlapCircle(wallCheck.position, 0.2f, whatIsGround);

        isGrabbing = false;

        if (!grounded && isWall)
        {
            isGrabbing = true;
        }

        if (isGrabbing)
        {
            rb2d.gravityScale = 0;
            if (jumpButton)
            {
                rb2d.gravityScale = gravityStore;
                if (faceingLeft)
                {
                    rb2d.velocity = new Vector2(jumpForce / 2, jumpForce / 2);
                    faceingLeft = !faceingLeft;
                }
                else
                {
                    rb2d.velocity = new Vector2(-jumpForce / 2, jumpForce / 2);
                    faceingLeft = !faceingLeft;
                }
                isGrabbing = false;
            }

        }
        else
        {
            rb2d.gravityScale = gravityStore;
        }
    }

    void PlayerTransform()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) isBall = !isBall;

        if (isBall)
        {
            _cube.SetActive(false);
            _sphere.SetActive(true);
        }
        else
        {
            _cube.SetActive(true);
            _sphere.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.position, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(wallCheck.position, 0.2f);
    }
}
