using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerEnergy playerEnergy;

    [Header("Attack Settings")]
    public Transform attackPoint;       // Assign in inspector (child object near weapon)
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    public int attack1Damage = 10;
    public int attack2Damage = 15;
    public int attack3Damage = 20;
    public int specialDamage = 30;
    

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
            Debug.Log("Not enough energy for attack " + attackNumber);
        }
    }

    // Called by animation events
    public void OnAttack1Hit()
    {
        DealDamage(attack1Damage);
    }

    public void OnAttack2Hit()
    {
        DealDamage(attack2Damage);
    }

    public void OnAttack3Hit()
    {
        DealDamage(attack3Damage);
    }

    public void OnSpecialHit()
    {
        DealDamage(specialDamage);
    }

    public void DealDamage(int damage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            if (eh == null)
                eh = enemy.GetComponentInParent<EnemyHealth>();

            if (eh != null)
            {
                Debug.Log($"Damaging {enemy.name} for {damage}");
                eh.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
