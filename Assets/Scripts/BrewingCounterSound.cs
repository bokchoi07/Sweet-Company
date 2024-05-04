using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrewingCounterSound : MonoBehaviour
{
    [SerializeField] BrewingCounter brewingCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        brewingCounter.OnStateChanged += BrewingCounter_OnStateChanged;
    }

    private void BrewingCounter_OnStateChanged(object sender, BrewingCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == BrewingCounter.State.Brewing;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
