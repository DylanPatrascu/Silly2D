using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeckUIController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text title;

    public GameObject deckCardPrefab;
    public GameObject deckPanel;

    public CardManager cardManager;

    public DeckSO selectedDeck;

    public InventoryUI inventoryUI;
    public GameObject removeButton;
    public GameObject addButton;
    public GameObject buttonHeaders;

    public bool editMode = false;
    public GameObject editButton;
    public GameObject saveButton;

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
        foreach (Transform child in deckPanel.transform)
        {
            Debug.Log("Destroying children");
            Destroy(child.gameObject);
        }
        Debug.Log("Destroyed Children");
        foreach (CardSO card in deck.GetAllCards())
        {
            Debug.Log("Initializing cards");
            GameObject cardGO = Instantiate(deckCardPrefab, deckPanel.transform);
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
        foreach (Transform child in deckPanel.transform)
        {
            Debug.Log("Destroying children");
            Destroy(child.gameObject);
        }
        Debug.Log("Destroyed Children");
        foreach (CardSO card in selectedDeck.GetAllCards())
        {
            Debug.Log("Initializing cards");
            GameObject cardGO = Instantiate(deckCardPrefab, deckPanel.transform);
            DeckCardUI deckCardUI = cardGO.GetComponent<DeckCardUI>();
            deckCardUI.Setup(selectedDeck, card, cardManager, true);
        }
        Debug.Log("Initialized Cards");
    }

    public void DisableEditMode()
    {
        editMode = false;
        editButton.SetActive(true);
        saveButton.SetActive(false);
        DisplayDeck(selectedDeck);
        SetHeaderVisibility(true);
    }

    public void SetHeaderVisibility(bool visible)
    {
        buttonHeaders.SetActive(visible);
        addButton.SetActive(visible);
        removeButton.SetActive(visible);
    }
}
