using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed;
    [SerializeField] private KitchenObject[] kitchenObjectArray;

    private void Update()
    {
        foreach (KitchenObject kitchenObject in kitchenObjectArray)
        {
            kitchenObject.transform.Rotate(rotationSpeed * Time.deltaTime);
        }
    }
}
