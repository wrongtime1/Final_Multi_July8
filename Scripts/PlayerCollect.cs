using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

//For SINGLE GAME  player collection
public class PlayerCollect : MonoBehaviour
{
    [Header("Public")]
    public Image[] spheres;
   
  
    [Header("Private")]
    [TextArea(1,1)]
  
    private static int collectPrize;
    private SphereCollider sphereCol;

    public static PlayerCollect instance;


    public void Awake()
    {
        if (instance != null)
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
        collectPrize = 0;
        sphereCol = GetComponent<SphereCollider>();
        //spheres = GetComponents<SphereCollider>();

        //for (int i = 0; i < spheres.Length; i++)
        //{
        //    Debug.Log(spheres[i].name);
       
        //}
        
      // var tempColor = spheres[0].color;
      // tempColor.a = 1f;
        //spheres[0].color = tempColor;
    }

    // Update is called once per frame
   public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prize"))
        {
            collectPrize++;
            CollectingVisible(collectPrize);
          
        }
    }

    public void CollectingVisible(int x)
    {

        Debug.Log("Collecting " + x.ToString());
        switch (x)
        { 
            
            case 1:
                //spheres[0].color = new Color(spheres[0].color.r, spheres[0].color.g, spheres[0].color.b, 1f);
                var tempColor = spheres[0].color;
                tempColor.a = 1f;
                spheres[0].color = tempColor;
               
                break;

            case 2:
                tempColor = spheres[1].color;
                tempColor.a = 1f;
                spheres[1].color = tempColor;
                break;

            case 3:
                tempColor = spheres[2].color;
                tempColor.a = 1f;
                spheres[2].color = tempColor;
                break;

            case 4:
                tempColor = spheres[3].color;
                tempColor.a = 1f;
                spheres[3].color = tempColor;
                break;

            case 5:
                tempColor = spheres[4].color;
                tempColor.a = 1f;
                spheres[4].color = tempColor;
                break;
            case 6:
                tempColor = spheres[5].color;
                tempColor.a = 1f;
                spheres[5].color = tempColor;
                break;

        }
    }
    //public static T ChangeAlpha<T>(this T g, float newAlpha)
    //    where T : Graphic
    //{
    //    var color = g.color;
    //    color.a = newAlpha;
    //    g.color = color;
    //    return g;
    //}
}
