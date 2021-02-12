using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //  Movement Controls
    Rigidbody2D rb2d;

    public float moveSpeed = 10;

    public float jumpForce = 10;
    public Transform groundCheck;
    public LayerMask whatIsGround;



    private void Start()
    {
        rb2d = this.transform.GetComponent<Rigidbody2D>();
        groundCheck = this.transform.GetChild(0).GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
    }
    
    void PlayerMovement()
    {
        //  Player Movement
        float move = Input.GetAxis("Horizontal") * moveSpeed;

        if (move < 0) rb2d.velocity = new Vector3(move, rb2d.velocity.y);
        if (move > 0) rb2d.velocity = new Vector3(move, rb2d.velocity.y);
    }
    void PlayerJump()
    {
        //  Player Jump
        bool jump = Input.GetButton("Jump");
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);

        if (jump && grounded) rb2d.AddForce(Vector3.up * jumpForce);
    } 
}
