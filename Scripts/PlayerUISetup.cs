using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class PlayerUISetup : MonoBehaviourPunCallbacks
{
    //public GameObject fixedThign;
    //public GameObject rotationThing;

    public GameObject playerUIPrefab;
    private PlayerMovmentController playerMovementController;
    public Camera FPSCamera;
   
    private Animator animator;
    // Start is called before the first frame update


    void Start()
    {
       // this.transform.Find("Camera").gameObject.GetComponent<CameraControler>().enabled = true;
        animator = GetComponent<Animator>();
  
        //GameObject uiTag = GameObject.FindGameObjectWithTag("UItag");
        playerMovementController = GetComponent<PlayerMovmentController>();
        if (photonView.IsMine)
        {
            GameObject c = GameObject.Find("Camera");
            c.transform.parent = this.transform;
            c.transform.position = this.transform.position + new Vector3(0, 2, -2.5f);
            animator = GetComponent<Animator>();
            // GameObject player = GameObject.FindGameObjectWithTag("Player");
           
            GameObject playerUIGameoBJect = Instantiate(playerUIPrefab);
            // playerUIGameoBJect.transform.SetParent(player.transform);
            playerMovementController.joystick = playerUIGameoBJect.transform.Find("Fixed Joystick").GetComponent<Joystick>();
            playerMovementController.fixedTouchField = playerUIGameoBJect.transform.Find("RotationTouchField").GetComponent<FixedTouchField>();

           // playerMovementController.playerBtn = playerUIGameoBJect.transform.Find("FireButton");
            
             //playerMovementController.fireButn = uiTag.transform.Find("FireButton").GetComponentInChildren<Button>();
            //playerMovementController.fireButn = uiTag.transform.Find("FireButton").GetComponent<Button>();
            // playerMovementController.Fire1() = uiTag.transform.Find("FireButton").GetComponent<Button>();


           // FPSCamera.enabled = true;
          
            // playerMovementController.joystick = playerUIPrefab.transform.Find("Fixed Joystick").GetComponent<Joystick>();
           // playerMovementController.fixedTouchField = playerUIPrefab.transform.Find("RotationTouchField").GetComponent<FixedTouchField>();

        }
        else
        {
            playerMovementController.enabled = false;
            GetComponent<RigidbodyFirstPersonController>().enabled = false;
         //   FPSCamera.enabled = false;
        }
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
 
     //   [PunRPC]
    //public void GetIt()
    //{
    //    animator = GetComponent<Animator>();
    //    if (animator != null)
    //    {
    //        animator.SetLayerWeight(animator.GetLayerIndex("RaiseWeapon"), 1f);
    //    }
    //  //  animator = GetComponent<Animator>();
    //  //  animator.SetLayerWeight(1, 1);
    //    Debug.Log("touch it ");
    //}
}
