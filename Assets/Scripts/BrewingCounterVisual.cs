using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject counterBrewingGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private BrewingCounter brewingCounter;


    private void Start()
    {
        brewingCounter.OnStateChanged += BrewingCounter_OnStateChanged;
    }

    private void BrewingCounter_OnStateChanged(object sender, BrewingCounter.OnStateChangedEventArgs e)
    {
        /*bool showVisual = e.state == BobaPotCounter.State.Boiling || e.state == BobaPotCounter.State.Boiled;
        potBoilingGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);*/
        if (e.state == BrewingCounter.State.Idle)
        {
            counterBrewingGameObject.SetActive(false);
            particlesGameObject.SetActive(false);

        }
        else if (e.state == BrewingCounter.State.Brewing)
        {
            counterBrewingGameObject.SetActive(true);
            particlesGameObject.SetActive(true);
        }
        else if (e.state == BrewingCounter.State.Brewed)
        {
            counterBrewingGameObject.SetActive(false);
            particlesGameObject.SetActive(false);
        }
    }
}
