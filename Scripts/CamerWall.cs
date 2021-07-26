using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerWall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cameraInChil;
    public GameObject Aimer;
    public GameObject CamerHolder;
    public GameObject childPosition;
    public int active;
    float distance;
    void Start()
    {
        active=0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       // Vector3 pos3 = new Vector3(0.075f, 0.44f, -0.213f);
        Vector3 pos = cameraInChil.GetComponent<Transform>().transform.position;
        Vector3 pos2 = cameraInChil.GetComponent<Transform>().transform.localPosition;
        Vector3 direction = Aimer.transform.position - pos;//camea to the aimer

        Vector3 direction7 = childPosition.transform.position - Aimer.transform.position;
        //Vector3 direction8 = Aimer.transform.position - childPosition.transform.position;

       distance = Vector3.Distance(pos, Aimer.transform.position);
        //Debug.Log("distance " + distance);
        if (active==1)
        {
            
           
                cameraInChil.GetComponent<Transform>().transform.Translate(direction *= 3f * Time.deltaTime, Space.World);
                
        }
        else if (active == 2)
        {

            if (distance <= 1.2f)
            {
                cameraInChil.GetComponent<Transform>().transform.Translate(direction7 *= 3f * Time.deltaTime, Space.World);
            }
          // eraInChil.GetComponent<Transform>().transform.localPosition = Vector3.Lerp(Aimer.transform.position, new Vector3(0.1f, 0.4f, -0.3f), 6);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "World")
        {
            active = 1;
           // transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
           
        }
    }

    public void OnTriggerExit(Collider other)
    {
        active = 2;
    }
}
