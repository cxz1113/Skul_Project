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
    public Item[,] itemsBox = new Item[4, 3];

    [Header("ItemBox")]
    public List<Image> imagesItem = new List<Image>();


    void Start()
    {
        StartCoroutine(DotweenScroll());
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
            if (items1[i] == null)
                break;
            else
                itemsBox[0, i] = items1[i];
        }
        for(int i = 0; i < items2.Count; i++)
        {
            if (items1[i] == null)
                break;
            else
                itemsBox[1, i] = items2[i];
        }
        for (int i = 2; i < 4; i++)
        {
            for (int j = 0; j < items3.Count; j++)
            {
                if (items3[j] == null)
                    break;
                else
                    itemsBox[i, j] = items3[j];
            }
        }
    }
}
