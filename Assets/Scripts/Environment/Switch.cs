using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool canInteract;
    private bool activated = false;
    private PlayerMovement nearbyPlayerMovement;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D collision;

    [SerializeField] private Door linkedDoor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (activated || !canInteract || nearbyPlayerMovement == null)
            return;

        if (nearbyPlayerMovement.interact)
        {
            nearbyPlayerMovement.interact = false;

            Debug.Log("Switch Activated");
            spriteRenderer.enabled = false;
            collision.enabled = false;
            activated = true;

            if (linkedDoor != null)
                linkedDoor.OpenDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearbyPlayerMovement = other.GetComponent<PlayerMovement>();
            canInteract = nearbyPlayerMovement != null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>() == nearbyPlayerMovement)
            {
                nearbyPlayerMovement = null;
                canInteract = false;
            }
        }
    }

    public bool GetActivated()
    {
        return activated;
    }
}
