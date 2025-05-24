using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 5f;
    public GameObject hitEffect; // Optional: effect when hitting player

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Auto-destroy after time
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
