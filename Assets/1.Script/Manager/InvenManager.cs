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
    public Sequence mySequence;
    public Item[][] itemBox = new Item[4][];
    public Item itemSelect;
    public Image selectImage;
    public int indexX = 0;
    public int indexY = 0;
    public bool isItemC = false;
    public bool isIndex = false;
    Direct dir;
    void Awake() => Instance = this;

    void Update()
    {
        if(PlayerBasket.Instance.isInven)
        {
            SelectItem();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ProjectManager.Instance.inven.invenCanvas.gameObject.SetActive(false);
                scroll.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 1080);
                PlayerBasket.Instance.isInven = false;
                if (!PlayerBasket.Instance.isInven)
                    Time.timeScale = 1;
            }
            else if (Input.GetKey(KeyCode.D))
                PlayerBasket.Instance.isDetail = true;
            else if(Input.GetKeyUp(KeyCode.D))
                PlayerBasket.Instance.isDetail = false;
        }
    }
    public IEnumerator DotweenScroll()
    {
        mySequence = DOTween.Sequence();
        yield return new WaitForSeconds(0.5f);
        mySequence.Append(scroll.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1720, 1080), 1f, true));
    }

    public void ItemBox(List<Item> items1, List<Item> items2, List<Item> items3)
    {
        InputData(itemBox, items1, 0, 2);
        InputData(itemBox, items2, 1, 1);
        InputData(itemBox, items3, 2, 3);
        InputData(itemBox, items3, 3, 3);

        InvenData();
    }

    void InvenData()
    {
        InputData(ui.imagesItem, itemBox, 0);
        InputData(ui.imagesItem, itemBox, 1);
        InputData(ui.imagesItem, itemBox, 2);
        InputData(ui.imagesItem, itemBox, 3);
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
    }

    public Item ItemIn()
    {
        if (indexY == 1)
        {
            indexX = 0;
            itemSelect = itemBox[indexY][indexX];
        }
        else
        {
            isItemC = true;
            itemSelect = itemBox[indexY][indexX];
        }

        JsonSet(itemSelect);
        selectImage.transform.position = ui.imagesItem[indexY][indexX].transform.position;
        
        return itemSelect;
    }

    public void JsonSet(Item itemS)
    {
        ItemData itemData = FindObjectOfType<ItemData>();
        SkulData skulData = FindObjectOfType<SkulData>();

        int count = 0;
        if (itemS == null)
            return;
        if (itemS.it == ItemType.Item)
        {
            while (count < itemData.itemDatajson.item.Count)
            {
                if (itemS.name != itemData.itemDatajson.item[count].itemname)
                {
                    count++;
                }
                else
                {
                    itemS.itemJson = itemData.itemDatajson.item[count];
                    break;
                }
            }
        }
        else if (itemS.it == ItemType.Head)
        {
            while (count < skulData.skulDataJson.skul.Count)
            {
                if (itemS.name != skulData.skulDataJson.skul[count].itemskul)
                {
                    count++;
                }
                else
                {
                    itemS.skulJson = skulData.skulDataJson.skul[count];
                    break;
                }
            }
        }
    }
    void InputData(Item[][] itemsBox, List<Item> items, int a, int b)
    {
        itemsBox[a] = new Item[b];

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                break;
            else if (a == 3 && items.Count < 3)
                break;
            else if(a == 3 && items.Count > 2)
                itemsBox[a][i] = items[i+3];
            else
                itemsBox[a][i] = items[i];
        }
    }

    void InputData(Image[][] images, Item[][] itemsBox, int a)
    {
        for (int i = 0; i < itemsBox[a].Length; i++)
        {
            if (itemsBox[a][i] == null)
                images[a][i].sprite = ui.nullSprite;
            else
            {
                if(a == 0)
                    images[a][i].sprite = itemsBox[a][i].ss.headItem;
                else if (a == 3)
                    images[a][i].sprite = itemsBox[a][i + 3].id.item;
                else
                    images[a][i].sprite = itemsBox[a][i].id.item;
            }
        }
    }

    void keySelect()
    {
        switch (dir)
        {
            case Direct.Left:
                isIndex = true;
                indexX--;
                break;
            case Direct.Right:
                isIndex = true;
                indexX++;
                break;
            case Direct.Up:
                isIndex = true;
                indexY--;
                break;
            case Direct.Down:
                isIndex = true;
                indexY++;
                break;
        }
    }
}
