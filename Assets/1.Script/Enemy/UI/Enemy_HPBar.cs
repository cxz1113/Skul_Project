using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_HPBar : MonoBehaviour
{
    public GameObject prfHP;
    public GameObject canvas;
    public Enemy enemy;
    public Transform enemytrans;

    RectTransform hpBar_Rect;
    GameObject hpBar;
    Image hpBar_img;

    public float height;
    public float hpBar_width;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("InGameCanvas");
        hpBar_Rect = Instantiate(prfHP, canvas.transform).GetComponent<RectTransform>();
        hpBar = hpBar_Rect.GetChild(0).gameObject;
        hpBar_img = hpBar.transform.GetChild(0).GetComponent<Image>();
        RectTransform hpBarBG_Rect = hpBar.GetComponent<RectTransform>();
        hpBarBG_Rect.sizeDelta = new Vector2(hpBar_width, hpBarBG_Rect.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.ed.hp == enemy.ed.maxhp)
            hpBar.SetActive(false);
        else
        {
            if (enemy.ed.hp <=0)
            {
                Destroy(hpBar_Rect.gameObject);
                enabled = false;
                return;
            }

            hpBar.SetActive(true);  
            hpBar_img.fillAmount = enemy.ed.hp / enemy.ed.maxhp;
            Vector3 hpBarPos = Camera.main.WorldToScreenPoint(transform.position - Vector3.up * height);
            //앵커 왼쪽 아래, inGameCanvas와 해상도 연동 필요
            hpBar_Rect.anchoredPosition = hpBarPos;
        }
    }
}
