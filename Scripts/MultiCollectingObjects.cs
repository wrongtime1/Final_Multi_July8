using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.UI;

//[RequireComponent(typeof())]
public class MultiCollectingObjects : MonoBehaviour
{   
    [HideInInspector]
    public PhotonView photonView;

    [Header("Public")]
    //public Transform collection = null;
    private GameObject [] prizeImages;

    [Header("Private")]
    static int x;
    private List<string> transforList;
    
    private List<string> distinct ;
    private List<GameObject> spherePrize;

    private AudioManager audio;
    public Text text;
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView.IsMine)
        {
            transforList = new List<string>();
            prizeImages = GameObject.FindGameObjectsWithTag("ImagePrizes");
            spherePrize = GameObject.FindGameObjectsWithTag("Prize").ToList();
            //photonView = GetComponent<PhotonView>();
            audio = FindObjectOfType<AudioManager>();
           // audio.Play("COllectionBell");
        
        }
          //Get audio bell from audiomanager
    }
       
 
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (!photonView.IsMine)
    //    {
    //        return;
    //    }
    //       else if (photonView.IsMine)
    //    {
    //        photonView = GetComponent<PhotonView>();
    //      string photonName = photonView.name;
    //        //Debug.Log("Name of this game " + photonName);
    //        // text.text = "click this";
    //        if (other.GetComponent<PhotonView>().GetComponent<Collider>().tag == "Prize")
    //        {
    //            //Debug.Log(" PRize" + other.GetComponent<PhotonView>().GetComponent<Collider>().tag.ToString());
               
    //            transforList.Add(other.GetComponent<PhotonView>().transform.name);
    //            //Debug.Log(" click it 0 ");
           
    //            distinct = transforList.Distinct().ToList();
    //            //after
               
    //            foreach (string value in distinct)
    //            {
    //             //   Debug.Log(" click it 1 ");
    //                for (int i = 0; i < prizeImages.Length; i++)
    //                {
    //                    // Debug.Log(prizeImages[0 + i]);

    //                   // Debug.Log(" click it 2");
    //                    switch (distinct.Count)
    //                    {
    //                        case 1://distinct count 1
    //                            prizeImages[0].GetComponent<Image>().enabled = true;
    //                            // Debug.Log(prizeImages[0]);
    //                           // MobileFPSGameManager.instance.GetVictor(photonName);
    //                            //audio.Play("COllectionBell");
    //                            break;
    //                        case 2:
    //                            prizeImages[1].GetComponent<Image>().enabled = true;
    //                           // Debug.Log(prizeImages[0]);
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 3:
    //                            prizeImages[2].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 4:
    //                            prizeImages[3].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 5:
    //                            prizeImages[4].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 6:
    //                            prizeImages[5].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 7:
    //                            prizeImages[6].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 8:
    //                            prizeImages[7].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
    //                            break;
    //                        case 9:
    //                            prizeImages[8].GetComponent<Image>().enabled = true;
    //                            audio.Play("COllectionBell");
                               
    //                            break;

    //                    }

                       
    //                }
    //            }
    //        }
    //    }
    //}


  
}
