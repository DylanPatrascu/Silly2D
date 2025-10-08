using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    private bool opened = false;

    public void OpenDoor()
    {
        if (opened) return;

        Debug.Log("Door Opened!");
        animator.SetBool("Opened", true);
        opened = true;
    }
}
