using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private Transform playerOrientation;
    [SerializeField] private Transform sleepOrientation;

    private Transform playersOriginalOrientation;
    private bool isInRange = false;

    private void Start()
    {
        playersOriginalOrientation = playerOrientation;
    }

    public bool IsPlayerInRange()
    {
        return isInRange;
    }

    public void SetIsInRange(bool value)
    {
        isInRange = value;
    }


}
