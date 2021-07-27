using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreCollider : MonoBehaviour
{
    public static int xx;
    PhotonView photonView;
    public List<GameObject> prize = new List<GameObject>();

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
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
            Debug.Log("xx " + xx);
            //PRizeScore.instance.
            GetI(xx);
            // GameManSinglePlayer.gameManSIgleInstance.Get_Total();
            //photonView.RPC("GetI", RpcTarget.All, xx);
            GameManSinglePlayer.gameManSIgleInstance.Get_Total(xx, photonView.ID);
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
