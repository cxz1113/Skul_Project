using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image head1;
    public Image head2;
    public Image hpGage;
    public Image skill1Sprite;
    public Image skill2Sprite;
    public TMP_Text curHpTxt;
    public TMP_Text maxHpTxt;
    public PlayerDemo player;
    public Image skill1_Mask;
    public Image skill2_Mask;
    public Sprite nullSprite;
    public Image selectImage;
    public Image[][] imagesItem = new Image[4][];
    public Item item;
    public SkulData.Data dataSkul;
    public ItemData.Data dataItem;

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

    void Update()
    {
        if (PlayerBasket.Instance.isInven)
            DataType();
    }

    public void SetData()
    {
        item = InvenManager.Instance.ItemIn();
        if (item == null)
            return;

        if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {
            string[] jsonData = { dataSkul.name, dataSkul.tier, dataSkul.type, dataSkul.intro, dataSkul.passive, dataSkul.ability, dataSkul.skillname1, dataSkul.skillname2 };
            string[] jsonSkillData = { dataSkul.ability, dataSkul.abilitydetail, dataSkul.skillname1, dataSkul.skillname2, dataSkul.skillname1detail, dataSkul.skillname2detail,
            dataSkul.cooltime1, dataSkul.cooltime2};
            if(strType1.Count < 8 && strType1Skill.Count < 8)
            {
                InputData(strType1, jsonData);
                InputData(strType1Skill, jsonSkillData);
            }
        }
        else if(item.it == ItemType.Head && item.ss.Skill2 == null)
        {
            string[] jsonData1 = { dataSkul.name, dataSkul.tier, dataSkul.type, dataSkul.intro, dataSkul.passive, dataSkul.ability, dataSkul.skillname1};
            string[] jsonSkillData1 = { dataSkul.ability, dataSkul.abilitydetail, dataSkul.skillname1, dataSkul.skillname1detail, dataSkul.cooltime1};
            if (strType2.Count < 8 && strType2Skill.Count < 5)
            {
                InputData(strType2, jsonData1);
                InputData(strType2Skill, jsonSkillData1);
            }
        }
        else if(item.it == ItemType.Item || item.it == ItemType.Essence)
        {
            string[] jsonData = { dataItem.name, dataItem.tier, dataItem.intro, dataItem.itemdetail, dataItem.abillity1, dataItem.abillity2 };
            if(strType3.Count < 6)
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
            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);
            return;
        }

        dataSkul = item.skulJson;
        dataItem = item.itemJson;
        
        if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {

            type1.gameObject.SetActive(true);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);

            SetData();
            InvenType1();
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
        {

            type2.gameObject.SetActive(true);
            type1.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);

            SetData();
            InvenType2();
        }
        else if (item.it == ItemType.Item || item.it == ItemType.Essence)
        {

            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(true);

            SetData();
            InvenItem();
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
        }
    }
}
