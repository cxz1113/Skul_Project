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
            int rand = Random.Range(0, 101);
            MapManager.Instance.isHead = true;
            MapManager.Instance.coinParent.gameObject.SetActive(true);

            /*if(rand > 33)
            {
                MapManager.Instance.isHead = true;
                MapManager.Instance.coinParent.gameObject.SetActive(true);
            }
            else
            {
                MapManager.Instance.isGold = true;
                MapManager.Instance.goldParent.gameObject.SetActive(true);
            }*/

            MapManager.Instance.isActive = true;
        }
    }
}
