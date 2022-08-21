using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource[] mySource;

    private AudioSource swoop;
    private AudioSource hit;

    public void Start()
    {
        mySource = GetComponents<AudioSource>();

        hit = mySource[0];
    }

    public void PlaySwoop()
    {
        swoop.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }

}
