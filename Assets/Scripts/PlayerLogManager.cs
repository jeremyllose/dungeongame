using TMPro;
using UnityEngine;

public class PlayerLogManager : MonoBehaviour
{
    public static PlayerLogManager Instance { get; private set; }

    public TextMeshProUGUI logText;
    private float logDuration = 3f;
    private float logTimer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (logText != null && logText.enabled)
        {
            logTimer -= Time.deltaTime;
            if (logTimer <= 0)
                logText.enabled = false;
        }
    }

    public void Log(string message)
    {
        if (logText == null) return;

        logText.text = message;
        logText.enabled = true;
        logTimer = logDuration;
    }
}
