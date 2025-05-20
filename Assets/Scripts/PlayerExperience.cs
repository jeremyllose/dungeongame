using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public int currentXP = 0;
    public int currentLevel = 1;

    public int[] xpThresholds = { 0, 100, 200, 400 }; // XP needed for levels 1–4
    public Slider xpSlider;
    public TMP_Text levelText;

    private PlayerMovement movement;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        CheckLevelUp();
        UpdateUI();
    }

    private void CheckLevelUp()
    {
        while (currentLevel < xpThresholds.Length && currentXP >= xpThresholds[currentLevel])
        {
            currentLevel++;
            Debug.Log($"Level Up! You are now level {currentLevel}");
        }
    }

    private void UpdateUI()
    {
        int currentThreshold = currentLevel < xpThresholds.Length ? xpThresholds[currentLevel] : xpThresholds[^1];
        int previousThreshold = currentLevel > 1 ? xpThresholds[currentLevel - 1] : 0;

        xpSlider.maxValue = currentThreshold - previousThreshold;
        xpSlider.value = currentXP - previousThreshold;

        levelText.text = $"Lv {currentLevel}";
    }

    public bool CanUseAttack(int levelRequired)
    {
        return currentLevel >= levelRequired;
    }
}
