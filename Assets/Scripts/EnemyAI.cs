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

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (PlayerInSight())
        {
            if (distanceToPlayer <= detectionRadius)
                chasingPlayer = true;
        }

        if (distanceToPlayer > chaseDistance)
        {
            chasingPlayer = false;
        }

        if (chasingPlayer)
        {
            MoveTowards(player.position);
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

        if (hit.collider != null)
        {
            return hit.collider.CompareTag("Player");
        }

        return false;
    }

    void MoveTowards(Vector2 target)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void Patrol()
    {
        Transform patrolTarget = patrolPoints[patrolIndex];
        MoveTowards(patrolTarget.position);

        if (Vector2.Distance(transform.position, patrolTarget.position) < 0.2f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
