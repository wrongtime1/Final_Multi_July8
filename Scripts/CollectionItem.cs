using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CollectionItem : MonoBehaviourPunCallbacks
{
   

    public static int GCount = -1;
    public static bool globalDetectBool;
    public static CollectionItem GlobalDetection
    {

        get;
        private set;
    }
    public void Awake()
    {
        //    if (globalDetection == null)
        //    {
        GlobalDetection = this;
        //    }
        //    else
        //    {

        //        Destroy(this.gameObject);
        //    }
    }
    public void Start()
    {
        int GCount = -1;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public void Update()
    {
        if (globalDetectBool == true)
        {
            globalDetectBool = true;
            //   Debug.Log("GCount " + GCount);
        }
        else if (globalDetectBool == false)
        {
            globalDetectBool = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GCount = GCount + 1;
           // Invenotry.instance.GetSpheres(GCount);
            Debug.Log("GCount " + GCount);
            globalDetectBool = true;



        }
        // return this.globalDetectBool;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            globalDetectBool = false;
            //

        }
    }

    //internal class globalDetection
    //{
    //    internal class Update
    //    {
    //    }
    //}
}


 

