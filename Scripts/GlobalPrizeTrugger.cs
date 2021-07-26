using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class GlobalPrizeTrugger : MonoBehaviour
{

    public Text score;
    static int amount;
    // Start is called before the first frame update

        void Start()
    {
       
     
    }

    private void Update()
    {
       
        //    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(-3,-4, Mathf.PingPong(Time.time, 1f)), this.transform.position.z);

        

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {

            amount += 1;
          
            score.text = amount.ToString();
           
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
