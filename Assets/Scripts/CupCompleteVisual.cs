using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CupCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private CupKitchenObject cupKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        cupKitchenObject.OnIngredientAdded += CupKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void CupKitchenObject_OnIngredientAdded(object sender, CupKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO.objectName.Equals(e.kitchenObjectSO.objectName))
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
