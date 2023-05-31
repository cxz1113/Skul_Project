using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileData
{
    public float speed;
    public float damage;
}

public abstract class Projectile : MonoBehaviour
{
    protected ProjectileData pd = new ProjectileData();

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    abstract protected void Init();
    abstract protected void Move();

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);

    }
}
