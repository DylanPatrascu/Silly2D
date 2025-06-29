using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text quantityText;
    public TMP_Text rarityText;
    public Image questionMarkSprite;
    public Button cardButton;

    public void Setup(CardSO card, int count, bool discovered, CardUIController cardUI)
    {
        if (discovered)
        {
            icon.sprite = card.sprite;
            icon.color = Color.black;
            rarityText.text = "<sprite=" + ((int)card.rarity) + ">";
            quantityText.text = "x" + count;
            cardButton.onClick.AddListener(delegate { cardUI.DisplayCard(card); });
        }
        else
        {
            icon.sprite = questionMarkSprite.sprite;
            rarityText.text = "";
            quantityText.text = "";

        }
    }
}
