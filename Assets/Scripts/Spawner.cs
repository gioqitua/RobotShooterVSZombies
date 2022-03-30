using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float spawnDelay = 3f;

    void Start()
    {
        InvokeRepeating("Spawn", 3f, spawnDelay);
    }
    void Spawn()
    {
        var randomNumber = Random.Range(0, spawnPoints.Length);
        var selectedSpawner = spawnPoints[randomNumber];

        var enemy = EnemyPool.Instance.Get();
        enemy.gameObject.SetActive(true);
        enemy.transform.position = selectedSpawner.transform.position;
        enemy.UpdateStatsAfterReEnebling();
    }
}