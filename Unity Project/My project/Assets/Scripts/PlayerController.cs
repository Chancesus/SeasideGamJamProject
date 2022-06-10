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
    Animator animator;
    
    void Start()
    {
        playerRigid = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        FlipSprite();
        IsGrounded();
        Walking();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            KickAttack();
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

    private void Walking()
    {
        if (playerRigid.velocity.x != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void KickAttack()
    {
        animator.SetTrigger("KickAttack");
    }
}
