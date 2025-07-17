using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float horizontalMovement;
    public float verticalMovement;
    public SpriteRenderer sprite;

    private float lastHorizontalDirection = 1f;

    public PauseMenuController pauseMenu;

    public bool interact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(horizontalMovement != 0)
        {
            lastHorizontalDirection = horizontalMovement;
        }
        rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
        sprite.flipX = lastHorizontalDirection > 0;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;

    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseMenu.PauseGame(!pauseMenu.IsPaused());
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interact = true;
            Debug.Log("Interact started");
        }
        else if (context.canceled)
        {
            interact = false;
            Debug.Log("Interact canceled");
        }
    }


}
