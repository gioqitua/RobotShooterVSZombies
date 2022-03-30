using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;
    [SerializeField] private Enemy[] enemyPrefabs;
    private Queue<Enemy> enemysAvailable = new Queue<Enemy>();
    [SerializeField] GameObject container;

    void Awake() => Instance = this;
    public Enemy Get()
    {
        if (enemysAvailable.Count == 0)
        {
            return AddEnemy();
        }

        return enemysAvailable.Dequeue();
    }

    private Enemy AddEnemy()
    {
        var randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
        var randomEnemy = enemyPrefabs[randomIndex];
        var enemy = Instantiate(randomEnemy);
        enemy.transform.parent = container.transform;
        return enemy;
    }

    public void Return(Enemy enemy)
    {
        enemysAvailable.Enqueue(enemy);
    }

}