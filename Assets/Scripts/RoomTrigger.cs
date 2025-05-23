using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            GetComponentInParent<EnemyRoomManager>().TriggerRoom();
            activated = true;
            Destroy(gameObject); // Optional: trigger only once
        }
    }
}
