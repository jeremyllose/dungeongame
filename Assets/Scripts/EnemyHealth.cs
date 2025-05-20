using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    public GameObject healthUI;
    public Slider healthSlider;
    public Transform player;
    public float uiDisplayDistance = 5f;

    void Start()
    {
        currentHP = maxHP;
        UpdateHealthUI();
        healthUI.SetActive(false);
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        healthUI.SetActive(distance <= uiDisplayDistance);
    }
    public void TakeDamage(int amount)
    {
        Debug.Log($"Enemy took {amount} damage");  // <- Add this line
        currentHP -= amount;
        currentHP = Mathf.Max(0, currentHP);
        UpdateHealthUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = (float)currentHP / maxHP;
    }

    void Die()
    {
        Destroy(gameObject); // or trigger death animation
    }
}
