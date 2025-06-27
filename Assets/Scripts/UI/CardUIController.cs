using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{
    public GameObject canvas;
    public Image sprite;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text flavour;
    public TMP_Text rarity;

    public void DisplayCard(CardSO card)
    {
        sprite.sprite = card.sprite;
        title.text = card.title;
        description.text = card.description;
        flavour.text = card.flavour;
        rarity.text = "<sprite=" + ((int)card.rarity) + ">";
        canvas.SetActive(true);
    }

    public void HideCard()
    {
        canvas.SetActive(false);
    }

}
