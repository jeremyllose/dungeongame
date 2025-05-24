using UnityEngine;

public class Collectible : MonoBehaviour

{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
            // Play sound effect
            if (audioManager != null)
            {
                audioManager.PlaySFX(audioManager.SnakeFigureSFX);
            }
        }
    }
}
