using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Enemy : MonoBehaviour
{
    Player player;
    Enemy enemy;

    private void Start()
    {
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player == null)
        {
            player = collision.GetComponent<Player>();
            player.Damaged(enemy.ed.damage);
        }
    }

    private void OnEnable() => player = null;
}
