using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EssenceUI : MonoBehaviour
{
    public static EssenceUI Instance;

    [SerializeField] Image essencespname;

    [SerializeField] TMP_Text essencename;
    [SerializeField] TMP_Text tier;
    [SerializeField] TMP_Text cooltime;
    [SerializeField] TMP_Text intro;
    [SerializeField] TMP_Text ability;
    [SerializeField] TMP_Text detail;

    EssenceData.Data essenceData;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        essencename.text = essenceData.name;
        tier.text = essenceData.tier;
        cooltime.text = essenceData.cooltime.ToString();
        intro.text = essenceData.intro;
        ability.text = essenceData.ability;
        detail.text = essenceData.detail;
        essencespname.sprite = Resources.Load<Sprite>($"3.UI/Item/{essenceData.essencespname}");
    }

    public void SetData(EssenceData.Data data)
    {
        essenceData = data;
        Init();
    }
}
