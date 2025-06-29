using UnityEngine;

public class Chest : MonoBehaviour
{
    public CardSO[] cardRewards;
    public PackSO[] packRewards;
    public bool canInteract;
    public Sprite openedSprite;
    public CardManager cardManager;
    private SpriteRenderer spriteRenderer;
    public bool opened = false;
    public Inventory inventory;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(opened)
        {
            return;
        }
        if(canInteract)
        {
            //check if interact is pressed
            Debug.Log("Chest Opened");
            ClaimRewards();
            spriteRenderer.sprite = openedSprite;
            opened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cardManager = other.gameObject.GetComponent<CardManager>();
            inventory = cardManager.GetInventory();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cardManager = null;
            inventory = null;
            canInteract = false;
        }
    }

    public void ClaimRewards()
    {
        for(int i = 0; i < cardRewards.Length; i++)
        {
            inventory.AddCard(cardRewards[i]);
        }

        for (int i = 0; i < packRewards.Length; i++)
        {
            inventory.AddPack(packRewards[i]);
        }
    }
}
