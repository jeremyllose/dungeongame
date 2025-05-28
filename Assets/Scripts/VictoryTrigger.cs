using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [Header("Assign all enemies that must be defeated")]
    [SerializeField] private GameObject[] enemiesToDefeat;

    [Header("Assign the Win Panel GameObject")]
    [SerializeField] private GameObject winPanel;

    public AudioClip VictorySFX;
    private AudioManager audioManager;

    private bool hasWon = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (hasWon) return;

        if (AllEnemiesDefeated())
        {
            hasWon = true;
            ShowWinPanel();
        }
    }

    bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemiesToDefeat)
        {
            if (enemy != null)
                return false;
        }
        return true;
    }

    void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Debug.Log("Victory! All enemies defeated.");

            // Mute the game music
            if (audioManager.gameMusic != null)
                audioManager.gameMusic.mute = true;

            // Play victory SFX
            audioManager.PlaySFX(VictorySFX);

            // Freeze time
            Time.timeScale = 0f;
        }
    }
}
