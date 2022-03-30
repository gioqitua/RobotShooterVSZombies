using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerTest : MonoBehaviour
{
    private static PoolManagerTest _instance;
    public static PoolManagerTest Instance
    {
        get
        {
            if (_instance = null)
            {
                Debug.Log("PoolManager Is Null");
            }
            return _instance;
        }
    }
    [SerializeField] GameObject objectPrefab;
    [SerializeField] GameObject prefabContainer;
    [SerializeField] List<GameObject> objectPool;
    [SerializeField] int amountOfObjcts = 10;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        objectPool = GeneratePool(amountOfObjcts);
    }
    private List<GameObject> GeneratePool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.transform.parent = prefabContainer.transform;
            obj.SetActive(false);
            objectPool.Add(obj);            
        }
        return objectPool;
    }

    public GameObject RequestObjectFromPool()
    {
        foreach (var obj in objectPool)
        {
            if (obj.activeInHierarchy == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObject = Instantiate(objectPrefab);
        newObject.transform.parent = prefabContainer.transform;
        objectPool.Add(newObject);

        return newObject;
    }


}
