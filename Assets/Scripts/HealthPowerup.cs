using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPowerup : MonoBehaviour
{
    [SerializeField] GameObject HealthEffect;
    [SerializeField] float spawnDelay = 15f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StartCoroutine(TakePowerUp(other));
        }
    }

    IEnumerator TakePowerUp(Collider player)
    {
        Instantiate(HealthEffect, transform.position, Quaternion.identity);

        PlayerHealth.playerHealth += 50f;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(spawnDelay);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        transform.position = GetRandomPosition();
    }

    private Vector3 GetRandomPosition()
    {
        int x = UnityEngine.Random.Range(-30, 38);
        int z = UnityEngine.Random.Range(-40, 30);
        return new Vector3(x, 1, z);
    }
}
