using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DestroyBulletSingleEnemy : MonoBehaviour
{
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //StartCoroutine("DesBullet");
    }

       public IEnumerator DesBullet()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
   

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("World") || other.gameObject.CompareTag("Player"))
        {
           // Destroy(this.gameObject, 0.2f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject,0.2f);
        }
    }
}
