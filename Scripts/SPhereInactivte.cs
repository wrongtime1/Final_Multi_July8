using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SPhereInactivte : MonoBehaviour
{
   
    
    public float closeDistance = 5.0f;
    public Transform other;
   
    public void Update()
    {
       // DistanceFromEnem();
    }
    public void DistanceFromEnem()
    {
        if (other)
        {
            Vector3 offset = other.transform.position - transform.position;
            float sqrLen = offset.sqrMagnitude;

            if (sqrLen < closeDistance * closeDistance)
            {
               // print("The other transform is close to me!");
              //  Debug.Log("sqrLen " + sqrLen);
            }
        }
    }

}


