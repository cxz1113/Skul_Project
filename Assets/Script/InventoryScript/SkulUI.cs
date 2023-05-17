using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SkulUI : MonoBehaviour
{

    public static SkulUI Instance;

    [SerializeField] Image mainsSrite;
    [SerializeField] Image skillsprite;

    [SerializeField] TMP_Text name;
    [SerializeField] TMP_Text tier;
    [SerializeField] TMP_Text type;
    [SerializeField] TMP_Text intro;
    [SerializeField] TMP_Text detail;
    [SerializeField] TMP_Text ability;
    [SerializeField] TMP_Text skillname;

    SkulData.Data skulData;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        name.text = skulData.name;
        tier.text = skulData.tier;
        type.text = skulData.type;
        intro.text = skulData.intro;
        detail.text = skulData.detail;
        ability.text = skulData.ability;
        skillname.text = skulData.skillname;
        skillsprite.sprite = Resources.Load<Sprite>($"3.UI/Skill/{skulData.skillspname}");
    }

    public void SetData(SkulData.Data data)
    {
        skulData = data;
        Init();
    }

}
