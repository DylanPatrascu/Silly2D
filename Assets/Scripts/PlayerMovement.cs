using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float horizontalMovement;
    public float verticalMovement;
    public SpriteRenderer sprite;
    public Animator animator;

    private float lastHorizontalDirection = 1f;

    public PauseMenuController pauseMenu;

    public bool interact;

    public bool canMove = true;
    public bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!canMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if(horizontalMovement != 0)
        {
            lastHorizontalDirection = horizontalMovement;
        }
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        sprite.flipX = lastHorizontalDirection < 0;

        // Update animator Speed parameter
        float currentSpeed = new Vector2(horizontalMovement, verticalMovement).magnitude;
        animator.SetFloat("Speed", currentSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!canMove)
        {
            horizontalMovement = 0;
            verticalMovement = 0;
        }
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;

    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(!canMove)
        {
            return;
        }

        if (context.performed)
        {
            pauseMenu.PauseGame(!pauseMenu.IsPaused());
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if(!canMove)
        {
            return;
        }
        if (context.started)
        {
            interact = true;
            animator.SetBool("IsInteracting", true);

        }
        else if (context.canceled)
        {
            interact = false;
            animator.SetBool("IsInteracting", false);

        }
    }

    public void DisableMovement()
    {
        canMove = false;
        horizontalMovement = 0;
        verticalMovement = 0;
        animator.SetFloat("Speed", 0f);
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void Die()
    {
        isDead = true;
        canMove = false;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsDead", true);
    }


}
