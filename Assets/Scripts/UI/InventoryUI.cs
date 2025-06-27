using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject cardSlotPrefab; // Assign in Inspector
    public GameObject panel; // Your GridLayoutGroup container

    public void DisplayInventory(Inventory inventory)
    {
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (CardSO card in inventory.GetAllCards())
        {
            int count = inventory.GetCardCount(card);
            bool discovered = inventory.GetCardDiscovered(card);

            GameObject slotGO = Instantiate(cardSlotPrefab, panel.transform);
            CardInventoryUI cardSlotUI = slotGO.GetComponent<CardInventoryUI>();
            cardSlotUI.Setup(card, count, discovered);
        }
    }
}
