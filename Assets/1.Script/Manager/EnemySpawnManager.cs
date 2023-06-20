using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnpoints;
    [SerializeField] Enemy prepab;
    public CountCheck enemycount;

    void Start()
    {
        EnemySpawn();
        enemycount = FindObjectOfType<CountCheck>();
        enemycount.killCount = spawnpoints.Length;
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
