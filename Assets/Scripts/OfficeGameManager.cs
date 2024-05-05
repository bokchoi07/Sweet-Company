using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeGameManager : MonoBehaviour
{
    [SerializeField] private Door bobaShopDoor;
    [SerializeField] private Door mapDoor;

    private void Update()
    {
        if (bobaShopDoor.IsPlayerInRange() && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(3); // boba shop scene index
        }
        if (mapDoor.IsPlayerInRange() && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(4); // map scene index
        }
    }
}
