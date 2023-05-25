using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
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
            MapManager.Instance.gold.gameObject.SetActive(true);
            MapManager.Instance.isActive = true;
        }
    }
}
