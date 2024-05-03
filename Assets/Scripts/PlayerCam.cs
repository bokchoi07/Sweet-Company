using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    private bool menuMode = false; // Flag to track whether the game is in menu mode

    float xRotation;
    float yRotation;

    void Start()
    {
        SetGameMode(); // Start in game mode
    }

    void Update()
    {
        if (menuMode)
        {
            // In menu mode, unlock the cursor and allow free mouse movement
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Handle menu interactions here
            // For example, detect clicks on inventory items using Raycasts

            // Toggle back to game mode when necessary (e.g., closing the menu)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetGameMode();
            }
        }
        else
        {
            // In game mode, lock the cursor and control camera with mouse movement
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);

            // Check for opening the inventory menu (e.g., pressing a key)
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetMenuMode();
            }
        }
    }

    // Switch to menu mode
    void SetMenuMode()
    {
        Time.timeScale = 0f; // Pause the game
        menuMode = true;
    }

    // Switch to game mode
    void SetGameMode()
    {
        Time.timeScale = 1f; // Resume the game
        menuMode = false;
    }
}
