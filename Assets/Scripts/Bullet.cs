using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 1500f;
    [SerializeField] float damage = 3f;
    [SerializeField] ParticleSystem hit;


    private void OnEnable()
    {
        Invoke("Hide", 1.5f);
    }
    private void Hide()
    {
        this.gameObject.SetActive(false);
        BulletPool.Instance.Return(this);
    }
    public void Launch(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = direction * bulletSpeed * Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        BulletPool.Instance.Return(this);
        gameObject.SetActive(false);

        if (collision.gameObject.TryGetComponent(out Enemy currentEnemy))
        {
            currentEnemy.TakeHit(damage);
            Instantiate(hit, this.transform.position, Quaternion.identity);
        }

    }
}
