using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackInventoryUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text quantityText;
    public Image questionMarkSprite;
    public Button packButton;
    public void Setup(PackSO pack, int count, bool discovered, PackUIController packUI)
    {
        if (discovered)
        {
            icon.sprite = pack.sprite;
            icon.color = Color.black;
            quantityText.text = "x" + count;
            packButton.onClick.AddListener(delegate { packUI.DisplayPack(pack); });
        }
        else
        {
            icon.sprite = questionMarkSprite.sprite;
            quantityText.text = "";

        }
    }
}
