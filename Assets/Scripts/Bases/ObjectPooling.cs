using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooling : MonoBehaviour
{
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;
    protected Queue<GameObject> objectPool;
    [SerializeField] private Transform spawnedObjectsParent;

    private void Awake()
    {

        objectPool = new Queue<GameObject>();
    }

    public void Inýtýalize(GameObject objectToPool, int poolSize)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }
    public GameObject CreateObject()
    {
        CreateObjectParent();
        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + objectPool.Count;
            spawnedObject.transform.parent = spawnedObjectsParent;
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }
    private void CreateObjectParent()
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
            {
                spawnedObjectsParent = parentObject.transform;
            }
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }
}
