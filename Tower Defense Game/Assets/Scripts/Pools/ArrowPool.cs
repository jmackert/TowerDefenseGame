using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool current;
    public GameObject pooledObject;
    public int pooledAmount;

    private List<GameObject> pooledObjects;
    void Start()
    {
        current = this;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject(){
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy){
                return pooledObjects[i];
            }
        }
        GameObject obj = Instantiate(pooledObject);
        obj.SetActive(true);
        return obj;
    }
}
