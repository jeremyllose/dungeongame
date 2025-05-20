using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private bool isUnlocked = false;

    public void Unlock()
    {
        isUnlocked = true;
        GetComponent<Collider2D>().enabled = false; // Remove the block
        GetComponent<Animator>()?.SetTrigger("Open"); // Optional animation
    }
}
