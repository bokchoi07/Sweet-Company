using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f; // Movement speed of the player
    [SerializeField] private float mouseSensitivity = 10.0f; // Sensitivity of mouse movement

    private float verticalRotation = 0; // Rotation around the X-axis

    void Update()
    {
        // Player Movement
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Player Rotation (Mouse Look)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
}
