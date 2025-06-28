using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject cardSlotPrefab;
    public GameObject panel;

    public GameObject packSlotPrefab;
    public GameObject packPanel;

    public void DisplayInventory(Inventory inventory)
    {
        //Cards
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (CardSO card in inventory.GetAllCards())
        {
            Debug.Log($"Card: {card.title}, Count: {inventory.GetCardCount(card)}, Discovered: {inventory.GetCardDiscovered(card)}");
        }

        foreach (CardSO card in inventory.GetAllCards())
        {
            int count = inventory.GetCardCount(card);
            bool discovered = inventory.GetCardDiscovered(card);

            GameObject slotGO = Instantiate(cardSlotPrefab, panel.transform);
            CardInventoryUI cardSlotUI = slotGO.GetComponent<CardInventoryUI>();
            cardSlotUI.Setup(card, count, discovered);
        }

        //Packs
        foreach (Transform child in packPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PackSO pack in inventory.GetAllPacks())
        {
            int count = inventory.GetPackCount(pack);
            bool discovered = inventory.GetPackDiscovered(pack);

            GameObject slotGO = Instantiate(packSlotPrefab, packPanel.transform);
            PackInventoryUI packSlotUI = slotGO.GetComponent<PackInventoryUI>();
            packSlotUI.Setup(pack, count, discovered);
        }
    }
}
