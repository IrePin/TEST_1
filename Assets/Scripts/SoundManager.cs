using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource[] mySource;

    private AudioSource click;
    private AudioSource hit;
    private AudioSource appleHit;
    private AudioSource win;


    public void Start()
    {
        mySource = GetComponents<AudioSource>();

        hit = mySource[0];
        click = mySource[1];
        appleHit = mySource[2];
        win = mySource[3];
    }

    public void PlayClick()
    {
        click.Play();
    }

    public void PlayHit()
    {
        hit.Play();
    }

    public void PlayAppleHit()
    {
        appleHit.Play();
    }

    public void WinHit()
    {
        win.Play();
    }
}
