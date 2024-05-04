using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // display msg "ready to sell? smth like that
        // let player confirm or go back
        // if confirm, change scene
        SceneManager.LoadScene(0);
    }
}
