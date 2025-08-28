using TMPro;
using UnityEngine;

public class DeckUIController : MonoBehaviour
{
    public GameObject canvas;
    public TMP_Text title;

    public void DisplayDeck(DeckSO deck)
    {
        title.text = deck.title;
    }

    public void HideDeck()
    {
        canvas.SetActive(false);
    }
}
