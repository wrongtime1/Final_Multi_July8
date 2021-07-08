using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampHipsENemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  

        Vector3 currentRotation = this.transform.localRotation.eulerAngles;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -1f, 1f);
       this.transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
