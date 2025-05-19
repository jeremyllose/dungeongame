using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEnergy : MonoBehaviour
{
    public int maxEnergy = 100;
    private float currentEnergy;

    public float regenRate = 5f; // Energy per second

    public Slider energySlider;
    public TMP_Text energyText;

    public int attack1Cost = 10;
    public int attack2Cost = 20;
    public int attack3Cost = 30;
    public int specialCost = 50;

    void Start()
    {
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        RegenerateEnergy();
    }

    private void RegenerateEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += regenRate * Time.deltaTime;
            if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
            UpdateEnergyUI();
        }
    }

    private void UpdateEnergyUI()
    {
        energySlider.value = currentEnergy;
        energyText.text = $"{Mathf.FloorToInt(currentEnergy)} / {maxEnergy}";
    }

    public bool TryUseEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            UpdateEnergyUI();
            return true;
        }
        return false;
    }

    public bool CanUseAttack(int attackNumber)
    {
        switch (attackNumber)
        {
            case 1: return currentEnergy >= attack1Cost;
            case 2: return currentEnergy >= attack2Cost;
            case 3: return currentEnergy >= attack3Cost;
            case 4: return currentEnergy >= specialCost;
            default: return false;
        }
    }

    public int GetAttackCost(int attackNumber)
    {
        return attackNumber switch
        {
            1 => attack1Cost,
            2 => attack2Cost,
            3 => attack3Cost,
            4 => specialCost,
            _ => 0
        };
    }
}
