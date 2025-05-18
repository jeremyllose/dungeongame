using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Slider healthSlider;
    public TMP_Text healthText;


    void Update()
    {
        int current = playerHealth.GetCurrentHealth();
        int max = playerHealth.GetMaxHealth();

        healthSlider.maxValue = max;
        healthSlider.value = current;
        healthText.text = $"{current} / {max}";
    }
}
