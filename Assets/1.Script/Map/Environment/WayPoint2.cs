using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint2 : Environment
{
    public BoxCollider2D waycollider2D;
    public Canvas canvas;
    public Player player;
    public Enemy enemy;

    public int killCount;

    public override void Initialize()
    {

    }

    void Start()
    {
        Initialize();
        canvas.transform.GetChild(0).transform.localPosition = new Vector2(0f, 20f);
        waycollider2D = GetComponent<BoxCollider2D>();
        enemy = GetComponent<Enemy>();
        MapManager.Instance.killCheck = MapManager.Instance.enemies.Count;
        MapManager.Instance.killCount = killCount;
    }

    void Update()
    {
        player = ProjectManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.isPush && collision.CompareTag("Player"))
        {
            switch (gameObject.tag)
            {
                case "WayTown":
                    DataManager.Instance.SaveData();
                    break;
                case "WayBoss":
                    DataManager.Instance.SaveData();
                    break;
            }
        }
    }

}
