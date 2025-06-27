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
    public CardUIController cardUIController;

    public void Setup(CardSO card, int count, bool discovered)
    {
        cardUIController = GetComponentInParent<CardUIController>();
        if (discovered)
        {
            icon.sprite = card.sprite;
            icon.color = Color.black;
            rarityText.text = "<sprite=" + ((int)card.rarity) + ">";
            quantityText.text = "x" + count;
            cardButton.onClick.AddListener(delegate { cardUIController.DisplayCard(card); });
        }
        else
        {
            icon.sprite = questionMarkSprite.sprite;
            rarityText.text = "";
            quantityText.text = "";

        }
    }
}
