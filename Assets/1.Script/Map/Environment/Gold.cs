using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerActivity.Instance.isPush)
        {
            MapManager.Instance.isActive = false;
            MapManager.Instance.isWay = true;
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canvas.gameObject.SetActive(true);
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
