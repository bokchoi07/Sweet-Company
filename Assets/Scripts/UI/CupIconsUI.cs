using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupIconsUI : MonoBehaviour
{
    [SerializeField] private CupKitchenObject cupKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        cupKitchenObject.OnIngredientAdded += CupKitchenObject_OnAddedIngredient;
    }

    private void CupKitchenObject_OnAddedIngredient(object sender, CupKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in cupKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<CupIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
