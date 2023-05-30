using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : Projectile
{
    protected override void Init()
    {
        pd.speed = 20;
    }

    void Start()
    {
        Init();
        Invoke("Dest", 2);
    }

    protected override void Move()
    {
        transform.Translate(Vector2.up * pd.speed * Time.deltaTime);
    }

    void Dest()
    {
        Destroy(gameObject);
    }
}
