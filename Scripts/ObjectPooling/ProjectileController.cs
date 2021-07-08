using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update

    private void OnEnable()
    {
        //    if(rb != null)
        //{
        //    rb.velocity = Vector3.forward * 20f;
        //}
       Invoke("Disable", 2f);
    }
 
   
    void Disable()
    {
      this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "World")
        {
            this.gameObject.SetActive(false);
        }
    }
}
