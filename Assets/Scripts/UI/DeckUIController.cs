using TMPro;
using UnityEngine;

public class DeckUIController : MonoBehaviour
{
    public TMP_Text title;

    public void DisplayDeck(DeckSO deck)
    {
        title.text = deck.title;
        
    }
}
