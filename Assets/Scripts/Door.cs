using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool isInRange = false;

    public bool IsPlayerInRange()
    {
        return isInRange;
    }

    public void SetIsInRange(bool value)
    {
        isInRange = value;
    }
}
