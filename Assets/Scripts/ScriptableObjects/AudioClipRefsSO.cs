using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] boil;
    public AudioClip[] brew;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFailed;
    public AudioClip[] objectPickup;
    public AudioClip[] objectDrop;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
