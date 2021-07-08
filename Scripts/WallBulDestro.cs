using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBulDestro : MonoBehaviour
{
    // Start is called before the first frame update
  

   

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
