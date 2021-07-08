using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimatorPr : MonoBehaviourPunCallbacks
{
    private Animator anim;
  

    
    [SerializeField]
    private PlayerMovmentController playerMovementController;
    [SerializeField]
    private GameObject btn;
    List<GameObject> activeDucks;
    void Start()
    {

       // btn = GetComponent<GameObject>();
           activeDucks = new List<GameObject>();
        //g = GameObject.FindGameObjectsWithTag("Player");

      //prefab.GetComponent<Animator>().SetLayerWeight(1, 1);
        //foreach (var item in g)
        //{
        //    if (item.activeInHierarchy)
        //    {
        //        activeDucks.Add(item);
        //        // Debug.LogWarning("the name is " + item);
        //        // Debug.LogWarning("this index is array " + activeDucks.IndexOf(item));
        //        // Debug.LogWarning("this index is array " + activeDucks.IndexOf(item));
        //        //Fire1(activeDucks.IndexOf(item));

        //    }
        //    //OnMouseEnter(activeDucks.IndexOf(item));
        //    //OnMouseEnter(activeDucks.IndexOf(item));
        //}
        //if (photonView.IsMine)
        //{
        //  //  PhotonView photonView = PhotonView.Get(this);
        //  //  playerMovementController = prefab.GetComponent<PlayerMovmentController>();

        //}
    }
    
    public void OnMouseEnter()
    {
        PlayerMovmentController.instance.Fire1();

       // btn.SetActive(true);
        //GameObject gg = GameObject.Find("PlayerPRefab(Clone)");
        //gg.GetComponent<PlayerMovmentController>().Fire1();



        //GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log("lower");
        // playerMovementController = 

        //    foreach (var item in g)
        //    {
        //        if (item.activeInHierarchy)
        //        {
        //            activeDucks.Add(item);
        //            // Debug.LogWarning("the name is " + item);
        //            // Debug.LogWarning("this index is array " + activeDucks.IndexOf(item));
        //            // Debug.LogWarning("this index is array " + activeDucks.IndexOf(item));
        //            //Fire1(activeDucks.IndexOf(item));

        //        }



        //       // g[activeDucks.IndexOf(item)].GetComponent<PlayerMovmentController>().Fire1();


        //      //  g[activeDucks.IndexOf(item)].GetComponent<Animator>().SetLayerWeight(1, 1);

        //      //  g[activeDucks.IndexOf(item)].GetComponent<AudioSource>().Play();

        //    //prefab.GetComponent<Animator>().SetLayerWeight(1, 1);
        //   // prefab.GetComponent<PhotonView>().GetComponent<Animator>().SetLayerWeight(1, 1);

        //}

    }
  
    
    public void Lower(int x)
    {
        PlayerMovmentController.instance.Lower();

       // GameObject gg = GameObject.Find("PlayerPRefab(Clone)");
        //gg.GetComponent<PlayerMovmentController>().Lower();
       // gg.GetComponent<Animator>().SetLayerWeight(1, 0);
        // playerMovementController = GameObject.FindObjectOfType<PlayerMovmentController>();

        //prefab.GetComponent<Animator>().SetLayerWeight(1, 0);
        //g[x].GetComponent<Animator>().SetLayerWeight(1, 0);

    }
} 
//anim.GetComponent<Animator>();
  // anim.transform.SetParent(prefab.transform);
  //transform.parent = transform;
  //prefab.GetComponent<Animator>().SetLayerWeight(1, 1);
  // Transform myAnim = Instantiate(prefab) as Transform;
  //myAnim.parent = transform;
  //photonView.RPC("Fire1", RpcTarget.All);
  //GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
  // Debug.Log("lower");
  //g[0].GetComponent<Animator>().SetLayerWeight(1, 0);
  //anim.GetComponent<Animator>().SetLayerWeight(1, 1);
  // anim.transform.SetParent(prefab.transform);
  // prefab.GetComponent<Animator>().SetLayerWeight(1, 0);

//anim = GetComponent<Animator>();
//  anim.GetComponent<Animator>();
// anim.gameObject.transform.SetParent(prefab.gameObject.transform);


//GameObject myAnim = Instantiate(prefab);
// anim = prefab.GetComponent<Animator>();
// GameObject myBrick = Instantiate(prefab) as GameObject;
// anim.transform.parent = transform;
//Transform myAnim = Instantiate(prefab) as Transform;
//myAnim.parent = transform;