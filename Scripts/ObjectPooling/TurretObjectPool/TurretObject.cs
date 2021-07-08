using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Effects;

public class TurretObject : MonoBehaviour
{
    public static TurretObject currentTurrent;
    public GameObject poolObject;
    public int poolAmount;
    public bool willGrow;

    public List<GameObject> pooledObjects;

    // Start is called before the first frame update
    void Start()
    {
        currentTurrent = this;

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = Instantiate(poolObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

        
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = Instantiate(poolObject);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }
    // Update is called once per frame
 
}
