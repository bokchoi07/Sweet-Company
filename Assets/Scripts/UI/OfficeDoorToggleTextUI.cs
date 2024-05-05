using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfficeDoorToggleTextUI : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform objectToShowImage;
    [SerializeField] private float minDistanceToShow = 1.13f;
    [SerializeField] private GameObject canvasToShow;
    [SerializeField] private Door door;

    private void Awake()
    {
        canvasToShow.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, objectToShowImage.position);

        if (distance <= minDistanceToShow)
        {
            canvasToShow.SetActive(true);
            door.SetIsInRange(true);
        }
        else
        {
            canvasToShow.SetActive(false);
            door.SetIsInRange(false);
        }
    }
}
