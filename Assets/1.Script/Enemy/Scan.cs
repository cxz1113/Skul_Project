using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;

    KnightMove kingtscan;

    void Update()
    {
        tans.position = EnemyObj.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            kingtscan.scan = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        kingtscan.scan = false;
    }
}
