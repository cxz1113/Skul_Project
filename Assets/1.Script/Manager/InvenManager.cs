using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum Direct
{
    Left,
    Right,
    Up,
    Down
}
public class InvenManager : MonoBehaviour
{
    [SerializeField] public Canvas invenCanvas;
    [SerializeField] GameObject scroll;
    [SerializeField] PlayerUI ui;
    public Transform itemT;
    public SkulData skulData;
    public ItemData itemData;
    public Sequence mySequence;
    public Item[,] itemsBox = new Item[4, 3];
    public Item iss;
    Direct dir;
    int indexX = 1;
    int indexY = 0;
    void Start()
    {
        StartCoroutine(DotweenScroll());
        keySelect();
    }

    void Update()
    {
        if(PlayerBasket.Instance.isInven)
        {
            SelectItem();
            //keySelect();
            //SelectKey();
        }
    }
    IEnumerator DotweenScroll()
    {
        mySequence = DOTween.Sequence();
        yield return new WaitForSeconds(1);
        mySequence.Append(scroll.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1720, 1080), 1f, true));
    }

    public void ItemBox(List<Item> items1, List<Item> items2, List<Item> items3)
    {
        for (int i = 0; i < items1.Count; i ++)
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
        for (int i = 0; i < items3.Count; i++)
        {
            if (items3[i] == null)
                break;
            else
                itemsBox[2, i] = items3[i];
            
        }
        for (int i = 3; i < items3.Count; i++)
        {
            if (items3[i] == null)
                break;
            else
                itemsBox[3, i] = items3[i];

        }
        InvenData();
    }
    void InvenData()
    {
        ui.imagesitemData[0].sprite = itemsBox[0,0].ss.headItem;
        ui.imagesitemData[1].sprite = itemsBox[0, 1].ss.headItem;
        //ui.imagesitemData[2].sprite = itemsBox[1, 0].ss.headItem;
        ui.imagesitemData[3].sprite = itemsBox[2, 0].id.item;
    }

    void SelcetData()
    {
        keySelect();
    }
    
    void keySelect()
    {
        switch(dir)
        {
            case Direct.Left:
                indexX--;
                break;
            case Direct.Right:
                indexX++;
                break;
            case Direct.Up:
                indexY--;
                break;
            case Direct.Down:
                indexY++;
                break;
        }
    }


    void SelectItem()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) &&  indexX < 2)
        {
            dir = Direct.Right;
            keySelect();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && indexX > 0)
        {
            dir = Direct.Left;
            keySelect();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && indexY > 0)
        {
            dir = Direct.Up;
            keySelect();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && indexY < 3)
        {
            dir = Direct.Down;
            keySelect();
        }
        iss = itemsBox[indexY, indexX];
        Debug.Log(indexX);
        //Debug.Log(indexY);

        Debug.Log(itemsBox[indexY, indexX]);
    }

}
