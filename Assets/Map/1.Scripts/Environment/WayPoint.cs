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

    void Update()
    {
        if(gameObject.tag == "WayTown")
        {
            if(MapManager.Instance.isPushWay && MapManager.Instance.isPush)
            {
                SceneManager.LoadScene(1);
            }
        }
        else if(gameObject.tag == "WayBoss")
        {
            if (MapManager.Instance.isPushWay && MapManager.Instance.isPush)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
            MapManager.Instance.isPushWay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
