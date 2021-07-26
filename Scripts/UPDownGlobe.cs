using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UPDownGlobe : MonoBehaviour
{
    public GameObject[] globes;
    [HideInInspector]
    public float g;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < globes.Length - 1; i++)
        {
            Debug.DrawLine(globes[i].transform.position, globes[i+1].transform.position, Color.blue);
        }
   

        foreach (var item in globes)
        {
            
            item.transform.position = new Vector3(item.transform.position.x, Mathf.Lerp(-2, 1, Mathf.PingPong(Time.time, 1)), item.transform.position.z);
           
        }

    }


}
