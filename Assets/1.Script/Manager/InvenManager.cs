using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class InvenManager : MonoBehaviour
{
    [SerializeField] public Canvas invenCanvas;
    [SerializeField] GameObject scroll;
    public SkulData skulData;
    public ItemData itemData;
    public Sequence mySequence;
    public Sprite nullSprite;


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

    #region Item
    [Header("Item")]
    public List<Image> imagesItem = new List<Image>();
    #endregion
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
}
