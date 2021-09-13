using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(Collider))]
public class EnemyPlayerMultiHealth : MonoBehaviour
{
    
    private NavMeshHit hit;
    private bool blocked = false;
    private Animator anim;
    private PhotonView photonView;
    EnemyMultiShooting refScript;
    static int deathCount;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
           anim = GetComponent<Animator>();
         refScript = GetComponent<EnemyMultiShooting>();
         
        //refScript.enabled = true;
    }
    public enum Injury
    {
        IsInjured1,
        IsInjured2,
        IsInjured3
    }
 
  

    [PunRPC]
  public void OnCollisionEnter(Collision other)
    {
     

        foreach (ContactPoint contact in other.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
           // Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (contact.otherCollider.tag == "Bullet")
            {
               // photonView.StartCoroutine("REsetAnimation");

                //Debug.Log("injury");
                //PhotonView.RPC("GetInjury", RpcTarget.All); 
                if (photonView.IsMine)
                {
                    
                    deathCount++;
                    if (deathCount == 4)
                    {
                        photonView.RPC("Death",RpcTarget.AllBuffered);
                    }
                    else if(deathCount <= 3)
                    {
                        PhotonNetwork.Destroy(other.gameObject);
                        photonView.RPC("GetInjury", RpcTarget.AllBuffered);
                    }
                }

                //GetInjury();
            }
          

        }

      
    }
   
    [PunRPC]
    public void GetInjury()
    {
        int random = Random.Range(0, 3);
        Debug.Log(" random " + random.ToString());
        switch (random)
        {
            case 0:
                anim.SetBool("IsInjured1", true);
                break;
            case 1:
                anim.SetBool("IsInjured2", true);
                break;
            case 2:
                anim.SetBool("IsInjured3", true);
                break;


        }
        Invoke("ReturnPose", 2f);
    }
    [PunRPC]
    public void ReturnPose()
    {
                anim.SetBool("IsInjured1", false);
      
                anim.SetBool("IsInjured2", false);
      
                anim.SetBool("IsInjured3", false);
    }

    [PunRPC]
    public void Death()
    {
        anim.SetBool("IsDeath",true);
        refScript.enabled = false;
        Invoke("ReturnEnemyTOLife", 3f);
    }
    [PunRPC]
    public void ReturnEnemyTOLife()
    {
        anim.SetBool("IsDeath", false);
        deathCount = 0;
        Invoke("InvokeDeath",10f);
         

}
    [PunRPC]
    public void InvokeDeath()
    {
        refScript.enabled = true;
    }
      
}
