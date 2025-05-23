using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public LockedDoor door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            door.Unlock();
            Destroy(gameObject);
        }
    }
}
