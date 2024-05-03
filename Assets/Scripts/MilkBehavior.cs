using UnityEngine;
using UnityEngine.AI;

public class MilkBehavior : MonoBehaviour
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

    //Scare
    public float scareRange;
    public GameObject transformedObjectPrefab;
    public float transformedSpeedMultiplier;

    private GameObject originalObject;
    private bool isTransformed = false;
    private GameObject transformedInstance;

    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        originalObject = gameObject;
        // Start patrolling immediately
        Patroling();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Stop patrolling and chase the player
            agent.SetDestination(player.position);
            RotateToPlayer();
        }
        else
        {
            // If not in attack range, patrol
            if (!agent.hasPath || agent.remainingDistance < 0.5f)
            {
                Patroling();
            }
        }

        // Check if the player is within scare range and the object is not already transformed
        if (!isTransformed && Vector3.Distance(transform.position, player.position) <= scareRange)
        {
            Scare();
        }
        else if (isTransformed && originalObject != gameObject)
        {
            Revert();
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
    private void Scare()
    {
        // Instantiate the transformed object prefab if it doesn't exist
        if (transformedInstance == null)
        {
            // Create a transformed instance at the current position of the object
            transformedInstance = Instantiate(transformedObjectPrefab, transform.position, transform.rotation);

            // Set the speed of the transformed object
            transformedInstance.GetComponent<NavMeshAgent>().speed *= transformedSpeedMultiplier;

            // Deactivate the original object
            gameObject.SetActive(false);

            // Update the transformation flag
            isTransformed = true;
        }
    }

    private void Revert()
    {
        if (transformedInstance != null)
        {
            // Destroy the transformed object and reactivate the original one
            Destroy(transformedInstance);
            gameObject.SetActive(true);
            isTransformed = false;
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
    private void RotateToPlayer()
    {
        transform.LookAt(player);

        Vector3 direction = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
}
