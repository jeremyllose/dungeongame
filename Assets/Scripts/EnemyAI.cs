using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float chaseDistance = 8f;
    public LayerMask wallLayer;

    public Transform[] patrolPoints;
    private int patrolIndex = 0;

    public float speed = 2f;
    private bool chasingPlayer = false;

    private Animator animator;

    public GameObject swordHitbox; // Assign in inspector

    public void EnableHitbox()
    {
        swordHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        swordHitbox.SetActive(false);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (PlayerInSight() && distanceToPlayer <= detectionRadius)
        {
            chasingPlayer = true;
        }

        if (distanceToPlayer > chaseDistance)
        {
            chasingPlayer = false;
        }

        // Update animator detect state
        animator.SetBool("Detect", chasingPlayer);

        if (chasingPlayer)
        {
            MoveTowards(player.position);
            animator.SetFloat("Speed", speed); // Trigger Walk animation
        }
        else
        {
            Patrol();
        }

        // Optionally trigger attack when very close
        if (chasingPlayer && distanceToPlayer < 1.5f)
        {
            animator.SetTrigger("Attack");
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
        animator.SetFloat("Speed", patrolSpeed); // Trigger Walk or Idle

        if (Vector2.Distance(transform.position, patrolTarget.position) < 0.2f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }
}
