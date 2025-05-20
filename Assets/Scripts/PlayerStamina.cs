using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public int maxStamina = 100;
    private float currentStamina;

    [Header("Regeneration")]
    public float regenRate = 5f; // Stamina per second
    private bool isRunning;

    [Header("UI")]
    public Slider staminaSlider;
    public TMP_Text staminaText;

    [Header("Action Costs")]
    public float runCostPerSecond = 10f; // Drain per second while holding Shift
    public float minStaminaToRun = 10f;  // Minimum stamina needed to start running

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
        if (!isRunning)
        {
            RegenerateStamina();
        }
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += regenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            UpdateStaminaUI();
        }
    }

    public bool CanRun()
    {
        return currentStamina >= minStaminaToRun;
    }

    public void StartRunning()
    {
        isRunning = true;
    }

    public void StopRunning()
    {
        isRunning = false;
    }

    public bool DrainRunStamina()
    {
        float drainAmount = runCostPerSecond * Time.deltaTime;

        if (currentStamina >= drainAmount)
        {
            currentStamina -= drainAmount;
            UpdateStaminaUI();
            return true;
        }
        else
        {
            currentStamina = Mathf.Max(0, currentStamina);
            UpdateStaminaUI();
            return false;
        }
    }

    private void UpdateStaminaUI()
    {
        if (staminaSlider != null) staminaSlider.value = currentStamina;
        if (staminaText != null) staminaText.text = $"{Mathf.FloorToInt(currentStamina)} / {maxStamina}";
    }
}
