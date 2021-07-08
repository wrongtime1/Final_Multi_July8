using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    [SerializeField]
    public GameObject poolObject;
    public GameObject poolObject2;
    public int poolAmount;
    public bool willGrow;
    [SerializeField]
    public List<GameObject> pooledObjects;

    private Queue<GameObject> pool;
    public GameObject Prefab;

    public PhotonView photonView;
    // Start is called before the first frame update

    void Awake()
    {
        //pool = new Queue<GameObject>();
        //PhotonNetwork.PrefabPool = this;
    }
    void Start()
    {
        current = this;
       
        for (int i = 0; i < poolAmount; i++)
        {

             GameObject obj = Instantiate(poolObject);
            Vector3 position = new Vector3(0.0f, 1.0f, 0.0f);
          // GameObject obj=   PhotonNetwork.Instantiate(poolObject.name, transform.position, Quaternion.identity);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }

    }

    //public void Practice()
    //{
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            //return pooledObjects[i];
    //            for (int ii = 0; i < poolAmount; ii++)
    //            {
    //                 pooledObjects[ii].SetActive(true); ;

    //            }
    //            //return PhotonNetwork.Instantiate(pooledObjects[i].name, transform.position, Quaternion.identity);

    //        }
    //    }

    //    for (int i = 0; i < poolAmount; i++)
    //    {
    //      //  pooledObjects[i].SetActive(true);

    //    }
    //}


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


    public GameObject GetPooledObjectPhoton()
    {
       
       
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    //return pooledObjects[i];
                    for (int ii = 0; i < poolAmount; ii++)
                    {
                       // Debug.Log("activated GamePhoton ");
                        pooledObjects[ii].SetActive(true);
                        return pooledObjects[ii];

                    }
                    //return PhotonNetwork.Instantiate(pooledObjects[i].name, transform.position, Quaternion.identity);

                }
            }

            if (willGrow)
            {
                // GameObject obj = Instantiate(poolObject);
                //  PhotonNetwork.Instantiate(obj.name, transform.position, Quaternion.identity);
                // pooledObjects.Add(obj);
                // return obj;
            }
       
            return null;
        
    }

    //public  GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    //{
    //    if (pool.Count > 0)
    //    {
    //        GameObject go = pool.Dequeue();
    //        go.transform.position = position;
    //        go.transform.rotation = rotation;
    //        go.SetActive(true);

    //        return go;
    //    }
    //    return Instantiate(Prefab, position, rotation);
    //}

    //public void Destroy(GameObject gameObject)
    //{
    //   // gameObject.SetActive(false);
    //   // pool.Enqueue(gameObject);
    //}
}
