using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class MobileFPSGameManager : MonoBehaviour
{
    [Tooltip("")]
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    public static Text strCount;
    PhotonView photonView;


    

    public static MobileFPSGameManager instance;
    void Start()
    {
       
        if (instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }

       // openPanel.SetActive(false);
        photonView = GetComponent<PhotonView>();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(playerPrefab != null)
            {
                
                int randomPoint = Random.Range(-10, 10);
                PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity);
                
                
                //  anim = GetComponent<Animator>();
               // anim.GetComponent<Animator>();
               //anim.transform.SetParent(playerPrefab.transform);
            }
            else
            {
                Debug.Log("player needs to be instantiated");
            }           
        }
        //openPanel.SetActive(true);
       
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            if(playerPrefab != null)
            {
                int randomPoint = Random.Range(-10, 10);
              Instantiate(playerPrefab, new Vector3(randomPoint, 0, randomPoint), Quaternion.identity);

            }
        }
       
    }
   
    //get victor's photon name
    [PunRPC]
  //public void GetVictor(string name)
  //  {
  //    //  Debug.Log(name);
  //      //photonView.RPC("FreezeScene", RpcTarget.AllBuffered, name);
  //     // openPanel.GetComponent<Text>().text = name + " has won the game ";
     

  //  }
    void FreezeScene()
    {
        //display game over
       // EnemyMultiAnimation.enemyAnim.enabled = false;
      //  Health.instance.enabled = false;
        // panel.SetActive(true);
        
        //display game victor
        //freeze gave play
        //restart game

    }


    [PunRPC]
    public void Restart()
    {
        Time.timeScale = 1;
    }
}
