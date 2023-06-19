using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : Projectile
{
    protected override void Init()
    {
        pd.speed = 20;
    }

    protected override void Move()
    {
        transform.Translate(Vector2.up * pd.speed * Time.deltaTime);
    }

    void Start()
    {
        Init();
        Invoke("Dest", 2);
    } 

    void Dest()
    {
        Destroy(gameObject);
    }
}
