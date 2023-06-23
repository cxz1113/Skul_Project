using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasket : MonoBehaviour
{
    public static PlayerBasket Instance;

    public Player player;
    public List<Item> heads = new List<Item>();
    public float curHp;
    public float maxHp;
    public float HP
    {
        get { return curHp; }
        set
        {
            curHp = value;
            ProjectManager.Instance.ui.hpGage.fillAmount = curHp / maxHp;
            ProjectManager.Instance.ui.curHpTxt.text = $"{curHp}";
        }
    }
    public bool invectoryActivated = false;

    public bool isHeadChange { get; set; }
    public bool isInven { get; set; }
    public bool isDetail1 { get; set; }


    public string item;

    void Awake() => Instance = this;

    void Update()
    {
        player = ProjectManager.Instance.player;
    }
}
