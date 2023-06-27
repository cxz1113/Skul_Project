using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public Enemy enemy;
    public AudioSource audiosource;
    public AudioClip atkready;
    public AudioClip atksuond;
    public AudioClip tackleready;
    public AudioClip tackle;
    public AudioClip hit;

    public void AtkReady()
    {
        audiosource.clip = atkready;

        audiosource.Play();
    }

    public void AtkSoundStart()
    {
        audiosource.clip = atksuond;

        audiosource.Play();
    }
    public void TackleReady()
    {
        audiosource.clip = tackleready;

        audiosource.Play();
    }
    public void TackleStart()
    {
        audiosource.clip = tackle;
        audiosource.Play();
    }
    public void Hit()
    {
        audiosource.clip = hit;
        audiosource.Play();
    }

}
