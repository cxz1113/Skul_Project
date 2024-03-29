using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : Environment
{
    public CountCheck goldCheck;
    float time = 0;
    public override void Initialize()
    {
        evd.obj = transform.gameObject;
    }

    void Start()
    {
        Initialize();
        GetComponent<SpriteAnimation>().SetSprite(active, 0.05f);
        CoinObject();
        goldCheck = FindObjectOfType<CountCheck>();
        Invoke("GoldPickUp", 3f);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 3)
            GoldPickUp();
    }

    void CoinObject()
    {
        Rigidbody2D rigid = evd.obj.GetComponent<Rigidbody2D>();
        int rand = Random.Range(0, 101);
        float x = Random.Range(-0.3f, 0.3f);
        if (rand < 33)
        {
            rigid.mass = 0.6f;
            rigid.AddForce(new Vector2(0, 1) * 450);
        }
        else
        {
            rigid.mass = Random.Range(0.7f, 1.2f);
            rigid.AddForce(new Vector2(x, 1) * 450);
        }
        evd.obj.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void GoldPickUp()
    {
        goldCheck.goldCount++;
        Destroy(gameObject);
    }
}
