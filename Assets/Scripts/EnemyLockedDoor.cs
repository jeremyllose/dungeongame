using UnityEngine;

public class EnemyLockedDoor : MonoBehaviour
{
    public enum UnlockMode { Manual, EnemyClear }
    public UnlockMode unlockMode = UnlockMode.Manual;

    private BoxCollider2D doorCollider;
    private Animator animator;
    private bool isLocked = true;

    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        if (unlockMode == UnlockMode.EnemyClear)
            Lock(); // Start locked
        else
            doorCollider.enabled = false; // Start unlocked
    }

    public void Lock()
    {
        isLocked = true;
        doorCollider.enabled = true;
        animator?.SetTrigger("Lock");
    }

    public void Unlock()
    {
        if (!isLocked) return;

        isLocked = false;
        doorCollider.enabled = false;
        animator?.SetTrigger("Open");
    }

    public void OnEnemiesCleared()
    {
        if (unlockMode == UnlockMode.EnemyClear)
            Unlock();
    }
}
