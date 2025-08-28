using UnityEngine;
using UnityEngine.UI;

public class DeckInventoryUI : MonoBehaviour
{
    public Button deckButton;
    public void Setup(DeckSO deck, DeckUIController deckUI)
    {
        deckButton.onClick.AddListener(delegate { deckUI.DisplayDeck(deck); });
    }
}
