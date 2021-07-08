using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CollectionImageFromPlayer : MonoBehaviour
{
    [SerializeField]
    PhotonView photonView;

    [HideInInspector]
    public Image[] images;

    [Header("instance")]
    public static CollectionImageFromPlayer instance;
    // Start is called before the first frame update
    public GameObject panel;
    void Awake()
    {
        if(instance != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        photonView = GetComponent<PhotonView>();
          images = GetComponentsInChildren<Image>();

      
    }

    //Get value from Health counter
    //count will be used to determine value of collected objects
    //public void GetCollection(int x)
    //{
    //    images = GetComponentsInChildren<Image>();

    //    switch (x)
    //    {
    //        case 1:
    //            images[0].enabled = true;
    //            break;
    //        case 2:
    //            images[1].enabled = true;
    //            break;
    //        case 3:
    //            images[2].enabled = true;
    //            break;
    //        case 4:
    //            images[3].enabled = true;
    //            break;
    //        case 5:
    //            images[4].enabled = true;
    //            break;
    //    }
        
    //}

    [PunRPC]
    public void GetFinalPoints(int points)
    {

       
            GameObject.FindGameObjectWithTag("WinnerCountTage").GetComponent<Text>().text = points.ToString();

            // strCount.text = points.ToString();
            if (points == 1)
            {
                EnemyMultiAnimation.enemyAnim.enabled = false;
                Health.instance.enabled = false;
                panel.SetActive(true);
       
        
            photonView = GetComponent<PhotonView>();
          
        }
        }
 
        public void StopTimeFIrst()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("StopTime", RpcTarget.AllBuffered);
        }
    }

    [PunRPC] public void StopTime()
    {
        Debug.Log("and it was here");
     
    }
}
