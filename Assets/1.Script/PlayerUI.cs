using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;
    public Image head1;
    public Image head2;
    public Image hpGage;
    public TMP_Text curHpTxt;
    public TMP_Text maxHpTxt;
    public PlayerDemo player;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {
        curHpTxt.text = string.Format($"{player.curHp}");
    }

    void PlayerUISet()
    {
        maxHpTxt.text = string.Format($"{player.maxHp}");
    }
}
