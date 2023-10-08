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

    [SerializeField]
    private float moveSpeed;
    private float xInput;

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
    }

    private void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (xInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (xInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void AnimatorController()
    {
        bool isMoving = rb.velocity.x != 0;
        animator.SetBool("isMoving", isMoving);
    }
}