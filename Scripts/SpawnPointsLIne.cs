using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = System.Random;

public class SpawnPointsLIne : MonoBehaviour
{
    public float max_time = 10.0f;
    public float time = 0.0f;
    //uint time_mul = 0;
    GameObject firstItem;
    List<GameObject> Second = new List<GameObject>();
    public List<GameObject> SpawnPath = new List<GameObject>();
    //List<String> items;
    //List<string> distinct;
    
    void Start()
    {
        //items = new List<string>();
        
        for (int i = 0; i < SpawnPath.Count - 1; i++)
        {
            Debug.DrawLine(SpawnPath[i].transform.position, SpawnPath[i + 1].transform.position, Color.green);
        }

        InvokeRepeating("GetSpawn", 2.0f, max_time); //starting in 2 seconds, launch every 3s
    }
    public void Update()
    {
       // DistanceFromEnem();

    }

    public GameObject GetSpawn()
    {
       
        foreach (var item in SpawnPath)
        {
        
            item.SetActive(false);
        }
        SpawnPath = Shuffle(SpawnPath);
     
        int x= RandomSequence();
      
        foreach (var item in SpawnPath)
        {
           // Debug.Log(x);
            firstItem = SpawnPath[x];
            //Debug.Log(" PRIME Item: {0} "+ item.name);
           // items.Add(firstItem.name);
           // Debug.Log(firstItem.name + " " + x);
            //distinct = items.Distinct().ToList();
            //foreach (string value in distinct)
            //{
            //    Debug.Log( value);
               
            //}


            firstItem.SetActive(true); 
         
            break;          
        }  
        return firstItem ;      
    }
   
    // Update is called once per frame


    int RandomSequence()
    {
        List<int> listNumbers = new List<int>();
        int number=0;
        Random random = new Random();
        for (int i = 0; i < 9; i++)
        {
            do
            {
                number = random.Next(0, 9);
            } while (listNumbers.Contains(number));
            listNumbers.Add(number);
         
            
        }
        return number;
    }
    public static List<T> Shuffle<T>(List<T> list)
    {
      //int count = 0 - 5 + 1;
        //UnityEngine.Random rnd = new Random();
      //  int newRandomNumber = UnityEngine.Random.Range(0, 5 - 1);
        for (int i = 0; i < list.Count; i++)
        {
            int k = UnityEngine.Random.Range(0, 9);
            T value = list[k];
            list[k] = list[i];
            list[i] = value ;

        }
    
       
        //return (result < exclusion) ? result : result + 1;
        return list;
    }
    public static List<int> GetRandomNumbers(int count)
    {
        List<int> randomNumbers = new List<int>();
        Random random = new Random();
        for (int i = 0; i < count; i++)
        {
            int number;
           
            do number = random.Next();
            while (randomNumbers.Contains(number));

            randomNumbers.Add(number);
        }

        return randomNumbers;
    }
    public void Lis()
    {
        List<int> mylist = new List<int>();
        mylist.Add(1);
        mylist.Add(2);
        mylist.Add(3);
        mylist.Add(4);
        mylist.Add(5);
        mylist.Add(6);

        mylist = Shuffle(mylist);

        //foreach (var item in mylist)
        //{
        //    //Console.WriteLine(item);
        //}
       /// Console.ReadLine();
    }

  
}
