using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float spawnDelay = 5f;

    public float currentDelay;

    void Start()
    {
        currentDelay = spawnDelay;
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        foreach (var item in spawnPoints)
        {
            var randomNumber = Random.Range(0, spawnPoints.Length);
            var selectedSpawner = spawnPoints[randomNumber];

            var enemy = EnemyPool.Instance.Get();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = item.transform.position;
            enemy.UpdateStatsAfterReEnebling();
        }
        if (currentDelay >= 0.5f)
        {
            currentDelay -= 0.1f;
        }
        
        yield return new WaitForSeconds(currentDelay);
        StartCoroutine(Spawn());
    }


}