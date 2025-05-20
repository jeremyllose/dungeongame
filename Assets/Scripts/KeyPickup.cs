using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Optionally store key possession in player
            FindObjectOfType<LockedDoor>().Unlock();
            Destroy(gameObject);
        }
    }
}
