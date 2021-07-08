using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    // Photon photon;
    PhotonView photon;

    void Start()
    {
        photon = GetComponent<PhotonView>();
    }
    private void OnEnable() //when an object is enabled ar set active
    {
        // transform.GetComponent<Rigidbody>().WakeUp();

        Invoke("Hidebullet", 2.0f);
        // StartCoroutine("Starts");
        photon.RPC("Hidebullet", RpcTarget.All);
    }

    [PunRPC]
    void Hidebullet()
    {
        if (photon.IsMine)
        {
            photon.gameObject.SetActive(false);
           // gameObject.SetActive(false);
        }
    }

    public IEnumerator Starts()
    {
        yield return new WaitForSeconds(2.0f);
     
    }
    [PunRPC]
    public void Callit()
    {
       // gameObject.SetActive(false);
       // photon.RPC("Callit", RpcTarget.All);

    }
    [PunRPC]
     public IEnumerator Finish()
    {
        yield return new WaitForSeconds(1.0f);
        //make rpc call
        // photon.RPC("Callit", RpcTarget.All);
        StopCoroutine("Starts");
    }
    private void OnDisable()
    {
        CancelInvoke("Hidebullet");
        transform.GetComponent<Rigidbody>().Sleep();

    }
}
