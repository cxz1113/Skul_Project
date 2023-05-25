using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public Player player;
    void Start()
    {
        
    }

    void Update()
    {
        player = ProjectManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && player.isPush)
        {
            MapManager.Instance.isActive = false;
            MapManager.Instance.isWay = true;
            MapManager.Instance.gate.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(transform.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
