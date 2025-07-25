using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerEnergy playerEnergy;

    private PlayerExperience playerXP;

    [Header("Attack Settings")]
    public Transform attackPoint;       // Assign in inspector (child object near weapon)
    public float attackRange = 1f;
    public LayerMask enemyLayer;

    public int attack1Damage = 10;
    public int attack2Damage = 15;
    public int attack3Damage = 20;
    public int specialDamage = 30;
    public AudioClip ATK1SFX;
    public AudioClip ATK2SFX;
    public AudioClip ATK3SFX;
    public AudioClip ATK4SFX;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        playerEnergy = GetComponent<PlayerEnergy>();
        playerXP = GetComponent<PlayerExperience>();
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
        // Level requirements: 1 = C, 2 = E, 3 = Q, 4 = R
        int requiredLevel = attackNumber;

        if (!playerXP.CanUseAttack(requiredLevel))
        {
            string message = $"Requires Level {requiredLevel} to use this attack.";
            Debug.Log(message);
            PlayerLogManager.Instance.Log(message);
            return;
        }

        int cost = playerEnergy.GetAttackCost(attackNumber);

        if (playerEnergy.TryUseEnergy(cost))
        {
            switch (attackNumber)
            {
                case 1:
                    animator.SetTrigger("Attack1");
                    if (audioManager != null)
                        audioManager.PlaySFX(audioManager.ATK1SFX); // Replace with specific SFX if available
                    break;

                case 2:
                    animator.SetTrigger("Attack2");
                    if (audioManager != null)
                        audioManager.PlaySFX(audioManager.ATK2SFX); // Replace with specific SFX if available
                    break;

                case 3:
                    animator.SetTrigger("Attack3");
                    if (audioManager != null)
                        audioManager.PlaySFX(audioManager.ATK3SFX); // Replace with specific SFX if available
                    break;

                case 4:
                    animator.SetTrigger("Special");
                    if (audioManager != null)
                        audioManager.PlaySFX(audioManager.ATK4SFX); // Replace with special SFX
                    break;
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
