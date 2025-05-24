using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    public GameObject deathPanel; // assign in inspector
    private Animator animator;
    private SpriteRenderer sr;

    public AudioClip HurtSFX;
    public AudioClip DeathSFX;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        PlayerLogManager.Instance.Log("You took " + amount + " damage.");

        if (currentHealth <= 0 || isInvincible)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            // Play hurt SFX
            if (audioManager != null)
                audioManager.PlaySFX(audioManager.HurtSFX);

            StartCoroutine(HandleIFrames());
        }
        else
        {
            currentHealth = 0;
            animator.SetTrigger("Dead");

            // Play death SFX
            if (audioManager != null)
                audioManager.PlaySFX(audioManager.DeathSFX);

            // Disable movement
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;

            // Show death panel
            if (deathPanel != null)
                deathPanel.SetActive(true);
        }
    }

    private IEnumerator HandleIFrames()
    {
        isInvincible = true;
        for (int i = 0; i < 2; i++)
        {
            sr.color = Color.white;
            yield return new WaitForSeconds(invincibilityDuration / 4);
            sr.color = Color.red;
            yield return new WaitForSeconds(invincibilityDuration / 4);
        }
        sr.color = Color.white;
        isInvincible = false;
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    public void ReviveAtCheckpoint()
    {
        currentHealth = maxHealth;
        sr.color = Color.white;

        // Trigger animator revive transition
        animator.SetTrigger("Revive");

        // Respawn at checkpoint
        PlayerRespawnSystem.Instance.RespawnPlayer(gameObject);

        // Re-enable controls
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<PlayerAttack>().enabled = true;

        // Hide panel
        if (deathPanel != null)
            deathPanel.SetActive(false);
    }
    public void Heal(int amount)
    {

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Healed: " + amount + ", Current Health: " + currentHealth);
        PlayerLogManager.Instance.Log("You healed for " + amount + ".");


    }


}
