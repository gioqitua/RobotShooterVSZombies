using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;
    [SerializeField] private Bullet prefab;
    [SerializeField] GameObject container;
    private Queue<Bullet> bulletsAvailable = new Queue<Bullet>();

    private void Awake()
    {
        Instance = this;
    }

    public Bullet Get()
    {
        if (bulletsAvailable.Count == 0)
        {
            return AddBullet();
        }

        return bulletsAvailable.Dequeue();
    }

    private Bullet AddBullet()
    {
        var bullet = Instantiate(prefab);
        bullet.transform.parent = container.transform;
        return bullet;
    }

    public void Return(Bullet bullet)
    {
        bulletsAvailable.Enqueue(bullet);
    }
}
