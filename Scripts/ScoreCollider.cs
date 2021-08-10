using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using System;

public class ScoreCollider : MonoBehaviourPunCallbacks
{
    public int xx;
    PhotonView photonView;
    public List<GameObject> prize = new List<GameObject>();
    static float timer =0.0f;
    bool timerTrigger;
    bool exitCollider;
    public void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
        xx = 0;

        timerTrigger=true;

        // prize.AddRange(GameObject.FindGameObjectsWithTag("Prize"));

        for (int i = 0; i < prize.Count; i++)
        {
            prize[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1f, .05f);

        }

        // EnemyPlayer.GetComponent<NavMeshAgent>().enabled = true;

   
}
    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("GlobalPRize"))
        {
         
           // Debug.Log("Global pRIze");
           
           Debug.Log("timerTrigger " + timerTrigger);
           if(timerTrigger==true){
            xx = xx + 1;
            
            //Debug.Log("xx " + xx);
            //PRizeScore.instance.
            GetI(xx);
           }
             GameManSinglePlayer.gameManSIgleInstance.Get_Total(xx,photonView.ViewID );
            //photonView.RPC("GetI", RpcTarget.All, xx);
           // Debug.Log("photonviewiD " + photonView.ViewID);
            //GameManSinglePlayer.gameManSIgleInstance.Get_Total(xx, photonView.ViewID);
            //photonView.RPC("GameManSinglePlayer.gameManSIgleInstance.Get_Total", RpcTarget.All, xx, photonView.ViewID);
         
           //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        
        //PhotonNetwork.RaiseEvent(evCode, datas,)
       // GetComponentInParent<PhotonView>().RPC("Get_Total", RpcTarget.Others, xx, photonView.ViewID) ;
       
      
        }

    }
  
    void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag("GlobalPRize"))
        {
             timer=0.0f;
      timerTrigger=false;
      //if timerTrigger is set to false, then
      //activate timer
      //if timer reaches 4 second
      //set timer trigger to true;
        }

    }
  void Update(){
 //if timer is trigger to TRUE
 if(timerTrigger==false){
        timer += Time.deltaTime;

       
        
        
      
           
     }
 if(timer >=4.0f ){
            timerTrigger= true;
     Debug.Log(" timer " +  timer);
  }
  }

    [PunRPC]
    public void GetI(int hit)
    {   
        //start a timer from 0
        //if timer is greater 2 and less than 4
        //then cannot activate
        
        //countdown to 4

      
   
        switch (hit)
        {
            case 1:

               prize[0].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                // WinGame();
                //wonGame = true;
                break;
            case 2:
            prize[1].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 3:
                prize[2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 4:
             prize[3].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 5:
              prize[4].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 6:
              prize[5].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);

                break;

         }

        

    }

}
