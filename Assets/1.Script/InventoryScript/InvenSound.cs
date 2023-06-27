using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSound : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip invenOpen;
    public AudioClip invenclose;

    public void InvenOpen()
    {
        audiosource.clip = invenOpen;
        audiosource.Play();
    }

    public void InvenClose()
    {
        audiosource.clip = invenclose;
        audiosource.Play();
    }
}
