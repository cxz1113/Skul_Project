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
    public Image skulIconType1;
    public List<Image> imagesType1 = new List<Image>();
    public List<Image> imagesSkillType1 = new List<Image>();
    public List<TMP_Text> txtType1 = new List<TMP_Text>();
    public List<TMP_Text> txtSkillType1 = new List<TMP_Text>();
    public Dictionary<string, string> dicType1_1 = new Dictionary<string, string>();
    public Dictionary<string, string> dicType1_2 = new Dictionary<string, string>();
    #endregion

    #region MainSkulDataType2
    [Header("MainSkulDataType2")]
    public GameObject type2;

    public List<Image> imagesType2 = new List<Image>();
    public Dictionary<int, string> dicType2 = new Dictionary<int, string>();
    #endregion

    #region
    [Header("MainItemData")]
    public GameObject type3;
    public List<Image> imagesItemMain = new List<Image>();
    #endregion

    #region Item
    [Header("Item")]
    public List<Image> imagesItemHeadData = new List<Image>();
    public List<Image> imagesItemEssenceData = new List<Image>();
    public List<Image> imagesItem1Data = new List<Image>();
    public List<Image> imagesItem2Data = new List<Image>();
    #endregion


    void Start()
    {
        SetData();
    }

    void Update()
    {
        if (PlayerBasket.Instance.isInven)
            DataType();
    }

    public void SetData()
    {
        dataSkul = InvenManager.Instance.ItemIn().skulJson;

        string[] dataName = { "name", "tier", "type", "intro", "passive", "ability", "skillname1", "skillname2" };
        string[] jsonData = { dataSkul.name, dataSkul.tier, dataSkul.type, dataSkul.intro, dataSkul.passive, dataSkul.ability, dataSkul.skillname1, dataSkul.skillname2 };
        InputData(dataName, jsonData, dicType1_1);
    }

    void InvenType1()
    {
        Sprite[] sprite = { item.ss.skill1, item.ss.Skill2 };
        string[] dataName = { "name", "tier", "type", "intro", "passive", "ability", "skillname1", "skillname2" };
        SkulIcon(skulIconType1, item);
        ImageData(imagesType1, sprite);
        TextData(txtType1, dataName, dicType1_1);

    }

    void InvenType2()
    {
        
    }

    void InvenItem()
    {
        dataItem = InvenManager.Instance.ItemIn().itemJson;
    }

    void DataType()
    {
        item = InvenManager.Instance.ItemIn();
        if (item == null)
        {
            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);
            return;
        }

        dataItem = item.itemJson;
        dataSkul = item.skulJson;
        
        if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {

            type1.gameObject.SetActive(true);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);

            InvenType1();
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
        {

            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(true);
            type3.gameObject.SetActive(false);

            InvenType2();
        }
        else if (item.it == ItemType.Item || item.it == ItemType.Essence)
        {

            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(true);

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

    void InputData(string[] str, string[] strJson, Dictionary<string, string> dic)
    {
        for (int i = 0; i < 8; i++)
        {
            dic.Add(str[i], strJson[i]);
        }
    }

    void InputData(Image[][] virtualBox, List<Image> imagesBox, int a, int b)
    {
        virtualBox[a] = new Image[b];
        for (int i = 0; i < imagesBox.Count; i++)
        {
            virtualBox[a][i] = imagesBox[i];
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

    void TextData(List<TMP_Text> box, string[] str, Dictionary<string, string> dic)
    {
        int count = 0;
        while (count < box.Count)
        {
            box[count].text = dic[str[count]];
            count++;
        }
    }

    void SkulIcon(Image type, Item item)
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
