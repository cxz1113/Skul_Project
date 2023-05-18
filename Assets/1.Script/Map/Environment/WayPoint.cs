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
        if(gameObject.tag == "WayTown" && MapManager.Instance.isTown && MapManager.Instance.isPush && !MapManager.Instance.isBoss)
        {
            SceneManager.LoadScene(1);
        }
        else if(gameObject.tag == "WayBoss" && MapManager.Instance.isBoss && MapManager.Instance.isPush && !MapManager.Instance.isTown)
        {
            SceneManager.LoadScene(2);
        }
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
}
