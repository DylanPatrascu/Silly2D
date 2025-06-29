using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject cardDisplay;
    private bool isPaused;
    [SerializeField] private InventoryUI inventoryUI;

    private void Start()
    {
        PauseGame(false);
    }

    public void PauseGame(bool paused)
    {
        inventoryUI.DisplayInventory();
        pauseCanvas.SetActive(paused);
        cardDisplay.SetActive(false);
        Cursor.visible = paused;
        isPaused = paused;
        Time.timeScale = paused ? 0 : 1;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

}
