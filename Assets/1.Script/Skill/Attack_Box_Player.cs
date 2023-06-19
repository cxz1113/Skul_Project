using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Box_Player :MonoBehaviour
{
    [HideInInspector] public List<Enemy> enemies = new List<Enemy>();
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !enemies.Contains(collision.GetComponent<Enemy>()))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Add(enemy);
            enemy.Damaged(player.Damage);
        }
    }

    private void OnEnable() => enemies.Clear();
}
