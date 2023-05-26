using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint : Environment
{
    public BoxCollider2D collider2D;
    public Canvas canvas;
    public Player player;

    public override void Initialize()
    {

    }

    void Start()
    {
        Initialize();
        canvas.transform.GetChild(0).transform.localPosition = new Vector2(0f,20f);
        collider2D = GetComponent<BoxCollider2D>();
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
            MapManager.Instance.isTown = MapManager.Instance.isBoss = true;
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
        if(player.isPush && collision.CompareTag("Player"))
        {
            switch(gameObject.tag)
            {
                case "WayTown":
                    DataManager.Instance.SaveData();
                    SceneManager.LoadScene(2);
                    break;
                case "WayBoss":
                    DataManager.Instance.SaveData();
                    SceneManager.LoadScene(3);
                    break;
            }
        }
    }
}
