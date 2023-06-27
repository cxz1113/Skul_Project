using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public Player player;
    public AudioSource audiosource;

    #region Inventory
    [Header("Inventory")]
    public AudioClip invenOpen;
    public AudioClip invenClose;
    #endregion

    #region Player
    [Header("Player")]
    public AudioClip atkA;
    public AudioClip atkB;
    public AudioClip Dash;
    public AudioClip Dead;
    public AudioClip Jump;
    public AudioClip JumpAtk;
    public AudioClip SkillA;
    public AudioClip Teleport;
    public AudioClip Switch;
    public AudioClip SwitchAtk;
    #endregion

    public void ATKA()
    {
        audiosource.clip = atkA;

        audiosource.Play();
    }

    public void ATKB()
    {
        audiosource.clip = atkB;

        audiosource.Play();
    }
    public void DASH()
    {
        audiosource.clip = Dash;

        audiosource.Play();
    }
    public void DEAD()
    {
        audiosource.clip = Dead;

        audiosource.Play();
    }
    public void JUMP()
    {
        audiosource.clip = Jump;

        audiosource.Play();
    }
    public void JUMPATK()
    {
        audiosource.clip = JumpAtk;

        audiosource.Play();
    }
    public void SKILLA()
    {
        audiosource.clip = SkillA;

        audiosource.Play();
    }
    public void TELEPORT()
    {
        audiosource.clip = Teleport;

        audiosource.Play();
    }
    public void SWITCH()
    {
        audiosource.clip = Switch;

        audiosource.Play();
    }
    public void SWITCHATK()
    {
        audiosource.clip = SwitchAtk;

        audiosource.Play();
    }
    public void InvenOpen()
    {
        audiosource.clip = invenOpen;

        audiosource.Play();
    }
    public void InvenClose()
    {
        audiosource.clip = invenClose;

        audiosource.Play();
    }
}
