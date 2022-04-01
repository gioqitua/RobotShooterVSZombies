using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotPowerup : MonoBehaviour
{
    [SerializeField] GameObject tripleShotEffect;
    [SerializeField] float powerUpDuration = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            StartCoroutine(TakePowerUp(other));
        }
    }

    IEnumerator TakePowerUp(Collider player)
    {
        Instantiate(tripleShotEffect, transform.position, Quaternion.identity);

        PlayerController.Controller.TripleShotActive = true;

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(powerUpDuration);

        PlayerController.Controller.TripleShotActive = false;

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
