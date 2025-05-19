using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerEnergy playerEnergy;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerEnergy = GetComponent<PlayerEnergy>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            TryAttack(1);

        if (Input.GetKeyDown(KeyCode.E))
            TryAttack(2);

        if (Input.GetKeyDown(KeyCode.Q))
            TryAttack(3);

        if (Input.GetKeyDown(KeyCode.R))
            TryAttack(4);  // Special attack
    }

    private void TryAttack(int attackNumber)
    {
        int cost = playerEnergy.GetAttackCost(attackNumber);

        if (playerEnergy.TryUseEnergy(cost))
        {
            // Enough energy, do attack animation
            switch (attackNumber)
            {
                case 1: animator.SetTrigger("Attack1"); break;
                case 2: animator.SetTrigger("Attack2"); break;
                case 3: animator.SetTrigger("Attack3"); break;
                case 4: animator.SetTrigger("Special"); break;
            }
        }
        else
        {
            // Not enough energy — could add a "fail" sound or UI feedback here
            Debug.Log("Not enough energy for attack " + attackNumber);
        }
    }
}
