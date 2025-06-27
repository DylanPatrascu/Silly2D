using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseCanvas;
    private bool isPaused;

    private void Start()
    {
        PauseGame(false);
    }

    public void PauseGame(bool paused)
    {
        pauseCanvas.SetActive(paused);
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
