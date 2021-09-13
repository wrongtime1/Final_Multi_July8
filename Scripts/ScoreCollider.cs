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

    public void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
        xx = 0;

        

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
            xx = xx + 1;
            //Debug.Log("xx " + xx);
            //PRizeScore.instance.
            GetI(xx);
             GameManSinglePlayer.gameManSIgleInstance.Get_Total(xx,photonView.ViewID );
            //photonView.RPC("GetI", RpcTarget.All, xx);
           // Debug.Log("photonviewiD " + photonView.ViewID);
            //GameManSinglePlayer.gameManSIgleInstance.Get_Total(xx, photonView.ViewID);
            //photonView.RPC("GameManSinglePlayer.gameManSIgleInstance.Get_Total", RpcTarget.All, xx, photonView.ViewID);
          byte evCode=0;
          bool reliable = true;
       
           object[] datas = new object[]{
                xx,
                photonView.ViewID

           };
           //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        
        //PhotonNetwork.RaiseEvent(evCode, datas,)

        }

    }
  
  

    [PunRPC]
    public void GetI(int hit)
    {

        //  Debug.Log("Is  mine ");
        // Debug.Log("ID " + ID);
        // Debug.Log("this.ViewID " + this.GetComponent<PhotonView>().ViewID);
        //s = xx;
        //Debug.Log(hit);

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
