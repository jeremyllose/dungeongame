using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public int maxStamina = 100;
    private int currentStamina;

    [Header("Regeneration")]
    public float regenRate = 5f; // Stamina per second

    [Header("UI")]
    public Slider staminaSlider;
    public TMP_Text staminaText;

    [Header("Action Costs")]
    public int runCost = 1;       // Cost per second while running
    public int dashCost = 20;     // One-time cost
    public int rollCost = 15;     // One-time cost

    private void Start()
    {
        currentStamina = maxStamina;

        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        UpdateStaminaUI();
    }

    private void Update()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += Mathf.CeilToInt(regenRate * Time.deltaTime);
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            UpdateStaminaUI();
        }
    }

    public bool TryUseStamina(int cost)
    {
        if (currentStamina >= cost)
        {
            currentStamina -= cost;
            UpdateStaminaUI();
            return true;
        }
        return false;
    }

    public bool CanUseStamina(int cost)
    {
        return currentStamina >= cost;
    }

    private void UpdateStaminaUI()
    {
        if (staminaSlider != null) staminaSlider.value = currentStamina;
        if (staminaText != null) staminaText.text = $"{currentStamina} / {maxStamina}";
    }

    public int GetCurrentStamina()
    {
        return currentStamina;
    }
}
