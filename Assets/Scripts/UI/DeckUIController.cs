using NUnit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DeckUIController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text title;

    public GameObject deckCardPrefab;
    public GameObject decklistPanel;

    public CardManager cardManager;

    public DeckSO selectedDeck;

    public InventoryUI inventoryUI;
    public GameObject removeButton;
    public GameObject addButton;
    public GameObject buttonHeaders;

    public bool editMode = false;
    public GameObject editButton;
    public GameObject saveButton;

    public GameObject cardPrefab;
    public GameObject decksPanel;

    private void Awake()
    {
        inventoryUI = GetComponent<InventoryUI>();
    }
    private void Update()
    {
        removeButton.SetActive(selectedDeck != null && editMode != true);
    }

    public void DisplayDeck(DeckSO deck)
    {
        title.text = deck.title;
        Debug.Log("Entered Method");
        foreach (Transform child in decklistPanel.transform)
        {
            Debug.Log("Destroying children");
            Destroy(child.gameObject);
        }
        Debug.Log("Destroyed Children");
        foreach (CardSO card in deck.GetAllCards())
        {
            Debug.Log("Initializing cards");
            GameObject cardGO = Instantiate(deckCardPrefab, decklistPanel.transform);
            DeckCardUI deckCardUI = cardGO.GetComponent<DeckCardUI>();
            deckCardUI.Setup(deck, card, cardManager, false);
        }
        Debug.Log("Initialized Cards");
        canvas.SetActive(true);
        selectedDeck = deck;
    }

    public void HideDeck()
    {
        canvas.SetActive(false);
        selectedDeck = null;
    }

    public void RemoveSelectedDeck()
    {
        cardManager.GetInventory().RemoveDeck(selectedDeck);
        inventoryUI.DisplayInventory();
        HideDeck();
    }

    public void CreateNewDeck()
    {
        cardManager.GetInventory().AddDeck();
        inventoryUI.DisplayInventory();
    }

    public void EnableEditMode()
    {
        SetHeaderVisibility(false);
        editMode = true;
        editButton.SetActive(false);
        saveButton.SetActive(true);
        title.text = selectedDeck.title;
        Debug.Log("Entered Method");
        foreach (Transform child in decklistPanel.transform)
        {
            Debug.Log("Destroying children");
            Destroy(child.gameObject);
        }
        Debug.Log("Destroyed Children");
        foreach (CardSO card in selectedDeck.GetAllCards())
        {
            Debug.Log("Initializing cards");
            GameObject cardGO = Instantiate(deckCardPrefab, decklistPanel.transform);
            DeckCardUI deckCardUI = cardGO.GetComponent<DeckCardUI>();
            deckCardUI.Setup(selectedDeck, card, cardManager, true);
        }
        Debug.Log("Initialized Cards");

        //Cards
        foreach (Transform child in decksPanel.transform)
        {
            Destroy(child.gameObject);
        }

        Inventory inv = cardManager.GetInventory();
        foreach (CardSO card in inv.GetAllCards())
        {
            int count = inv.GetCardCount(card);
            bool discovered = inv.GetCardDiscovered(card);

            GameObject slotGO = Instantiate(cardPrefab, decksPanel.transform);
            CardInventoryUI cardSlotUI = slotGO.GetComponent<CardInventoryUI>();
            cardSlotUI.EditModeSetup(selectedDeck, card, cardManager, count, discovered, this);
        }
    }

    public void DisableEditMode()
    {
        editMode = false;
        editButton.SetActive(true);
        saveButton.SetActive(false);
        DisplayDeck(selectedDeck);
        SetHeaderVisibility(true);
        inventoryUI.DisplayInventory();
    }

    public void SetHeaderVisibility(bool visible)
    {
        buttonHeaders.SetActive(visible);
        addButton.SetActive(visible);
        removeButton.SetActive(visible);
    }
}
