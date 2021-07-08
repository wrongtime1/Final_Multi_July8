using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPOintLines : MonoBehaviour
{
   
    public List<GameObject> ALienPath = new List<GameObject>();

    // Use this for initialization


    // Update is called once per frame
    [ExecuteInEditMode]
    void FixedUpdate()
    {
       

        for (int i = 0; i < ALienPath.Count - 1; i++)
        {
            Debug.DrawLine(ALienPath[i].transform.position, ALienPath[i + 1].transform.position, Color.green);
        }

     
    }
}
