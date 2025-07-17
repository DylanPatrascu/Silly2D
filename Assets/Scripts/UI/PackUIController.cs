using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PackUIController : MonoBehaviour
{
    public GameObject canvas;
    public Image sprite;
    public TMP_Text title;
    [SerializeField] private Button openButton;
    [SerializeField] private PackManager packManager;

    public void DisplayPack(PackSO pack)
    {
        sprite.sprite = pack.sprite;
        title.text = pack.title;
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(delegate { packManager.OpenPack(pack); });
        canvas.SetActive(true);
    }

    public void HidePack()
    {
        canvas.SetActive(false);
    }
}
