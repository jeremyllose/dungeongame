using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private BoxCollider2D doorCollider;
    private Animator animator;

    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        // Disable collider at start so the door is open
        doorCollider.enabled = false;
    }

    public void Lock()
    {
        doorCollider.enabled = true;
        animator?.SetTrigger("Lock");
    }

    public void Unlock()
    {
        doorCollider.enabled = false;
        animator?.SetTrigger("Open");
    }
}
