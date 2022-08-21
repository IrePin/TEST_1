using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioSource[] mySource;

    private AudioSource click;
    private AudioSource hit;

    public void Start()
    {
        mySource = GetComponents<AudioSource>();

        hit = mySource[0];
        click = mySource[1];
    }

    public void PlayClick()
    {
        click.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }

}
