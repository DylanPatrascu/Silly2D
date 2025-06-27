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

}
