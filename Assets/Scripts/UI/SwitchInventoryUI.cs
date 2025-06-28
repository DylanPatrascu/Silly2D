using UnityEngine;

public class SwitchInventoryUI : MonoBehaviour
{
    public GameObject cardCanvas;
    public GameObject packCanvas;

    public void EnablePackCanvas()
    {
        packCanvas.SetActive(true);
        cardCanvas.SetActive(false);
    }
    public void EnableCardCanvas()
    {
        cardCanvas.SetActive(true);
        packCanvas.SetActive(false);
    }
}
