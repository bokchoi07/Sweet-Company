using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMapDoorManager : MonoBehaviour
{
    [SerializeField] private Transform officeDoor;
    [SerializeField] private Transform player;

    private int minDistanceToShow = 3;

    private void Update()
    {
        float distance = Vector3.Distance(player.position, officeDoor.position);
        if (distance < minDistanceToShow)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                 SceneManager.LoadScene(2); // office scene index
            }
         }
    }
}
