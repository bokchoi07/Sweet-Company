using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAltAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OnReturnToOffice;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions(); // this instance stays so gotta destroy -> .Dispose()
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlt.performed += InteractAlt_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.ReturnToOffice.performed += ReturnToOffice_performed;
    }

    

    private void OnDestroy()
    {
        // unsubscribing from events
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlt.performed -= InteractAlt_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Player.ReturnToOffice.performed -= ReturnToOffice_performed; // enter button

        playerInputActions.Dispose();
    }
    private void ReturnToOffice_performed(InputAction.CallbackContext context)
    {
        OnReturnToOffice?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlt_performed(InputAction.CallbackContext obj)
    {
        OnInteractAltAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
