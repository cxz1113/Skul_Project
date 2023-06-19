using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint2 : Environment
{
    public BoxCollider2D waycollider2D;
    public Canvas canvas;
    public Player player;
    public Enemy enemy;
    public List<Enemy> enemies;

    public int killcount = 0;

    public override void Initialize()
    {
        evd.obj = this.gameObject;
    }

    void Start()
    {
        Initialize();
        canvas.transform.GetChild(0).transform.localPosition = new Vector2(22f, 0f);
        waycollider2D = GetComponent<BoxCollider2D>();
        enemy = GetComponent<Enemy>();
        
    }

    void Update()
    {
        player = ProjectManager.Instance.player;

        // 게이트 열림
        if (killcount == enemies.Count)
        {
            GetComponent<SpriteAnimation>().SetSprite(active, 0.2f);
            GetComponent<WayPoint2>().GetComponent<Collider2D>().enabled = true;
            killcount = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
            MapManager.Instance.isTown = true;
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
