using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float invincibilityDuration = 1f;
    private bool isInvincible = false;

    private Animator animator;
    private SpriteRenderer sr;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0 || isInvincible)
            return;

        currentHealth -= amount;

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(HandleIFrames());
        }
        else
        {
            currentHealth = 0;
            animator.SetTrigger("Dead");
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private IEnumerator HandleIFrames()
    {
        isInvincible = true;

        // Blink white effect
        for (int i = 0; i < 2; i++) // Blink twice
        {
            sr.color = Color.white;
            yield return new WaitForSeconds(invincibilityDuration / 4);
            sr.color = Color.red; // Or normal color
            yield return new WaitForSeconds(invincibilityDuration / 4);
        }

        sr.color = Color.white; // Reset color
        isInvincible = false;
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
    