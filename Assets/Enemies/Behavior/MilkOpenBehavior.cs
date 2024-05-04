using UnityEngine;
using UnityEngine.AI;

public class MilkOpenBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public float attackRange;
    public GameObject MilkMonsterPrefab; // Reference to the MilkMonster prefab

    private GameObject originalObjectInstance; // Instance of the original object
    private GameObject transformedInstance; // Instance of the transformed MilkMonster
    private Transform player; // Reference to the player object
    private bool isAttacking = false;
    private bool hasTransformed = false; // Flag to track if the object has transformed

    public int damageAmount; // Amount of damage the slime deals to the player

    //Audio
    public AudioClip BreathSound; // AudioClip for the slime sound
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        // Find the player object by tag
        player = GameObject.Find("PlayerObj").transform;

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Start attacking immediately
        AttackPlayer();

        // Instantiate the original object prefab
        if (MilkMonsterPrefab != null)
        {
            originalObjectInstance = Instantiate(MilkMonsterPrefab, transform.position, transform.rotation);
            originalObjectInstance.SetActive(false); // Deactivate the original object instance
        }
        else
        {
            Debug.LogError("MilkMonster prefab is not assigned.");
        }
    }

    void Update()
    {
        // Check if the player is found
        if (player == null)
            return;

        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // If not transformed and already attacking, continue to chase the player
            if (!hasTransformed && isAttacking)
            {
                agent.SetDestination(player.position);
                RotateToPlayer();
            }
            // If not transformed and not attacking yet, start attacking
            else if (!hasTransformed && !isAttacking)
            {
                AttackPlayer();
            }
            // Play the slime sound if not already playing
            else if (!audioSource.isPlaying && BreathSound != null)
            {
                audioSource.PlayOneShot(BreathSound);
            }
        }
        else if (isAttacking)
        {
            // If the player leaves the attack range, stop attacking
            isAttacking = false;
            // Transform back to the original object
            RevertToOriginalObject();
        }
    }

    private void AttackPlayer()
    {
        // Ensure that the agent and player object are valid
        if (agent && player)
        {

            // Set the destination to the player's position and start attacking
            agent.SetDestination(player.position);
            isAttacking = true;

            // Ensure that the original object instance is inactive
            if (originalObjectInstance != null)
            {
                originalObjectInstance.SetActive(false);
            }
        }
    }


    private void RevertToOriginalObject()
    {
        if (transformedInstance != null)
        {
            // Destroy the transformed object instance
            Destroy(transformedInstance);
            // Reset the transformed instance reference
            transformedInstance = null;
        }

        // Activate the original MilkMonster prefab if it exists
        if (originalObjectInstance != null)
        {
            // Activate the original MilkMonster prefab
            originalObjectInstance.SetActive(true);

            // Set the original object instance position and rotation to match the current object
            originalObjectInstance.transform.position = transform.position;
            originalObjectInstance.transform.rotation = transform.rotation;

            // Set the hasTransformed flag to false
            hasTransformed = false;

            // Start patrolling behavior
            originalObjectInstance.GetComponent<MilkBehavior>().enabled = true;
        }

        // Deactivate the current object
        gameObject.SetActive(false);
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













