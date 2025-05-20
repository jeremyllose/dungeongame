using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            GetComponentInParent<EnemyRoomManager>().TriggerRoom();
            Destroy(gameObject); // One-time trigger
        }
    }
}
