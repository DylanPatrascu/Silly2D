using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Key key;
    public bool hasKey;
    public bool canInteract;
    public bool opened = false;
    public Sprite openedSprite;
    public string nextRoom;

    private PlayerMovement nearbyPlayerMovement;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!canInteract || nearbyPlayerMovement == null)
            return;

        if (nearbyPlayerMovement.interact)
        {
            nearbyPlayerMovement.interact = false;

            if (!opened) // first press: unlock
            {
                if (key.GetCollected())
                {
                    Debug.Log("Key Collected, Door Unlocked!");
                    spriteRenderer.sprite = openedSprite;
                    opened = true;
                }
                else
                {
                    Debug.Log("Door is locked, need a key.");
                }
            }
            else // door is already unlocked, enter room
            {
                Debug.Log("Entering next room: " + nextRoom);
                LoadScene(nextRoom);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nearbyPlayerMovement = other.GetComponent<PlayerMovement>();
            canInteract = nearbyPlayerMovement != null;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>() == nearbyPlayerMovement)
            {
                nearbyPlayerMovement = null;
                canInteract = false;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
