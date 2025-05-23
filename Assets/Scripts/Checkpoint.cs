using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint triggered at: " + transform.position);
            PlayerRespawnSystem.Instance.SetCheckpoint(transform.position);
        }
    }

}
