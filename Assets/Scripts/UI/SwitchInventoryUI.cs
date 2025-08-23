using UnityEngine;

public class SwitchInventoryUI : MonoBehaviour
{
    public GameObject cardCanvas;
    public GameObject packCanvas;
    public GameObject deckCanvas;

    public void EnablePackCanvas()
    {
        packCanvas.SetActive(true);
        cardCanvas.SetActive(false);
        deckCanvas.SetActive(false);
    }
    public void EnableCardCanvas()
    {
        cardCanvas.SetActive(true);
        packCanvas.SetActive(false);
        deckCanvas.SetActive(false);
    }

    public void EnableDeckCanvas()
    {
        deckCanvas.SetActive(true);
        cardCanvas.SetActive(false);
        packCanvas.SetActive(false);
    }
}
