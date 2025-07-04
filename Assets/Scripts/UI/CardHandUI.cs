using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHandUI : MonoBehaviour
{

    public Image sprite;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text flavour;
    public TMP_Text rarity;
    public Button cardButton;

    public void Setup(CardSO card, BattleController battleController)
    {
        sprite.sprite = card.sprite;
        title.text = card.title;
        description.text = card.description;
        flavour.text = card.flavour;
        rarity.text = "<sprite=" + ((int)card.rarity) + ">";
        cardButton.onClick.AddListener(delegate { battleController.SetPlayerCard(card); });

    }

}
