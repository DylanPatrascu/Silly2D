using UnityEngine;

public class Chest : MonoBehaviour
{
    public CardSO[] cardRewards;
    public PackSO[] packRewards;
    public bool canInteract;
    public bool opened = false;

    private CardManager cardManager;
    private Inventory inventory;
    private PlayerMovement nearbyPlayerMovement;
    public Animator animator;
    public Key requiredKey;


    void Update()
    {
        if (opened || !canInteract || nearbyPlayerMovement == null)
            return;

        if (requiredKey != null && !requiredKey.GetCollected())
        {
            Debug.Log("Chest is locked. You need a key!");
            return;
        }

        if (nearbyPlayerMovement.interact)
        {
            nearbyPlayerMovement.interact = false;

            Debug.Log("Chest Opened");
            ClaimRewards();
            animator.SetBool("Opened", true);
            opened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cardManager = other.GetComponent<CardManager>();
            inventory = cardManager?.GetInventory();
            nearbyPlayerMovement = other.GetComponent<PlayerMovement>();
            canInteract = cardManager != null && nearbyPlayerMovement != null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>() == nearbyPlayerMovement)
            {
                cardManager = null;
                inventory = null;
                nearbyPlayerMovement = null;
                canInteract = false;
            }
        }
    }

    public void ClaimRewards()
    {
        if (inventory == null) return;

        foreach (var card in cardRewards)
        {
            inventory.AddCard(card);
        }

        foreach (var pack in packRewards)
        {
            inventory.AddPack(pack);
        }
    }
}
