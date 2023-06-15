using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HPBar : MonoBehaviour
{
    public GameObject prfHP;
    public GameObject canvas;

    RectTransform hpBar;

    public float height;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("InGameCanvas");
        hpBar = Instantiate(prfHP, canvas.transform).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(transform.position);
        hpBar.position = hpBarPos;
    }
}
