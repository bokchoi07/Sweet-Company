using UnityEngine;
using UnityEngine.AI;

public class SlimeBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public int damageAmount; // Amount of damage the slime deals to the player

    //Patroling
    public float patrolRange;
    private Vector3 walkPoint;
    private bool walkPointSet;

    //Attacking
    public float attackRange;

    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        // Start patrolling immediately
        Patroling();
        // Calculate base offset
        CalculateBaseOffset();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Stop patrolling and chase the player
            agent.SetDestination(player.position);
        }
        else
        {
            // If not in attack range, patrol
            if (!agent.hasPath || agent.remainingDistance < 0.5f)
            {
                Patroling();
            }
        }
    }
    private void Patroling()
    {
        if (!walkPointSet || agent.remainingDistance < 0.5f)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
    }

    private void SearchWalkPoint()
    {
        // Get a random point within the NavMesh bounds
        Vector3 randomPoint = RandomNavmeshLocation(patrolRange);
        if (randomPoint != Vector3.zero)
        {
            walkPoint = randomPoint;
            agent.SetDestination(walkPoint);
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }
    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }
    private bool isWithinAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    private void AttackPlayer()
    {
        if (!isWithinAttackRange())
        {
            // Only set destination if not already within attack range
            agent.SetDestination(player.position);
        }
        else
        {
            // Optionally, you could add logic here to stop the enemy when within attack range
            agent.ResetPath(); // Stop moving
                               // Add code for attacking the player
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("whatIsPlayer"))
        {
            // Deal damage to the player when colliding
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }

    private void CalculateBaseOffset()
    {
        // Adjust the base offset by the desired difference
        float adjustedOffset = 0.08f;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, whatIsGround))
        {
            float distanceToGround = transform.position.y - hit.point.y - adjustedOffset;
            agent.baseOffset = distanceToGround;
        }
    }
}

