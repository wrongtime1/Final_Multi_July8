using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class Health : MonoBehaviourPunCallbacks
{
    
    [Header("Collection Bell")]
    public string name3;
    private Animator anim;
    private SphereCollider sphereCol;

    private static int count;

    [HideInInspector]
    PhotonView photonView;
    static int optionalParams;
    List<int> Trophy;
    LayerMask mask;

    [Header("Health Related Stuff")]
    public float startHealth = 1f;
    private float health;
    public Image healthBar;


    public static Health instance;
    void Start()
    {
        if(instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }
        healthBar.fillAmount = 1f;
        Trophy = new List<int>();
        //listPhoton = new List<int>();
        photonView = GetComponent<PhotonView>();
        count = 0;
       // Debug.Log(photonView.ViewID);
         
        //GameObject[] im = GameObject.FindGameObjectsWithTag("CollectionsObjects");

        //im[0].GetComponent<Image>().enabled = true;
        //for (int i = 0; i < images.Length; i++)
        //{
        //    Debug.Log(images[i].name);
        //    images[i].enabled = true;
        //}

        sphereCol = GetComponent<PhotonView>().GetComponent<SphereCollider>();
        anim = GetComponent<PhotonView>().GetComponent<Animator>();
    }


    [PunRPC]
   public void OnTriggerEnter(Collider other)
    {       
       PhotonView photonView;
   

      photonView = GetComponent<PhotonView>();    

        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "SingleENemyBullet")
            {
                
                optionalParams += 1;
                photonView.RPC("CallFromTrigger", RpcTarget.All, optionalParams);
                PhotonNetwork.Destroy(other.gameObject.GetComponent<PhotonView>());

                healthBar.fillAmount -= 0.2f;
                Debug.Log("health " + healthBar.fillAmount);
            }
//        if (other.gameObject.tag == "Prize")
//        {
//            //Trophy.Clear();
//            int ID = other.gameObject.GetComponent<PhotonView>().ViewID;
//            if (!Trophy.Contains(ID))
//            {

//                count+=1;

//                CollectionImageFromPlayer.instance.GetCollection(count);
//                CollectionImageFromPlayer.instance.GetFinalPoints(count);
//                FindObjectOfType<AudioManager>().Play("COllectionBell");

//                photonView.RPC("GLobalCollection",RpcTarget.AllBuffered);
//                Trophy.Add(ID);

//            }
//        }
  }
}


//[PunRPC]
void GLobalCollection()
    {
        Time.timeScale = 0;
   
        if (photonView.IsMine)
        {
           // images[0].enabled = true;
            //meObject[] im = GameObject.FindGameObjectsWithTag("CollectionsObjects");

            //m[0].GetComponent<Image>().enabled = true;
        }
    }


    [PunRPC]
    public void CallFromTrigger(int x)
    {
        if (photonView.IsMine)
        {
           // Debug.Log(x);
            if (x <= 3)
            {
                int random = Random.Range(1, 3);
                //other.gameObject.GetComponent<PhotonView>().RPC("DestroyBullet", RpcTarget.AllBuffered, other.gameObject);
                switch (random)
                {
                    case 1:
                        anim.SetInteger("injury1", 1);
                        break;

                    case 2:
                        anim.SetInteger("injury2", 1);
                        break;
                    case 3:
                        anim.SetInteger("injury3", 1);
                        break;

                }


                StartCoroutine(REsetAnimation(random));
                // PhotonNetwork.StartRpcsAsCoroutine
                //photonView.StartCoroutine(REsetAnimation(random));
                //photonView.StartCoroutine(REsetAnimation(random));
                //photonView.RPC(photonView.StartCoroutine("REsetAnimation(random)", RpcTarget.All));
                //photonView.RPC.StartCoroutine= ;
                //Invoke("REsetAnimation(random)",2);
                //  Invoke("REsetAnimation", 2.0f);
                //anim.la
            }
            else if(x >= 4)
            {
                //
                EnemyMultiEnemyInSIght.enemySightStatic.playerInSight = false;
                GameObject.Find("EnemyPlayerMulti").GetComponent<Animator>().SetLayerWeight(1, 0);
                GameObject.Find("EnemyPlayerMulti").GetComponent<Animator>().SetBool("PlayerInSightSing", false);
                GameObject.Find("EnemyPlayerMulti").GetComponent<EnemyMultiShooting>().enabled = false;

                //call death
                anim.SetBool("IsDeath", true);
                // photonView.RPC("REturnToAliveState", RpcTarget.All);
                StartCoroutine("REturnToAliveState");
            }
        }
    }


    [PunRPC]
    IEnumerator REturnToAliveState()
    {
        if (photonView.IsMine)
        {
            //anim.SetLayerWeight(1, 1);
            //anim.SetBool("PlayerInSightSing", true);
            
            //GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<Animator>().SetBool("PlayerInSightSing", false);
            //GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<Animator>().SetLayerWeight(1, 0);
            optionalParams = 0; //reset hit count
             yield return new WaitForSeconds(5.5f);
            anim.SetBool("IsDeathReverse", true);
            anim.SetBool("IsDeath", false);
            healthBar.fillAmount = 1f;
            Debug.Log(healthBar.fillAmount.ToString());
            //EnemyMultiEnemyInSIght.enemySightStatic.playerInSight = false;
            //GameObject.Find("EnemyPlayerMulti").GetComponent<Animator>().SetLayerWeight(1, 0);
            //GameObject.Find("EnemyPlayerMulti").GetComponent<Animator>().SetBool("PlayerInSightSing", false);
            GameObject.Find("EnemyPlayerMulti").GetComponent<EnemyMultiShooting>().enabled = true;

        }
    }
    

    [PunRPC]
    public IEnumerator REsetAnimation(int x)
    {
        if (photonView.IsMine)
        {
            yield return new WaitForSeconds(1.5f);
            photonView.RPC("returnTONormal", RpcTarget.All, x);
        }
    }

 [PunRPC]
 public void returnTONormal(int x)
    {
        switch (x)
        {

            case 1:
                anim.SetInteger("injury1", 0);
                break;

            case 2:
                anim.SetInteger("injury2", 0);
                break;
            case 3:
                anim.SetInteger("injury3", 0);
                break;


        }
    }
   
 [PunRPC]
 public void DestroyBullet(GameObject other)
    {
        if (photonView.IsMine)
        {
//PhotonNetwork.Destroy(other.gameObject);
        }
    }
}
