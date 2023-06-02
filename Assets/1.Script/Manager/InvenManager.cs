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
    [SerializeField] PlayerUI ui;
    public Transform itemT;
    public SkulData skulData;
    public ItemData itemData;
    public Sequence mySequence;
    public Item[,] itemsBox = new Item[4, 3];

    int indexX = 0;
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
            keySelect();
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
        //keySelect();
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
        InvenData();
        //keySelect();
    }
    void InvenData()
    {
        ui.imagesitemData[0].sprite = itemsBox[0,0].ss.headItem;
        ui.imagesitemData[1].sprite = itemsBox[0, 1].ss.headItem;
        //ui.imagesitemData[2].sprite = itemsBox[1, 0].ss.headItem;
        
    }

    void SelcetData()
    {
        keySelect();
    }
    
    void keySelect()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");

        int saveX = 0;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            indexX++;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            indexX--;
        }
    }

    void SelectKey()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        int y = (int)Input.GetAxisRaw("Vertical");
        int indexX = 0;
        int indexY = 0;
        indexX += x < 0 ? x-- : x++;
        indexY += y < 0 ? y-- : y++;
        int saveX = indexX;
        int saveY = indexY;
        Item ss = itemsBox[saveX, saveY];
        Debug.Log(saveX);
        
    }
    void SelectItem()
    {
    }
}
