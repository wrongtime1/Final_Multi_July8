using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurretBullet : MonoBehaviour
{
    // Start is called before the first frame update


    public void Update()
    {
        //if (PlayerMoveSingle.instance.hit.distance > 1.2f)
        //{
        //    this.GetComponent<SphereCollider>().enabled = false;
        //}
        //else
        //{
        //    this.GetComponent<SphereCollider>().enabled = true;
        //}

        //if (PlayerMoveSingle.instance.jump)
        //{
        //    this.GetComponent<SphereCollider>().enabled = false;
        //    this.GetComponent<Rigidbody>().detectCollisions = false;
        //   // Debug.Log("true");
        //}
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("World") || collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("hit player " + collision.transform.name);
            this.gameObject.SetActive(false);
        }
    }
}
