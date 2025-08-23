using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject cardSlotPrefab;
    public GameObject panel;

    public GameObject packSlotPrefab;
    public GameObject packPanel;

    public GameObject deckSlotPrefab;
    public GameObject deckPanel;

    [SerializeField] private CardManager cardManager;
    private Inventory inv;

    private PackUIController packUI;
    private CardUIController cardUI;

    private void Awake()
    {
        packUI = GetComponent<PackUIController>();
        cardUI = GetComponent<CardUIController>();
        inv = cardManager.GetInventory();
        if(inv == null)
        {
            Debug.Log("inventory is null");
        }
        DisplayInventory();
    }

    public void DisplayInventory()
    {
        //Cards
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (CardSO card in inv.GetAllCards())
        {
            int count = inv.GetCardCount(card);
            bool discovered = inv.GetCardDiscovered(card);

            GameObject slotGO = Instantiate(cardSlotPrefab, panel.transform);
            CardInventoryUI cardSlotUI = slotGO.GetComponent<CardInventoryUI>();
            cardSlotUI.Setup(card, count, discovered, cardUI);
        }

        //Packs
        foreach (Transform child in packPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (PackSO pack in inv.GetAllPacks())
        {
            int count = inv.GetPackCount(pack);
            bool discovered = inv.GetPackDiscovered(pack);

            GameObject slotGO = Instantiate(packSlotPrefab, packPanel.transform);
            PackInventoryUI packSlotUI = slotGO.GetComponent<PackInventoryUI>();
            packSlotUI.Setup(pack, count, discovered, packUI);
        }

        //Decks
        foreach (Transform child in deckPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
