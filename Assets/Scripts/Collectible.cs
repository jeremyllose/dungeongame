using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int xpValue = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExperience xp = other.GetComponent<PlayerExperience>();
            if (xp != null)
            {
                xp.AddXP(xpValue);
                Destroy(gameObject);
            }
        }
    }
}
