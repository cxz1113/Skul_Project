using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;


public class InvenManager : MonoBehaviour
{
    [SerializeField] public Canvas invenCanvas;
    [SerializeField] GameObject scroll;
    public SkulData skulData;
    public ItemData itemData;
    public Sequence mySequence;
    public Sprite nullSprite;
    public Item[,] itemsBox = new Item[4, 3];

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

    [Header("ItemBox")]
    public List<Image> imagesItem = new List<Image>();


    void Start()
    {
        StartCoroutine(DotweenScroll());
        InvenType1();
        InvenType2();
    }

    void InvenType1()
    {
        string[] dataName = { "skulName", "rank", "type", "intro", "detail", "ability", "skillname1", "skillname2", 
            "skillname1_1", "skillNmae2_1", "skill1Detail", "skill2Detail", "ability1", "abilityDetail", "coolTime" };
        for(int i = 0; i < dicType1.Count; i++)
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

    void InvenData()
    {
        
    }

    IEnumerator DotweenScroll()
    {
        mySequence = DOTween.Sequence();
        yield return new WaitForSeconds(1);
        mySequence.Append(scroll.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1720, 1080), 1f, true));
    }

    public void ItemBox(List<Item> items1, List<Item> items2, List<Item> items3)
    {
        for(int i = 0; i < items1.Count; i ++)
        {
            itemsBox[0, i] = items1[i]; 
        }
        for(int i = 0; i < items2.Count; i++)
        {
            if (items2.Count == 0)
                itemsBox[1, i] = null;
            else
                itemsBox[1, i] = items2[i];
        }
        for (int i = 2; i < 4; i++)
        {
            for (int j = 0; j < items3.Count; j++)
            {
                itemsBox[i, j] = items3[j];
            }
        }
    }
    public void ItemBoxSprite(List<Item> items1, List<Item> items2, List<Item> items3)
    {
        int count = 0;
        while(count < 10)
        {
        }
    }
}
