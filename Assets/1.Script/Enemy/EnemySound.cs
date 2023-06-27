using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public Enemy enemy;
    public AudioClip atkready;
    public AudioClip atksuond;
    public AudioClip tackleready;
    public AudioClip tackle;
    public AudioSource audiosource;

    void Start()
    {
    }

    void Update()
    {
        
    }

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
        audiosource.clip = atksuond;

        audiosource.Play();
    }
    public void TackleStart()
    {
        audiosource.clip = atksuond;

        audiosource.Play();
    }

}
