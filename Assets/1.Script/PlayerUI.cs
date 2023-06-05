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
    public Image skill1;
    public Image skill2;
    public TMP_Text curHpTxt;
    public TMP_Text maxHpTxt;
    public PlayerDemo player;
    public Image skill1_Mask;
    public Image skill2_Mask;
    public Sprite nullSprite;
    public Image selectImage;
    public Image[][] imagesItem = new Image[4][];
    public Item item;
    public SkulData skulData;
    public SkulData.Data data;

    #region MainSkulDataType1
    [Header("MainSkulDataType1")]
    public GameObject type1;
    public List<Image> imagesType1 = new List<Image>();
    public List<TMP_Text> txtType1 = new List<TMP_Text>();
    public List<TMP_Text> txtskillType1 = new List<TMP_Text>();
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

    public bool IsType1 { get; set; }
    public bool IsType2 { get; set; }
    public bool IsType3 { get; set; }

    void Start()
    {
        
    }
    void Update()
    {
       if (PlayerBasket.Instance.isInven)
            DataSet();
    }
    void InvenType1()
    {
        int count = 0;
        data = item.skulJson;
        string[] dataName = { "name", "tier", "type", "intro", "passive", "ability", "skillname1", "skillname2"};
        string[] jsonData = {data.name, data.tier, data.type, data.intro, data.passive, data.ability, data.skillname1, data.skillname2};
        InputData(dataName, jsonData, dicType1_1);

        /*while(count < skulData.skulDataJson.skul.Count)
        {
            if(dicType1_1.ContainsKey(txtType1[count].name))
            {
                count++;
                txtType1[count].text = dicType1_1[dataName[count]];
            }
            break;
        }*/

        //InputData(dataName, jsonData, dicType1_2);
    }

    void InvenType2()
    {
        data = item.skulJson;
        string[] dataName = { "skulName", "rank", "type", "intro", "detail", "ability", "skillname1",
            "skillname1_1", "skill1Detail", "ability1", "abilityDetail", "coolTime" };
        for (int i = 0; i < dicType2.Count; i++)
        {
            //dicType1.Add(dataName[i], dataName[i]);
        }
    }

    void InvenItem()
    {

    }

    void DataSet()
    {
        item = InvenManager.Instance.itemSelect;
        
        if (item == null)
        {
            type1.gameObject.SetActive(false);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 != null)
        {
            ProjectManager.Instance.HeadJson();

            type1.gameObject.SetActive(true);
            type2.gameObject.SetActive(false);
            type3.gameObject.SetActive(false);

            InvenType1();
        }
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
        {
            ProjectManager.Instance.HeadJson();

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

            //InvenItem();
        }
        
    }
    public void ImageSet()
    {
        imagesItem[0] = new Image[2];
        imagesItem[1] = new Image[1];
        imagesItem[2] = new Image[3];
        imagesItem[3] = new Image[3];
        for (int i = 0; i < imagesItemHeadData.Count; i++)
        {
            imagesItem[0][i] = imagesItemHeadData[i];
        }
        for (int i = 0; i < imagesItemEssenceData.Count; i++)
        {
            imagesItem[1][i] = imagesItemEssenceData[i];
        }
        for (int i = 0; i < imagesItem1Data.Count; i++)
        {
            imagesItem[2][i] = imagesItem1Data[i];
        }
        for (int i = 0; i < imagesItem2Data.Count; i++)
        {
            imagesItem[3][i] = imagesItem2Data[i];
        }
    }

    void InputData(string[] str, string[] strJson, Dictionary<string, string> dic)
    {
        for(int i = 0; i < 9; i++)
        {
            dic.Add(str[i], i.ToString());
        }
        Debug.Log(dic["name"]);
    }
    /*void InputData(string[] str, Dictionary<string, SkulData.Data> dic)
    {
        for (int i = 0; i <= ; i++)
        {
            dic.Add(str[i], );
            Debug.Log(dic["name"]);
        }
    }*/

    void InputData(Image[][] virtualBox, List<Image> imagesBox, int a)
    {
        for(int i = 0; i < imagesBox.Count; i++)
        {
            virtualBox[a][i] = imagesBox[i];
        }
    }
}
