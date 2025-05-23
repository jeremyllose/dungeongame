using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healAmount = 10;
    public float healInterval = 1f;
    private bool playerInZone = false;
    private float timer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private void Update()
    {
        if (playerInZone)
        {
            timer += Time.deltaTime;
            if (timer >= healInterval)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                PlayerHealth health = player.GetComponent<PlayerHealth>();

                if (health != null && health.GetCurrentHealth() < health.GetMaxHealth())
                {
                    health.Heal(healAmount);
                }

                timer = 0f;
            }
        }
    }
}
