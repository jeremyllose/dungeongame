using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPanelUI : MonoBehaviour
{
    public PlayerHealth playerHealth; // drag player GameObject here in inspector

    public void OnReviveClicked()
    {
        playerHealth.ReviveAtCheckpoint();
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu"); // replace with your main menu scene name
    }
}
