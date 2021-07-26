using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;
using UnityEngine.Assertions.Must;
using UnityEngine.Analytics;
using UnityEngine.UI;


//using System.Diagnostics;

public static class Shuf
{
    private static Random rng = new Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class RotatePRizes : MonoBehaviour
{
    [HideInInspector]
    public GameObject containers;
    public List<GameObject> globalTransforms;   
    // public var prize;
    public static RotatePRizes instance;  
    private GameObject GunSpawn;
    public  bool containerBool;
    public bool containerBoolTWO; 
    public float distance;
   // [SerializeField]
   // public float COROdistanceFromGlobeToPlayer;
    [SerializeField]
    public float COROdistanceFromGlobeToPlayerLocal;
    [HideInInspector]

    public float timer1;
    public float timer2;
    public float timer3;
    public float timer4;
    public float timer5;
    public float timer6;
    public float timer7;
    public float timer8;
    public float timer9;
    public float timer10;

    public bool activate;

    
    [SerializeField]
    public Text TimerText;
    [SerializeField]
    public Text CorDristanceFromPlayer;

  
    // Start is called before the first frame update
    int number;

    public GameObject disc;
    public Animator animPlayer;
    void Start()
    {
     
        if (instance == null)
        {
            instance = this;
        }
       
        var globalTransforms = new List<GameObject>();
        distance = 0;

        timer1=3f;
        timer2=3f;
        timer3=5f;
        timer4=6f;
        timer5=7f;
        timer6=8f;
        timer7=9f;
        timer8=11f;
        timer9=12f;
        timer10=13f;

    // timerTester = 10f;
    InitalShuffle();
    }

 
    void InitalShuffle()
    {
        Shuf.Shuffle(globalTransforms);

        foreach (var item in globalTransforms)
        {
            string name = item.GetComponent<Transform>().name;
            item.SetActive(false);
            item.GetComponent<MeshRenderer>().enabled = false;
            item.GetComponent<SphereCollider>().enabled = false;
        
        }
      
        DisplayFirstPrize(globalTransforms);
    }
    void DisplayFirstPrize(List<GameObject> prize1)
    {

        Random rnd = new Random();
        var next = rnd.Next(prize1.Count);
        var next2 = rnd.Next(prize1.Count);
        GunSpawn = prize1[next];
    
        if (GunSpawn == prize1[next])
        {
            GunSpawn = prize1[next2];
           
            containerBool = true;


            DisplayFirstPrize1(GunSpawn);
        }

    }

    public void CalculateTIme(float distanceRemaining)
    {
     
        
        activate = true;
        //timerText.text = timerTester.ToString();
        if (distanceRemaining >= 90 && distanceRemaining < 100)
        {
            //  timer1 = 10f;
            COROdistanceFromGlobeToPlayerLocal = 13f;
        
        }
     
       else if (distanceRemaining >= 80 && distanceRemaining < 90)
        {

            // timer1 = 9f;
            COROdistanceFromGlobeToPlayerLocal = 12f;

         
        }
     
        else if (distanceRemaining >= 70 && distanceRemaining < 80)
        {
            COROdistanceFromGlobeToPlayerLocal = 11f;
          
        }
     
       else if (distanceRemaining >= 60 && distanceRemaining < 70)
        {
            COROdistanceFromGlobeToPlayerLocal = 9f;
       
        }
      
       else if (distanceRemaining >= 50 && distanceRemaining < 60)
        {
            COROdistanceFromGlobeToPlayerLocal = 8f;
        
        }
      
       else if (distanceRemaining >= 40 && distanceRemaining < 50)
        {
            COROdistanceFromGlobeToPlayerLocal = 7f;
        
        }
      
       else if (distanceRemaining >= 30 && distanceRemaining < 40)
        {
            COROdistanceFromGlobeToPlayerLocal = 6f;
            //timer1 = 4f;
            // timerTester4 -= Time.deltaTime;
            /// Debug.Log(" timerTester4 " + timerTester4);
            // timerText.text = Mathf.Abs(Mathf.Round(timerTester4)).ToString();
            //// Debug.Log(timerText.text);
        }
     
       else if (distanceRemaining >= 20 && distanceRemaining < 30)
        {
            COROdistanceFromGlobeToPlayerLocal = 5f;
            
        }
       
        if (distanceRemaining >= 10 && distanceRemaining < 20)
        {
            COROdistanceFromGlobeToPlayerLocal =3f;
      
        }
       
       else if (distanceRemaining >= 0 && distanceRemaining < 10)
        {
            COROdistanceFromGlobeToPlayerLocal = 3f;
            // timer1 = 1f;
            //timerTester1 -= Time.deltaTime;
            // Debug.Log(" timerTester1 " + timerTester1);
            //timerText.text = Mathf.Abs(Mathf.Round(timerTester1)).ToString();
            ////Debug.Log(timerText.text);
        }
      

        // Debug.Log("from pcd " + RotatePRizes.instance.COROdistanceFromGlobeToPlayerLocal);
    }
    public void Rearage(GameObject x)
    {
        timer1 = 0f;
        timer2 = 0f;
        timer3 = 0f;
        timer4 = 0f;
        timer5 = 0f;
        timer6 = 0f;
        timer7 = 0f;
        timer8 = 0f;
        timer9 = 0f;
        timer10 = 0f;
        globalTransforms.Remove(x);
      
        x.SetActive(false);
        x.GetComponent<MeshRenderer>().enabled = false;
        x.GetComponent<SphereCollider>().enabled = false;
        DisplayFirstPrize1(x);
    }
    public void DisplayFirstPrize1(GameObject prize1)
    {
        globalTransforms.Add(prize1);

        List<GameObject> uniqueLst = globalTransforms.Distinct().ToList();
    

        Random rnd = new Random();
        var third = new List<GameObject>();

        Shuf.Shuffle(uniqueLst);
        foreach (var item in uniqueLst)
        {
        
            if (prize1 != item)
            {

                third.Add(item);
                item.SetActive(true);
             
                item.GetComponent<SphereCollider>().enabled = true;
                item.GetComponent<MeshRenderer>().enabled = true;
                // item.gameObject.GetComponentInChildren<GameObject>().SetActive(true);
                // item.transform.GetChild(1).gameObject.SetActive(false);
                item.GetComponentInChildren<Transform>().gameObject.SetActive(true);
                containerBool = true;
                StartCoroutine("TimerPRize", item);
             
                return;

            }
        }
    }
    public IEnumerator TimerPRize(GameObject x)
    {
      //  Debug.Log("COROdistanceFromGlobeToPlayerLocal After " + COROdistanceFromGlobeToPlayerLocal);
        CorDristanceFromPlayer.text = COROdistanceFromGlobeToPlayerLocal.ToString();
          yield return new WaitForSecondsRealtime(COROdistanceFromGlobeToPlayerLocal);
        containerBool = false;
        // yield return new WaitForSeconds(1);
        Rearage(x);
    }

    void FixedUpdate()
    {
       
        if (activate == true)
        {
            if (COROdistanceFromGlobeToPlayerLocal == 13f)
            {

                //int x10 = (int)timer10;
                decimal x10 = (decimal)timer10;
                x10 = Math.Round(x10,1);
                x10 = Math.Abs(x10);
                timer10 += Time.deltaTime;
                TimerText.text = x10.ToString();
               
               
            }
            if (COROdistanceFromGlobeToPlayerLocal == 12f)
            {
                decimal x9 = (decimal)timer9;
                x9 = Math.Round(x9, 1);
                x9 = Math.Abs(x9);
                timer9 += Time.deltaTime;
                TimerText.text = x9.ToString();
                // Debug.Log(Mathf.Abs(timer9));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 11f)
            {
                decimal x8 = (decimal)timer8;
                x8 = Math.Round(x8, 1);
                x8 = Math.Abs(x8);
                timer8 += Time.deltaTime;
                TimerText.text = x8.ToString();
                //Debug.Log(Mathf.Abs(timer8));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 9f)
            {
                decimal x7 = (decimal)timer7;
                x7 = Math.Round(x7, 1);
                x7 = Math.Abs(x7);
                timer7 += Time.deltaTime;
                TimerText.text = x7.ToString();
                // Debug.Log(Mathf.Abs(timer7));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 8f)
            {
                decimal x6 = (decimal)timer6;
                x6 = Math.Round(x6, 1);
                x6 = Math.Abs(x6);
                // int x6 = (int)timer6;
                timer6 += Time.deltaTime;
                TimerText.text = x6.ToString();
                // Debug.Log(Mathf.Abs(timer6));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 7f)
            {
                decimal x5 = (decimal)timer5;
                x5 = Math.Round(x5, 1);
                x5 = Math.Abs(x5);
                //int x5 = (int)timer5;
                timer5 += Time.deltaTime;
                TimerText.text = x5.ToString();
                // Debug.Log(Mathf.Abs(timer5));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 6f)
            {
                decimal x4 = (decimal)timer4;
                x4 = Math.Round(x4, 1);
                x4 = Math.Abs(x4);
                timer4 += Time.deltaTime;
                TimerText.text = x4.ToString();
                // Debug.Log(Mathf.Abs(timer4));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 5f)
            {
                decimal x3 = (decimal)timer3;
                x3 = Math.Round(x3, 1);
                x3 = Math.Abs(x3);
                timer3 += Time.deltaTime;
                TimerText.text = x3.ToString();
                ///Debug.Log(Mathf.Abs(timer3));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 3f)
            {
                decimal x2 = (decimal)timer2;
                x2 = Math.Round(x2, 1);
                x2 = Math.Abs(x2);
                timer2 += Time.deltaTime;
                TimerText.text = x2.ToString();
                // Debug.Log(Mathf.Abs(timer2));
            }
            if (COROdistanceFromGlobeToPlayerLocal == 3f)
            {
                decimal x1 = (decimal)timer1;
                x1 = Math.Round(x1, 1);
                x1 = Math.Abs(x1);
                //int x1 = (int)timer1;
                timer1 += Time.deltaTime;
                TimerText.text = x1.ToString();
                // Debug.Log(Mathf.Abs(timer1));
            }
        }
  

    }

    #region one
    //void InitalShuffle1(GameObject prize1)
    //{


    //    if (GunSpawn.name != prize1.name)
    //    {
    //        Debug.Log(" FIRST  GunSpawn" + GunSpawn);
    //        foreach (var item in globalTransforms)
    //        {

    //            //string name = item.GetComponent<Transform>().name;
    //            item.SetActive(false);
    //            item.GetComponent<MeshRenderer>().enabled = false;
    //            item.GetComponent<SphereCollider>().enabled = false;

    //        }
    //        DisplayFirstPrize(globalTransforms);
    //    }
    //    else if(GunSpawn.name == prize1.name)
    //    {


    //        InitalShuffle();
    //    }
    //}
    #endregion
   

   



}

