using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint2 : Environment
{
    public BoxCollider2D waycollider2D;
    public Canvas canvas;
    public Player player;
    public Enemy enemy;
    public CountCheck killcheck;

    bool open;
    int rand;

    public override void Initialize()
    {
        evd.obj = this.gameObject;
    }

    void Start()
    {
        rand = Random.Range(1, 3);
        Initialize();
        canvas.transform.GetChild(0).transform.localPosition = new Vector2(22f, 0f);
        waycollider2D = GetComponent<BoxCollider2D>();
        enemy = GetComponent<Enemy>();
        killcheck = FindObjectOfType<CountCheck>();
    }

    void Update()
    {
        player = ProjectManager.Instance.player;

        // 게이트 열림
        if (killcheck.killCount == 0)
        {
            open = true;
            if (open == true)
            {
                GetComponent<SpriteAnimation>().SetSprite(active, 0.2f);
                GetComponent<WayPoint2>().GetComponent<Collider2D>().enabled = true;
                killcheck.killCount = -1;
            }
            open = false;
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
                    SceneManager.LoadScene(rand);
                    break;
            }
        }
    }
}
