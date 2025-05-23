using UnityEngine;

public class PlayerRespawnSystem : MonoBehaviour
{
    public static PlayerRespawnSystem Instance { get; private set; }

    private Vector2 checkpoint;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional for keeping between scenes
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
        Debug.Log("Checkpoint set at: " + checkpoint);
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = checkpoint;
    }
}
