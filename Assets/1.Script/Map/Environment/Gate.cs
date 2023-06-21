using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public Player player;

    void Update()
    {
        player = ProjectManager.Instance.player;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && MapManager.Instance.spawnCount == 0)
        {
            int rand = Random.Range(0, 101);

            if(rand > 33)
            {
                MapManager.Instance.isHead = true;
                MapManager.Instance.coinParent.gameObject.SetActive(true);
            }
            else
            {
                MapManager.Instance.isGold = true;
                MapManager.Instance.goldParent.gameObject.SetActive(true);
            }

            MapManager.Instance.isActive = true;
        }
    }
}
