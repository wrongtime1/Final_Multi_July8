using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ColliderDestroyBullet : MonoBehaviour
{
   
    Vector3 locPos = Vector3.zero;
    public GameObject hitEffectPrefa;
    private TrailRenderer tr;

    [HideInInspector]
    public PhotonView photonView;

    public LayerMask layer;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        tr = GetComponent<TrailRenderer>();
     
        tr.enabled = true;
    }

   
    public void OnTriggerEnter(Collider other)
    {

      
       // PhotonView photonView = PhotonView.Get(this);
        
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "World")
            {
                //Debug.Log("colliderDestory Bullet script");
               // photonView.RPC("DisabdleTrail", RpcTarget.All);

                StartCoroutine("CreateEffect");

               
            }

      
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "World")
        {
            //Debug.Log("colliderDestory Bullet script");
            // photonView.RPC("DisabdleTrail", RpcTarget.All);

            StartCoroutine("CreateEffect");


        }
    }


    public void DisabdleTrail()
    {
        tr.enabled = false;
        //tr.GetComponent<PhotonView>().GetComponent<TrailRenderer>().enabled = false;
     
    }

 
    public IEnumerator CreateEffect()//Vector3 position
    {
        //if (photonView.IsMine)
        //{
           // GetComponent<PhotonView>().GetComponent<Collider>().enabled = false;
            
          //  GetComponent<PhotonView>().GetComponent<Rigidbody>().detectCollisions = false;
            GameObject  hitEffectPrefa1 = Instantiate(hitEffectPrefa, this.transform.position, Quaternion.identity);
           
          
            yield return new WaitForSeconds(0.2f);
        DestBullet();
       // StartCoroutine("DestroyParticleEffect", hitEffectPrefa1);
        if (hitEffectPrefa1.activeInHierarchy)
        {
            Destroy(hitEffectPrefa1,1f);
        }
    }

    // [PunRPC]
    public void DestBullet()
    {
        Destroy(this.gameObject);
       
    }
         
    //public IEnumerator DestroyParticleEffect(GameObject hitEffectPrefa1)
    //{

     
    //    yield return new WaitForSeconds(1);
    //    Destroy(hitEffectPrefa1);

    //}

    }


//photonView.RPC("DestBullet", RpcTarget.All);
//PlayerMovmentController.instance.CreateEffect(other.transform.);


// Debug.LogWarning("Player hit");
//other.gameObject.GetPhotonView().tag == "Player"
// PhotonView photonView = PhotonView.Get(this);
//    //Debug.Log(other.gameObject.ToString());
//Destroy(this.gameObject);
//    // (other.gameObject);


//StartCoroutine(PlayerMovmentController.instance.DestroyFromWOrld);

//StartCoroutine(Camera.main.GetComponent<myTimer>().Counter());
// photonView.RPC("DestroyBullet", RpcTarget.AllBuffered);
// other.gameObject.GetComponent<PhotonView>().RPC("DestroyBullet", RpcTarget.AllBuffered);
//photonView.RPC("DestoryBullet", RpcTarget.AllBuffered);
//if (Physics.Raycast(transform.position, transform.forward, out hit))
//{
//    Debug.Log("Point of contact: " + hit.point);

//    locPos = transform.InverseTransformPoint(hit.point);
//    //GameObject hitEffectPrefa1 = PhotonNetwork.Instantiate("hitEffectPrefa", locPos, Quaternion.identity);
//    //GameObject hitEffectPrefa1 = PhotonNetwork.Instantiate("hitEffectPrefa", hit.point, Quaternion.identity);

//}
//{
//    PhotonView photonView = PhotonView.Get(this);
//    Debug.Log("Point of contact " + hit.point + " hit point " + hit.collider.name);
//    GameObject hitEffectPrefa1 = PhotonNetwork.Instantiate("hitEffectPrefa", hit.point, Quaternion.identity);
//    Destroy(hitEffectPrefa1, 0.5f);

//    //PlayerMovmentController.instance.DestroyFromWOrld(hit.point);

//public void OnCollisionEnter(Collision other)
//{

//    if (other.gameObject.tag == "Player" || other.gameObject.tag == "World")
//    {
//        GameObject hitEffectPrefa1 = PhotonNetwork.Instantiate("hitEffectPrefa", locPos, Quaternion.identity);

//        ContactPoint contact = other.contacts[0];
//            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
//            Vector3 pos = contact.point;
//            Destroy(hitEffectPrefa1, 2f);

//    }
//}

////[PunRPC]
////public void DestroyNonTargetBullet()
////{
////    if (PhotonNetwork.IsMasterClient || photonView.IsMine)
////    {
////        Debug.Log("called from DestroyCROutineBullet");

////      //  StartCoroutine("DestroyCROutineBullet");
////    }
////}

////public IEnumerator DestroyCROutineBullet()
////{
////    yield return new WaitForSeconds(1.5f);
////    Debug.Log("called from co routine");
////    Destroy(this.gameObject);
////}

////[PunRPC]
////public void DestroyBullet()
////{

////    if (PhotonNetwork.IsMasterClient || photonView.IsMine)
////    {
////        PhotonNetwork.Destroy(this.gameObject);
////    }
////}

//public Transform explosionPrefab;
//void OnCollisionEnter(Collision collision)
//{
//    ContactPoint contact = collision.contacts[0];
//    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
//    Vector3 pos = contact.point;
//    Instantiate(explosionPrefab, pos, rot);
//    Destroy(gameObject);
//}

