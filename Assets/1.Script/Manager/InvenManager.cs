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
    public static InvenManager Instance;
    [SerializeField] public Canvas invenCanvas;
    [SerializeField] GameObject scroll;
    [SerializeField] PlayerUI ui;
    public Transform itemT;
    public SkulData skulData;
    public ItemData itemData;
    public Sequence mySequence;
    public Item[][] itemBox = new Item[4][];
    public Item itemSelect;
    public Image selectImage;
    Direct dir;
    int indexX = 0;
    int indexY = 0;
    void Awake() => Instance = this;

    void Start()
    {
        StartCoroutine(DotweenScroll());
    }

    void Update()
    {
        if(PlayerBasket.Instance.isInven)
        {
            SelectItem();
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
        itemBox[0] = new Item[2];
        itemBox[1] = new Item[1];
        itemBox[2] = new Item[3];
        itemBox[3] = new Item[3];
        for (int i = 0; i < items1.Count; i++)
        {
            itemBox[0][i] = items1[i];
        }
        for (int i = 0; i < items2.Count; i++)
        {
            itemBox[1][i] = items2[i];
        }
        for (int i = 0; i < items3.Count; i++)
        {
            itemBox[2][i] = items3[i];
        }
        for(int i = 0; i < items3.Count-1; i++)
        {
            itemBox[3][i] = items3[i + 3];
        }
        Debug.Log(items3.Count);

        InvenData();
    }

    void InvenData()
    {
        for (int i = 0; i < itemBox[0].Length; i++)
        {
            if (itemBox[0][i] == null)
                ui.imagesItem[0][i].sprite = ui.nullSprite;
            else
                ui.imagesItem[0][i].sprite = itemBox[0][i].ss.headItem;
        }
        for (int i = 0; i < itemBox[1].Length; i++)
        {
            if (itemBox[1][i] == null)
                ui.imagesItem[1][i].sprite = ui.nullSprite;
            else
                ui.imagesItem[1][i].sprite = itemBox[1][i].id.item;
        }
        for (int i = 0; i < itemBox[2].Length; i++)
        {
            if (itemBox[2][i] == null)
                ui.imagesItem[2][i].sprite = ui.nullSprite;
            else
                ui.imagesItem[2][i].sprite = itemBox[2][i].id.item;
        }
        for (int i = 0; i < itemBox[3].Length; i++)
        {
            if (itemBox[3][i] == null)
                ui.imagesItem[3][i].sprite = ui.nullSprite;
            else
                ui.imagesItem[3][i].sprite = itemBox[3][i + 3].id.item;
        }
    }

    public void SelectItem()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) &&  indexX < itemBox[indexY].Length - 1)
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
       
        if (indexY == 1)
        {
            indexX = 0;
            itemSelect = itemBox[indexY][indexX];
        }
        else
            itemSelect = itemBox[indexY][indexX];

        selectImage.transform.position = ui.imagesItem[indexY][indexX].transform.position;
    }

    void keySelect()
    {
        switch (dir)
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
}
