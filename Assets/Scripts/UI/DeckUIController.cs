using TMPro;
using UnityEngine;

public class DeckUIController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text title;

    public GameObject deckCardPrefab;
    public GameObject deckPanel;

    public CardManager cardManager;

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
            deckCardUI.Setup(deck, card, cardManager);
        }
        Debug.Log("Initialized Cards");
    }

    public void HideDeck()
    {
        canvas.SetActive(false);
    }
}
