using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint : Environment
{
    public BoxCollider2D collider2D;
    public Canvas canvas;
    public override void Initialize()
    {

    }

    void Start()
    {
        Initialize();
        collider2D = GetComponent<BoxCollider2D>();
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
        if(PlayerActivity.Instance.isPush && collision.CompareTag("Player"))
        {
            switch(gameObject.tag)
            {
                case "WayTown":
                    SceneManager.LoadScene(2);
                    DataManager.Instance.SaveData();
                    /*if(PlayerData.Instance.saveDatas == null)
                    {
                        PlayerData.Instance.SaveData();
                    }
                    else if(PlayerData.Instance.saveDatas != null)
                    {
                        PlayerData.Instance.saveDatas.Clear();
                    }*/

                    break;
                case "WayBoss":
                    SceneManager.LoadScene(3);
                    DataManager.Instance.SaveData();
                    break;
            }
        }
    }
}
