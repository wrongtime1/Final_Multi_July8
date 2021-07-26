using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPrefabPool : MonoBehaviour, IPunPrefabPool
{
    private Queue<GameObject> pool;
    public GameObject Prefab;

   void Awake()
    {
        pool = new Queue<GameObject>();
        //PhotonNetwork.PrefabPool = this.Prefab;
    }

    // Start is called before the first frame update
  
    public void Destroy(GameObject gameObject)
    {
        gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }

    public  GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
       if(pool.Count > 0)
        {
            GameObject go = pool.Dequeue();
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.SetActive(true);

            return go;
        }
        return Instantiate(Prefab, position, rotation);
    }

}

internal interface IPunPrefabPool
{
}