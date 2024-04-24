using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPotVisual : MonoBehaviour
{
    [SerializeField] private GameObject potBoilingGameObject;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private BobaPotCounter bobaPotCounter;


    private void Start()
    {
        bobaPotCounter.OnStateChanged += BobaPotCounter_OnStateChanged;
    }

    private void BobaPotCounter_OnStateChanged(object sender, BobaPotCounter.OnStateChangedEventArgs e)
    {
        /*bool showVisual = e.state == BobaPotCounter.State.Boiling || e.state == BobaPotCounter.State.Boiled;
        potBoilingGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);*/
        if (e.state == BobaPotCounter.State.Idle)
        {
            potBoilingGameObject.SetActive(false);
            particlesGameObject.SetActive(false);

        }
        else if (e.state == BobaPotCounter.State.Boiling)
        {
            potBoilingGameObject.SetActive(true);

            ParticleSystem.MainModule main = particlesGameObject.GetComponent<ParticleSystem>().main;
            main.startColor = new Color(149, 101, 69); // Brown color
            particlesGameObject.SetActive(true);
        }
        else if (e.state == BobaPotCounter.State.Boiled)
        {
            potBoilingGameObject.SetActive(true);

            ParticleSystem.MainModule main = particlesGameObject.GetComponent<ParticleSystem>().main;
            main.startColor = Color.black;
            particlesGameObject.SetActive(true);
        }
        else if (e.state == BobaPotCounter.State.Burned)
        {
            potBoilingGameObject.SetActive(true);
            particlesGameObject.SetActive(false);
        }
    }
}
