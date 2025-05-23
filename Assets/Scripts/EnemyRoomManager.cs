using System.Collections;
using UnityEngine;

public class EnemyRoomManager : MonoBehaviour
{
    public LockedDoor door;
    public GameObject key;
    public GameObject[] enemies;

    private bool triggered = false;

    void Update()
    {
        if (triggered && AllEnemiesDefeated())
        {
            key.SetActive(true);
            triggered = false;
        }
    }

    public void TriggerRoom()
    {
        if (!triggered)
            StartCoroutine(StartRoomAfterDelay());
    }

    private IEnumerator StartRoomAfterDelay()
    {
        triggered = true;

        yield return new WaitForSeconds(3.0f); // ⏱ Delay in seconds before door locks

        door.Lock(); // Now lock the door after the delay
    }
    private bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
                return false;
        }
        return true;
    }
}
