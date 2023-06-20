using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] Enemy prepab;

    void Start()
    {
        EnemySpawn();
    }

    void Update()
    {
        
    }

    public void EnemySpawn()
    {
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            Instantiate(prepab, spawnpoints[i]);
        }
    }
}
