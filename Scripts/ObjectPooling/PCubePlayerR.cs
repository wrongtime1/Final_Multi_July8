using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PCubePlayerR : MonoBehaviour
{
    public static PCubePlayerR instance { get; private set; }
    public bool hit;
    [SerializeField]
    private GameObject player;
    public Vector3 distance;
    [SerializeField]
    public float d;
    // Start is called before the first frame update
    public float timer = 0;
    public float timer2;
    public  float timerTester1=1;
    public  float timerTester2 = 2;
    public  float timerTester3 = 3;
    public  float timerTester4 = 4;
    public  float timerTester5 = 5;
    public  float timerTester6 = 6;
    public  float timerTester7 = 7;
    public  float timerTester8 = 8;
    public  float timerTester9 = 9;
    public  float timerTester10 = 10;
    
    public Text timerText;
    public float timer1;
    public static int amount;
    public Vector3 lastKnownPosition;
    public int ID;

    PhotonView photonView;

    Vector3 po;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        amount = 0;


        if (player == null)
        {
            return;
        }

        po = player.transform.position;

        player = GameObject.FindGameObjectWithTag("Player");




    }

    public void Update()
    {
        CalculateDistance();
      //  timer1 += Time.deltaTime;
       // Debug.Log(Mathf.Abs(timer1));
    }
    public Vector3 lasPo()
    {
        //if (po != null) 
     
        
            return po;
       
    }
    void CalculateDistance()
    {
       
        if (RotatePRizes.instance.containerBool == true)
        {
          
            d= Vector3.Distance(this.transform.position, lasPo());
           
           
            RotatePRizes.instance.CalculateTIme(d);

           // timer1 -= Time.deltaTime;
            //Debug.Log(timer1.ToString());
        }
        if (RotatePRizes.instance.containerBool==false)
        {
            timer = 0; 
         
        }
               
       }

    //void CalculateTIme(float distanceRemaining)
    //{
    
    //    //timerText.text = timerTester.ToString();
    //    if (distanceRemaining >= 90 && distanceRemaining < 100)
    //    {
    //     //  timer1 = 10f;
    //  RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 10;
    //    //    timerTester10 -= Time.deltaTime;
    //   //Debug.Log(" timerTester10 " + timerTester10);
    //    //    timerText.text = Mathf.Abs(Mathf.Round(timerTester10)).ToString();
    //    //   // Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //     // timerTester10 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 80 && distanceRemaining < 90)
    //    {

    //       // timer1 = 9f;
    //       RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 9;

    //       // timerTester9 -= Time.deltaTime;
    //      //Debug.Log(" timerTester9 " + timerTester9);
    //       // timerText.text = Mathf.Abs(Mathf.Round(timerTester9)).ToString();
    //       //// Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      //timerTester9= Mathf.Abs(Mathf.RoundToInt(0));
    //    }
    //    if (distanceRemaining >= 70 && distanceRemaining < 80)
    //    {
    //        RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 8;
    //       // timer1 = 8f;
    //      //  timerTester8 -= Time.deltaTime;
    //     //Debug.Log(" timerTester8 " + timerTester8);
    //      //  timerText.text = Mathf.Abs(Mathf.Round(timerTester8)).ToString();

    //      // // Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester8 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 60 && distanceRemaining < 70)
    //    {
    //      RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 7;
    //    // timer1 = 7f;
    //     //   timerTester7 -= Time.deltaTime;
    //     //Debug.Log(" timerTester7 " + timerTester7);
    //     //   timerText.text = Mathf.Abs(Mathf.Round(timerTester7)).ToString();
    //     //  // Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //     //  timerTester7 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 50 && distanceRemaining < 60)
    //    {
    //     RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 6;
    //      //  timer1 = 6f;
    //      //  timerTester6 -= Time.deltaTime;
    //      //Debug.Log(" timerTester6 " + timerTester6);
    //      //  timerText.text = Mathf.Abs(Mathf.Round(timerTester6)).ToString();
    //      // // Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester6 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 40 && distanceRemaining < 50)
    //    {
    //     RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 5;
    //      // timer1 = 5f;
    //       // timerTester5 -= Time.deltaTime;
    //     //Debug.Log(" timerTester5 " + timerTester5);
    //       // timerText.text = Mathf.Abs(Mathf.Round(timerTester5)).ToString();
    //       //// Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester5 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >=30 && distanceRemaining < 40)
    //    {
    //     RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 4;
    //      //timer1 = 4f;
    //       // timerTester4 -= Time.deltaTime;
    //      /// Debug.Log(" timerTester4 " + timerTester4);
    //       // timerText.text = Mathf.Abs(Mathf.Round(timerTester4)).ToString();
    //       //// Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //     //  timerTester4 =Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 20 && distanceRemaining < 30)
    //    {
    //       RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 3;
    //       // timer1 = 3f;
    //       // timerTester3 -= Time.deltaTime;
    //        Debug.Log(" timerTester3 " + timerTester3);
    //       // timerText.text = Mathf.Abs(Mathf.Round(timerTester3)).ToString();
    //       //// Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester3 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining >= 10 && distanceRemaining < 20)
    //    {
    //       RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 2;
    //      // timer1 = 2f;
    //       // timerTester2 -= Time.deltaTime;
    //       Debug.Log(" timerTester2 " + timerTester2);
    //       // timerText.text = Mathf.Abs(Mathf.Round(timerTester2)).ToString();
    //       //// Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester2 = Mathf.Abs(0);
    //    }
    //    if (distanceRemaining > 1 && distanceRemaining < 10)
    //    {
    //      RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal = 1;
    //     // timer1 = 1f;
    //        //timerTester1 -= Time.deltaTime;
    //        Debug.Log(" timerTester1 " + timerTester1);
    //        //timerText.text = Mathf.Abs(Mathf.Round(timerTester1)).ToString();
    //        ////Debug.Log(timerText.text);
    //    }
    //    else
    //    {
    //      // timerTester1 = Mathf.Abs(0);
    //    }

    //   // Debug.Log("from pcd " + RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal);
    //}
    public  void CheckBool(bool value)
    {
        if (RotatePRizes.instance.containerBool || RotatePRizes.instance.containerBoolTWO)
        {
           
           Vector3 direction = this.player.transform.position - this.transform.position;
         float d = Vector3.Distance(this.transform.position, direction);
    //   Debug.Log(d);
    // FindObjectOfType<RotatePRizes>().distance = d;

        }
    }
   //private void OnTriggerEnter(Collider other)
   // {
   //     // PhotonView photonView = GetComponent<PhotonView>();
   //     //PhotonView photonView = GetComponent<PhotonView>();
      
       
   //     //if (other.gameObject.CompareTag("Player"))
   //     //{
   //     //    //int viewid = hit.collider.GetComponent<PhotonView>().viewID;
   //     //    //ID = other.GetComponent<Collider>().GetComponent<PhotonView>().ViewID;

          
   //     //    //Get the player's position
   //     //    //lastKnownPosition = player.transform.position;



   //     //    GetComponentInChildren<Transform>().gameObject.SetActive(false);



   //     //        this.GetComponent<SphereCollider>().enabled = false;
   //     //        // this.GetComponent<SphereCollider>().isTrigger = false;
   //     //        this.GetComponent<MeshRenderer>().enabled = false;
   //     //        amount += 1;
   //     //   // photonView.RPC("PRizeScore.instance.GetI", RpcTarget.AllBuffered, amount, ID);
   //     //   // PRizeScore.instance.GetI(amount,ID);
   //     //      //
            
   //     //}
        
   // }
}
