using UnityEngine;

public class BobaMonsterController : MonoBehaviour
{
    public float activationDelay = 180f; // Time delay in seconds before activating the monster (3 minutes)

    private bool monsterActivated = false;
    private float timer = 0f;

    void Start()
    {
        // Initially deactivate the monster
        transform.Find("BobaCrawler").gameObject.SetActive(false);
    }

    void Update()
    {
        // Start the timer
        timer += Time.deltaTime;

        // Check if the timer has exceeded the activation delay and the monster is not yet activated
        if (!monsterActivated && timer >= activationDelay)
        {
            // Activate the monster
            transform.Find("BobaCrawler").gameObject.SetActive(true);
            monsterActivated = true;
        }
    }
}


