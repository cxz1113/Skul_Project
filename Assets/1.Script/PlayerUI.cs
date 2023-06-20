using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image[][] imagesItem = new Image[4][];
    public Item item;

    #region InGameUI
    [Header("InGameUI")]
    public Image head1;
    public Image head2;
    public Image hpGage;
    public Image skill1Sprite;
    public Image skill2Sprite;
    public TMP_Text curHpTxt;
    public TMP_Text maxHpTxt;
    public Image skill1_Mask;
    public Image skill2_Mask;
    public Sprite nullSprite;
    #endregion

    #region MainSkulDataType1
    [Header("MainSkulDataType1")]
    public GameObject type1;
    public GameObject type1Skill;
    public Image skulIconType1;
    public List<Image> imagesType1 = new List<Image>();
    public List<Image> imagesSkillType1 = new List<Image>();
    public List<TMP_Text> txtType1 = new List<TMP_Text>();
    public List<TMP_Text> txtSkillType1 = new List<TMP_Text>();
    public List<string> strType1 = new List<string>();
    public List<string> strType1Skill = new List<string>();
    #endregion

    #region MainSkulDataType2
    [Header("MainSkulDataType2")]
    public GameObject type2;
    public GameObject type2Skill;
    public Image skulIconType2;
    public Image imagesType2;
    public Image imagesSkillType2;
    public List<TMP_Text> txtType2 = new List<TMP_Text>();
    public List<TMP_Text> txtSkillType2 = new List<TMP_Text>();
    public List<string> strType2 = new List<string>();
    public List<string> strType2Skill = new List<string>();
    #endregion

    #region
    [Header("MainItemData")]
    public GameObject type3;
    public Image type3Icon;
    public List<TMP_Text> txtType3 = new List<TMP_Text>();
    public List<string> strType3 = new List<string>();
    #endregion

    #region Item
    [Header("Item")]
    public List<Image> imagesItemHeadData = new List<Image>();
    public List<Image> imagesItemEssenceData = new List<Image>();
    public List<Image> imagesItem1Data = new List<Image>();
    public List<Image> imagesItem2Data = new List<Image>();
    #endregion

    #region
    [Header("ScrollView")]
    public CountCheck countCheck;
    public TMP_Text killTxt;
    public TMP_Text boneTxt;
    public TMP_Text goldTxt;
    public TMP_Text gemTxt;
    #endregion

    void Update()
    {
        if (PlayerBasket.Instance.isInven)
            DataType();
        else if(!PlayerBasket.Instance.isInven)
            ClearList(strType1, strType1Skill, strType2, strType2Skill);
        ScrollViewCountTxt();
    }

    public void SetData(Item item)
    {
        if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {
            SkulData.Data data = item.skulJson;
            string[] jsonData = { data.name, data.tier, data.type, data.intro, data.passive, data.ability, data.skillname1, data.skillname2 };
            string[] jsonSkillData = { data.ability, data.abilitydetail, data.skillname1, data.skillname2, data.skillname1detail, data.skillname2detail,
            data.cooltime1, data.cooltime2};
            ActiveObjChange(strType1, strType1Skill, jsonData, jsonSkillData);
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
        {
            SkulData.Data data = item.skulJson;
            string[] jsonData = { data.name, data.tier, data.type, data.intro, data.passive, data.ability, data.skillname1 };
            string[] jsonSkillData = { data.ability, data.abilitydetail, data.skillname1, data.skillname1detail, data.cooltime1 };
            ActiveObjChange(strType2, strType2Skill, jsonData, jsonSkillData);
        }
        else if (item.it == ItemType.Item || item.it == ItemType.Essence)
        {
            ItemData.Data data = item.itemJson;
            string[] jsonData = { data.name, data.tier, data.intro, data.itemdetail, data.abillity1, data.abillity2 };
            if (strType3.Count < 1)
                InputData(strType3, jsonData);
        }
    }

    void InvenType1()
    {
        Sprite[] sprite = { item.ss.skill1, item.ss.Skill2 };
        ImageIconType(skulIconType1, item);
        ImageData(imagesType1, sprite);
        TextData(txtType1, strType1);
        if(PlayerBasket.Instance.isDetail1)
        {
            type1Skill.SetActive(true);
            ImageData(imagesSkillType1, sprite);
            TextData(txtSkillType1, strType1Skill);
        }
        else
            type1Skill.SetActive(false);
    }

    void InvenType2()
    {
        ImageIconType(skulIconType2, item);
        imagesType2.sprite = item.ss.skill1;
        TextData(txtType2, strType2);
        if (PlayerBasket.Instance.isDetail1)
        {
            type2Skill.SetActive(true);
            imagesSkillType2.sprite = item.ss.skill1;
            TextData(txtSkillType2, strType2Skill);
        }
        else
            type2Skill.SetActive(false);
    }

    void InvenItem()
    {
        type3Icon.sprite = item.id.Inven;
        TextData(txtType3, strType3);
    }

    void DataType()
    {
        item = FindObjectOfType<InvenManager>().ItemIn();
        if (item == null)
        {
            ActiveObj(false, false, false);
            return;
        }
        if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {
            ActiveObj(true, false, false);
            SetData(item);
            InvenType1();
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
        {
            
            ActiveObj(false, true, false);
            SetData(item);
            InvenType2();
        }
        else if (item.it == ItemType.Item || item.it == ItemType.Essence)
        {
            ActiveObj(false, false, true);
            SetData(item);
            InvenItem();
        }
    }

    void ActiveObj(bool isType1, bool isType2, bool isType3)
    {
        type1.gameObject.SetActive(isType1);
        type2.gameObject.SetActive(isType2);
        type3.gameObject.SetActive(isType3);
    }

    void ActiveObjChange(List<string> strList, List<string> strSkillList, string[] jsonStr, string[] jsonSkillStr)
    {
        InvenManager inven = FindObjectOfType<InvenManager>();
        if (!inven.isIndex && strList.Count < 1 && strSkillList.Count < 1)
        {
            InputData(strList, jsonStr);
            InputData(strSkillList, jsonSkillStr);
        }
        else if (inven.isIndex)
        {
            strList.Clear();
            strSkillList.Clear();

            inven.isIndex = false;
            if (strList.Count < 1 && strSkillList.Count < 1)
            {
                InputData(strList, jsonStr);
                InputData(strSkillList, jsonSkillStr);
            }
        }
    }

    public void ImageSet()
    {
        InputData(imagesItem, imagesItemHeadData, 0, 2);
        InputData(imagesItem, imagesItemEssenceData, 1, 1);
        InputData(imagesItem, imagesItem1Data, 2, 3);
        InputData(imagesItem, imagesItem2Data, 3, 3);
    }

    void InputData(Image[][] virtualBox, List<Image> imagesBox, int a, int b)
    {
        virtualBox[a] = new Image[b];
        for (int i = 0; i < imagesBox.Count; i++)
        {
            virtualBox[a][i] = imagesBox[i];
        }
    }

    void InputData(List<string> box, string[] str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            box.Add(str[i]);
        }
    }

    void ImageData(List<Image> box, Sprite[] sprites)
    {
        int count = 0;
        while (count < box.Count)
        {
            box[count].sprite = sprites[count];
            count++;
        }
    }

    void TextData(List<TMP_Text> box, List<string> str)
    {
        for (int i = 0; i < box.Count; i++)
        {
            box[i].text = str[i];
        }
    }

    void ImageIconType(Image type, Item item)
    {
        RectTransform typePos = type.GetComponent<RectTransform>();
        switch (item.skulJson.itemskul)
        {
            case "LittleBorn":
                type.sprite = item.ss.headInven;
                type.SetNativeSize();
                typePos.anchoredPosition = new Vector2(0, 4);
                break;
            case "Wolf":
                type.sprite = item.ss.headInven;
                type.SetNativeSize();
                typePos.anchoredPosition = new Vector2(4, 4);
                break;
            case "Sword":
                type.sprite = item.ss.headInven;
                type.SetNativeSize();
                typePos.anchoredPosition = new Vector2(0, 4);
                break;
        }
    }

    void ClearList(List<string> strT1, List<string> strSkillT1, List<string> strT2, List<string> strSkillT2)
    {
        strT1.Clear();
        strSkillT1.Clear();
        strT2.Clear();
        strSkillT2.Clear();
    }

    void ScrollViewCountTxt()
    {
        killTxt.text = countCheck.killCount.ToString();
        boneTxt.text = countCheck.boneCount.ToString();
        goldTxt.text = countCheck.goldCount.ToString();
        gemTxt.text = countCheck.gemCount.ToString();
    }
}
