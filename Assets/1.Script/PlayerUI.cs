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
    #region MainSkulDataType1
    [Header("MainSkulDataType1")]
    public List<Image> imagesType1 = new List<Image>();
    public Dictionary<int, string> dicType1 = new Dictionary<int, string>();

    #endregion

    #region MainSkulDataType2
    [Header("MainSkulDataType2")]
    public List<Image> imagesType2 = new List<Image>();
    public Dictionary<int, string> dicType2 = new Dictionary<int, string>();
    #endregion

    #region MainItemData
    [Header("MainItemData")]
    public List<Image> imagesitemData = new List<Image>();

    #endregion

    void Start()
    {
        //selectImage = FindObjectOfType<InvenManager>().itemsBox;
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
}
