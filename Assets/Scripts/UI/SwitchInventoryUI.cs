using UnityEngine;

public class SwitchInventoryUI : MonoBehaviour
{
    public GameObject cardCanvas;
    public GameObject packCanvas;
    public GameObject deckCanvas;

    public DeckUIController deckUI;
    public PackUIController packUI;
    public CardUIController cardUI;

    private void Awake()
    {
        deckUI = GetComponent<DeckUIController>();
        packUI = GetComponent<PackUIController>();
        cardUI = GetComponent<CardUIController>();
    }
    public void EnablePackCanvas()
    {
        packCanvas.SetActive(true);
        cardCanvas.SetActive(false);
        deckCanvas.SetActive(false);
        HideInspects();
    }
    public void EnableCardCanvas()
    {
        cardCanvas.SetActive(true);
        packCanvas.SetActive(false);
        deckCanvas.SetActive(false);
        HideInspects();
    }

    public void EnableDeckCanvas()
    {
        deckCanvas.SetActive(true);
        cardCanvas.SetActive(false);
        packCanvas.SetActive(false);
        HideInspects();
    }

    public void HideInspects()
    {
        deckUI.HideDeck();
        packUI.HidePack();
        cardUI.HideCard();
    }
}
