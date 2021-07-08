using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#pragma warning disable warning

public class rioateLocal : MonoBehaviour {
    private Animator anim;
    //public Transform chest;
    //public Transform target;
    //public Transform shooter;
          
    // Use this for initialization
    void Start () {
        anim =GetComponent<Animator>();
       // chest = anim.GetBoneTransform(HumanBodyBones.Spine);
        // chest.rotation = new Quaternion(0, 12, 0,0);
    }
	
	
    public void LateUpdate()
    {
        //Debug.Log(chest.transform.rotation.ToString());

        #region mobile
        //IMPORTANT - UNCOMMENT THIS FOR MOBILE

        //  Quaternion looRootation = Quaternion.LookRotation(new Vector3(0.0f, Input.acceleration.x, Mathf.Clamp(-Input.acceleration.z, 0, 90f))); //-INput.acceleration.z
        // this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, looRootation, Time.deltaTime * 40f * 5f);
        #endregion

        //Vector3 e = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);
        //target.rotation = Quaternion.LookRotation(e);

        //Vector3 g = target.position - transform.position;

        //transform.Translate(g);
        //Vector3 lookAtS = target.position - shooter.position;
        //// shooter.transform.rotation = Quaternion.RotateTowards(shooter.transform.rotation, looRootation, Time.deltaTime * 40f * 5f);
        //shooter.localRotation = Quaternion.LookRotation(lookAtS);


        //SET THIS TO THE KEYBOARD ARROWS
        // float h = Input.GetAxis("Horizontal");
        // Quaternion turnHorizonat = Quaternion.LookRotation(new Vector3(0, h, 0f));
        //this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, turnHorizonat, Time.deltaTime * 40f * 5f);


        //Vector3 eulerAngles = chest.rotation.eulerAngles;
        //Debug.Log("transform.rotation angles x: " + Mathf.Clamp(eulerAngles.x, 2, 3)+ eulerAngles.x + " y: " + Mathf.Clamp(eulerAngles.y, 2, 3) + " z: " + eulerAngles.z);


        //Quaternion rotation = Quaternion.identity;

        // chest.eulerAngles = new Vector3(0, 30, 0);
        //     print(chest.eulerAngles.y);
        // chest.rotation.eulerAngles = new Vector3(0, 30, 0);
        // print(rotation.eulerAngles.y);
        //this.GetComponent<Transform>().rotation = new Quaternion(0, 45, 30,0);
        //this.transform.localRotation = Quaternion.Euler(Vector3.forward);
        //  Quaternion rotation = Quaternion.Euler(0, 30, 0);
        // chest.transform.Rotate(new Vector3(0, 5, 0), Space.Self);
        //chest.transform.rotation = Quaternion.Euler(new Vector3(0, 30, 0));
        //Debug.Log(chest.transform.rotation.ToString());
        //Vector3 e = new Vector3(Input.acceleration.x, 0, -Input.acceleration.z);
        // chest.rotation = Quaternion.LookRotation(e);



    }
  

    //public IEnumerator delay()
    //{
    //    Debug.Log("raised gun");
    //    anim.SetLayerWeight(1, 1);
    //    yield return new WaitForSeconds(3.5f);
    //    anim.SetBool("raiseShoot", true);
      
    //    anim.SetBool("shoot", true);
    //    Vector3 di = transform.TransformDirection(Vector3.forward) * 20f;
    //   // Debug.DrawRay(this.transform.position + adjustment, di, Color.blue);
    //    if (Physics.Raycast(transform.position, di, out hit, 25f))
    //    {
    //        Debug.Log("object hit is " + hit.collider.name);
    //        lineR.SetPosition(0, this.transform.position);
    //        lineR.SetPosition(1, di);

    //        if (hit.collider != null)
    //        {
               
    //            if (hit.collider.name == "house2")
    //            {

    //                // firebullet();
    //            }
    //        }
    //    }

    //    yield return new WaitForSeconds(0.2f);
    //}

 
   

    



}
