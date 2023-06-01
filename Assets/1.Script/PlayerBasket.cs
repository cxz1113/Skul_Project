using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasket : MonoBehaviour
{
    public static PlayerBasket Instance;

    public Player player;
    public Item headframe;
    public string skul;
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

    public string item;

    void Awake() => Instance = this;

    void Update()
    {
        player = ProjectManager.Instance.player;
        headframe = ProjectManager.Instance.heads[0];
    }
}
