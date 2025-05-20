using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    public GameObject key; // Assign in inspector
    public GameObject[] enemies; // Populate with enemies in Inspector
    private bool roomTriggered = false;

    void Update()
    {
        if (roomTriggered && AllEnemiesDefeated())
        {
            key.SetActive(true);
            roomTriggered = false; // Prevents repeat
        }
    }

    bool AllEnemiesDefeated()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                return false;
        }
        return true;
    }

    public void TriggerRoom()
    {
        roomTriggered = true;
        foreach (var enemy in enemies)
        {
            enemy.SetActive(true); // Wake up or spawn enemies
        }
    }
}
