using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaPotCounterSound : MonoBehaviour
{
    [SerializeField] BobaPotCounter bobaPotCounter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        bobaPotCounter.OnStateChanged += BobaPotCounter_OnStateChanged;
    }

    private void BobaPotCounter_OnStateChanged(object sender, BobaPotCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == BobaPotCounter.State.Boiling || e.state == BobaPotCounter.State.Boiled;
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
