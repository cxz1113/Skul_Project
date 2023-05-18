using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public static ItemUI Instance;

    [SerializeField] Image itemspname;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text intro;
    [SerializeField] TMP_Text itemdetail;
    [SerializeField] TMP_Text tier;
    [SerializeField] TMP_Text value1;
    [SerializeField] TMP_Text value2;
    [SerializeField] TMP_Text abillity1;
    [SerializeField] TMP_Text abillity2;

    ItemData.Data itemData;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        name.text = itemData.name;
        tier.text = itemData.tier;
        tier.text = itemData.tier;
        value1.text = itemData.value1;
        value2.text = itemData.value2;
        abillity1.text = itemData.abillity1;
        abillity2.text = itemData.abillity2;
        itemspname.sprite = Resources.Load<Sprite>($"3.UI/Item/{itemData.itemspname}");
    }

    public void SetData(ItemData.Data data)
    {
        itemData = data;
        Init();
    }
}
