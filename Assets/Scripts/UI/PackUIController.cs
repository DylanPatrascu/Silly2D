using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PackUIController : MonoBehaviour
{
    public GameObject canvas;
    public Image sprite;
    public TMP_Text title;

    public void DisplayPack(PackSO pack)
    {
        sprite.sprite = pack.sprite;
        title.text = pack.title;
        canvas.SetActive(true);
    }

    public void HidePack()
    {
        canvas.SetActive(false);
    }
}
