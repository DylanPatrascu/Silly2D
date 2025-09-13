using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardUI : MonoBehaviour
{
    public TMP_Text title;
    public Image sprite;
    public Button button;


    public void Setup(DeckSO deck, CardSO card, CardManager cardManager, bool editMode)
    {
        if(editMode)
        {
            button.onClick.AddListener(delegate { cardManager.GetInventory().RemoveCardFromDeck(deck, card); Destroy(this.gameObject); });
        }
        title.text = card.title;
        sprite.sprite = card.sprite;
    }
}
