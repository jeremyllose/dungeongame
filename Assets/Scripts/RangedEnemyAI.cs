using UnityEngine;

public class RangedEnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 6f;
    public float chaseDistance = 8f;
    public float attackRange = 5f;
    public LayerMask wallLayer;

    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    public float speed = 2f;
    private bool chasingPlayer = false;

    private Animator animator;

    public GameObject projectilePrefab; // Assign your fireball/arrow prefab here
    public Transform firePoint;         // Empty child object to set projectile spawn position
    public float attackCooldown = 2f;
    private float lastAttackTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
        if (projectilePrefab == null)
            Debug.LogError("Projectile prefab is NOT assigned! Please assign it in the Inspector.");

    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (PlayerInSight() && distanceToPlayer <= detectionRadius)
            chasingPlayer = true;

        if (distanceToPlayer > chaseDistance)
            chasingPlayer = false;

        animator.SetBool("Detect", chasingPlayer);

        if (chasingPlayer)
        {
            if (distanceToPlayer <= attackRange)
            {
                // Stop to attack
                animator.SetFloat("Speed", 0);
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    animator.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                MoveTowards(player.position);
                animator.SetFloat("Speed", speed);
            }
        }
        else
        {
            Patrol();
        }
    }

    bool PlayerInSight()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, wallLayer | LayerMask.GetMask("Player"));
        return hit.collider != null && hit.collider.CompareTag("Player");
    }

    void MoveTowards(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void Patrol()
    {
        Transform patrolTarget = patrolPoints[patrolIndex];
        MoveTowards(patrolTarget.position);

        float patrolSpeed = Vector2.Distance(transform.position, patrolTarget.position) > 0.1f ? speed : 0f;
        animator.SetFloat("Speed", patrolSpeed);

        if (Vector2.Distance(transform.position, patrolTarget.position) < 0.2f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    // This method should be called as an animation event during the attack animation
    public void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 8f; // Set projectile speed
        }
    }
}
