using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCounter : MonoBehaviour
{
    [SerializeField] private KitchenIngredientSO kitchenIngredientSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenCounter secondKitchenCounter;
    [SerializeField] private bool testing;

    private KitchenIngredient kitchenIngredient;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenIngredient != null)
            {
                kitchenIngredient.SetKitchenCounter(secondKitchenCounter);
            }
        }
    }

    public void Interact()
    {
        if (kitchenIngredient == null)
        {
            Transform kitchenIngredientTransform = Instantiate(kitchenIngredientSO.prefab, counterTopPoint);
            kitchenIngredientTransform.GetComponent<KitchenIngredient>().SetKitchenCounter(this);
            kitchenIngredientTransform.localPosition = Vector3.zero;

            kitchenIngredient = kitchenIngredientTransform.GetComponent<KitchenIngredient>();
            kitchenIngredient.SetKitchenCounter(this);
        }
        else
        {
            Debug.Log(kitchenIngredient.GetKitchenCounter());
        }
    }

    public Transform GetKitchenIngredientFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenIngredient(KitchenIngredient kitchenIngredient)
    {
        this.kitchenIngredient = kitchenIngredient;
    }

    public KitchenIngredient GetKitchenIngredient()
    {
        return kitchenIngredient;
    }

    public void ClearKitchenIngredient()
    {
        kitchenIngredient = null;
    }

    public bool hasKitchenIngredient()
    {
        return kitchenIngredient != null;
    }
}
