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

    #region MainSkulDataType1
    [Header("MainSkulDataType1")]
    public GameObject type1;
    public List<Image> imagesType1 = new List<Image>();
    public Dictionary<int, string> dicType1 = new Dictionary<int, string>();

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


    void Update()
    {
        //if (PlayerBasket.Instance.isInven)
            //DataSet();
    }
    void InvenType1()
    {
        string[] dataName = { "skulName", "rank", "type", "intro", "detail", "ability", "skillname1", "skillname2",
            "skillname1_1", "skillNmae2_1", "skill1Detail", "skill2Detail", "ability1", "abilityDetail", "coolTime" };
        for (int i = 0; i < dicType1.Count; i++)
        {
            dicType1.Add(i, dataName[i]);
        }
    }

    void InvenType2()
    {
        string[] dataName = { "skulName", "rank", "type", "intro", "detail", "ability", "skillname1",
            "skillname1_1", "skill1Detail", "ability1", "abilityDetail", "coolTime" };
        for (int i = 0; i < dicType1.Count; i++)
        {
            dicType1.Add(i, dataName[i]);
        }
    }

    void InvenItem()
    {

    }

    void DataSet()
    {
        //ProjectManager.Instance.HeadJson();
        Item item = InvenManager.Instance.SelectItem();
        if (item.it == ItemType.Head && item.ss.Skill2 != null)
            InvenType1();
        else if (item.it == ItemType.Head && item.ss.Skill2 == null)
            InvenType2();
        else if (item.it == ItemType.Item || item.it == ItemType.Essence)
            InvenItem();
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
}
