using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenIngredient : MonoBehaviour
{
    [SerializeField] private KitchenIngredientSO kitchenIngredientSO;

    private IKitchenIngredientParent kitchenIngredientParent;

    public KitchenIngredientSO GetKitchenIngredientSO()
    {
        return kitchenIngredientSO;
    }

    public void SetKitchenIngredientParent(IKitchenIngredientParent kitchenIngredientParent)
    {
        if (this.kitchenIngredientParent != null)
        {
            this.kitchenIngredientParent.ClearKitchenIngredient();
        }

        this.kitchenIngredientParent = kitchenIngredientParent;

        if (kitchenIngredientParent.HasKitchenIngredient())
        {
            Debug.LogError("IKitchenIngredientParent already has KitchenIngredient");
        }

        kitchenIngredientParent.SetKitchenIngredient(this);

        transform.parent = kitchenIngredientParent.GetKitchenIngredientFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenIngredientParent GetKitchenIngredientParent()
    {
        return kitchenIngredientParent;
    }

    public void DestroySelf()
    {
        kitchenIngredientParent.ClearKitchenIngredient();
        Destroy(gameObject);
    }

    public static KitchenIngredient SpawnKitchenIngredient(KitchenIngredientSO kitchenIngredientSO, IKitchenIngredientParent kitchenIngredientParent)
    {
        Transform kitchenIngredientTransform = Instantiate(kitchenIngredientSO.prefab);
        KitchenIngredient kitchenIngredient = kitchenIngredientTransform.GetComponent<KitchenIngredient>();
        kitchenIngredient.SetKitchenIngredientParent(kitchenIngredientParent);

        return kitchenIngredient;
    }
}
