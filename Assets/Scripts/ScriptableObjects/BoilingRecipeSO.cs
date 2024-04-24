using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BoilingRecipeSO : ScriptableObject
{
    public KitchenIngredientSO input;
    public KitchenIngredientSO output;
    public float boilTimerMax;

}
