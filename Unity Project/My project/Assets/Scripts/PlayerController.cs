using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpVelocity = 10;
    [SerializeField] float moveSpeed = 10f;
    SpriteRenderer playerSprite;

    [SerializeField] Transform feet;
    [SerializeField] LayerMask groundLayer;



    float horizontal;
    Rigidbody2D playerRigid;
    
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        FlipSprite();
        IsGrounded();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
        
    }

    private bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayer);
        if (groundCheck)
        {
            return true;
        }
        return false;
    }

    private void FixedUpdate() => playerRigid.velocity = new Vector2(horizontal * moveSpeed, playerRigid.velocity.y);

    private void Jump()
    {
        Vector2 jump = new Vector2(playerRigid.velocity.x, jumpVelocity);
        playerRigid.velocity = jump;
    }

    private void FlipSprite()
    {
        if (playerRigid.velocity.x < 0)
        {
            playerSprite.flipX = true;
        }
        else
        {
            playerSprite.flipX = false;
        }
    }
}
