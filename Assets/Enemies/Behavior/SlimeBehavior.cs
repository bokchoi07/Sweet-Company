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

    //Audio
    public AudioClip slimeSound; // AudioClip for the slime sound
    public float audioRange; // Distance threshold to play the audio
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

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
            AttackPlayer();
        }
        else
        {
            // If not in attack range, patrol
            if (!agent.hasPath || agent.remainingDistance < 0.5f)
            {
                Patroling();
            }
        }
        // Check if the player is within audio range
        if (Vector3.Distance(transform.position, player.position) <= audioRange)
        {
            // Play the slime sound if not already playing
            if (!audioSource.isPlaying && slimeSound != null)
            {
                audioSource.PlayOneShot(slimeSound);
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
            // Only set destination if not already within attack range
            agent.SetDestination(player.position);

            // Optionally, you could add logic here to stop the enemy when within attack range
            RotateToPlayer(); // Stop moving
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

