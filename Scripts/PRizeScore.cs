
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;
public class PRizeScore : MonoBehaviour
{

    public List<GameObject> prize = new List<GameObject>();
    private UnityEngine.UI.Image it;
    public static PRizeScore instance;
    public GameObject WinPanel;
    //public GameObject EnemyPlayer;
    static int xx;
    static int s;
    [HideInInspector]
    public bool wonGame;
    
    // Start is called before the first frame update
    void Start()
    {
        //    photonView = GetComponent<PhotonView>();
        //    xx = 0;

        //    instance = this;

        // // prize.AddRange(GameObject.FindGameObjectsWithTag("Prize"));

        //    for (int i = 0; i < prize.Count; i++)
        //    {
        //        prize[i].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1f, .05f);

        //    }

        //   // EnemyPlayer.GetComponent<NavMeshAgent>().enabled = true;

        //}
    }

}
   // [PunRPC]
 /*   public void GetI(int hit)
    {

        //  Debug.Log("Is  mine ");
        // Debug.Log("ID " + ID);
        // Debug.Log("this.ViewID " + this.GetComponent<PhotonView>().ViewID);
        //s = xx;
        //Debug.Log(hit);
        
        switch (hit) {

            case 1:
          
               // prize[0].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
               // WinGame();
                //wonGame = true;
                break;
            case 2:
               // prize[1].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 3:
               // prize[2].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 4:
               // prize[3].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 5:
               // prize[4].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                break;
            case 6:
                //prize[5].gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
                
                break;

        }

        
    
    }
  
    public void WinGame()
    {
        //freeze the time
       // PauseGame();

        //EnemyPlayer.GetComponent<NavMeshAgent>().enabled = false;
        //display victory

      

     
    }
*/


