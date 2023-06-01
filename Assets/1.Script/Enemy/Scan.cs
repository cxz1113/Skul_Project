using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] Transform tans;

    Enemy enemy;

    public bool hit;
    private void Start()
    {
        hit = false;
    }
    void Update()
    {
        tans.position = EnemyObj.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
    }

   
}
