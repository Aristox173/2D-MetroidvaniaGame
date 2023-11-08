using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    #endregion

    #region Numeric Values

    private int facingDir = 1;

    [Header("Movement Info")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;
    private float xInput;

    [Header("Dash Info")]
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    private float dashTime;

    [SerializeField]
    private float dashCooldown;
    private float dashCooldownTimer;
    private bool canDash = true; // Flag to control dashing

    [Header("Ground Check")]
    [SerializeField]
    private float groundCheckDistance;
    [SerializeField]
    private LayerMask whatIsGround;
    private bool isGrounded;
    #endregion

    #region Boolean Values

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckInput();
        Movement();
        Flip();
        AnimatorController();

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        if (isGrounded)
            canDash = true;

        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
    }

    private void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            Dash();
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0); // Dash horizontally with y velocity set to 0
            rb.gravityScale = 0; // Disable gravity during the dash
        }
        else
        {
            rb.gravityScale = 9.8f; // Reset gravity scale to its normal value
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

    }

    private void Flip()
    {
        if (xInput < 0)
        {
            spriteRenderer.flipX = true;
            facingDir = -1;
        }
        else if (xInput > 0)
        {
            spriteRenderer.flipX = false;
            facingDir = 1;
        }
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Dash()
    {
        if (dashCooldownTimer < 0)
        {
            dashCooldownTimer = dashCooldown;
            dashTime = dashDuration;
            canDash = false; // Disable dashing until the player is grounded
        }
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;

        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isMoving", isMoving);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}