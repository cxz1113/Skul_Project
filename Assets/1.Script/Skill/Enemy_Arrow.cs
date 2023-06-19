using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrow : Enemy_Projectile
{
    protected override void Init()
    {
        pd.speed = 25;
        pd.damage = 10;
    }

    protected override void Move()
    {
        transform.Translate(Vector2.right* dir * pd.speed * Time.deltaTime);
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
