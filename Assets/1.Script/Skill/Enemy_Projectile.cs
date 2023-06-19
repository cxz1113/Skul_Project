using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Enemy_ProjectileData
{
    public float speed;
    public float damage;
}


public abstract class Enemy_Projectile : MonoBehaviour
{
    public int dir;

    protected Enemy_ProjectileData pd = new Enemy_ProjectileData();

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    abstract protected void Init();
    abstract protected void Move();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Damaged(pd.damage);
            Destroy(gameObject);
        }

    }
}
