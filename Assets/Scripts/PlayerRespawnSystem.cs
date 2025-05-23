using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    public static PlayerRespawnSystem Instance { get; private set; }

    private Vector2 checkpoint;
    private bool hasCheckpoint = false; // track if checkpoint is set
    private Vector2 defaultSpawnPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Set default spawn to current position of player at start
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            defaultSpawnPosition = player.transform.position;
        }
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
        hasCheckpoint = true;
        Debug.Log("Checkpoint set at: " + checkpoint);
    }

    public void RespawnPlayer(GameObject player)
    {
        Vector2 targetPos = hasCheckpoint ? checkpoint : defaultSpawnPosition;
        Debug.Log("Respawning player at: " + targetPos);
        player.transform.position = targetPos;
    }
}
