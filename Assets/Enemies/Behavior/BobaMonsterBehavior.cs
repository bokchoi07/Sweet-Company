using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BobaMonsterBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public int damageAmount;
    public AudioClip spawnSound; // AudioClip for the spawn sound
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Play spawn sound when the monster spawns
        if (spawnSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        // Only set destination if not already within attack range
        agent.SetDestination(player.position);

        // Optionally, you could add logic here to stop the enemy when within attack range
        RotateToPlayer(); // Stop moving
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
