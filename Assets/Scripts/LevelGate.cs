using UnityEngine;

public class LevelGate : MonoBehaviour
{
    public int requiredLevel = 3;
    public GameObject doorBlocker;

    private PlayerExperience playerXP;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerXP = player.GetComponent<PlayerExperience>();
    }

    void Update()
    {
        if (playerXP != null)
        {
            doorBlocker.SetActive(playerXP.currentLevel < requiredLevel);
        }
    }
}
