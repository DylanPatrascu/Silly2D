using NUnit.Framework.Constraints;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool canInteract;
    private bool collected = false;
    private PlayerMovement nearbyPlayerMovement;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D collision;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (collected || !canInteract || nearbyPlayerMovement == null)
            return;

        if (nearbyPlayerMovement.interact)
        {
            nearbyPlayerMovement.interact = false;

            Debug.Log("Key Collected");
            spriteRenderer.sprite = null;
            collected = true;
            collision.enabled = false;
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

    public bool GetCollected()
    {
        return collected;
    }
}
