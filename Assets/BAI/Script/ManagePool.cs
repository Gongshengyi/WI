using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePool : MonoBehaviour
{
    public GameObject prefab; // prefab
    public int poolSize = 10; // size
    private Queue<GameObject> pool;

    private void Awake()
    {
        pool = new Queue<GameObject>();

        // Initialise object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // Get Objects
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Option to extend the pool when there are no objects in the pool
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // Return objects to the pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
