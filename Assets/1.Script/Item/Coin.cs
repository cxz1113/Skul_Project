using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : Environment
{
    public override void Initialize()
    {
        evd.obj = transform.gameObject;
    }

    void Start()
    {
        Initialize();
        GetComponent<SpriteAnimation>().SetSprite(active, 0.05f);
        Rigidbody2D rigid = evd.obj.GetComponent<Rigidbody2D>();
        int power = Random.Range(250, 450);
        float x = Mathf.Cos(90) * Mathf.Rad2Deg;
        float y = Mathf.Sin(90) * Mathf.Rad2Deg;
        rigid.AddForce(new Vector2(x, 1+ y) * 6);
        evd.obj.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
