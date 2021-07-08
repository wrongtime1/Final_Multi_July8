using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SinglePlayerShoot : MonoBehaviour
{

    public Transform shooter;

    public GameObject bulletPrefab;

    public Transform enemyPlayer;
    [Header("Private")]
    private HashIDs hash;

    private Animator anim;

    [HideInInspector]
    public bool liftArm;
    public bool liftArm2;
    float currentWeight;
    float animWeight1;
    // Start is called before the first frame update
    [Header("Pooling")]
    List<GameObject> bulletList;

    Transform head;
    private AudioSource audioSource;

    [HideInInspector]
    Vector3 pos;

    private PhotonView photonView;

    Ray ray;
    RaycastHit hit;

    Scene scene;
    public GameObject poolObject;
    public List<GameObject> pooledObjects;
    public int poolAmount;
    public bool willGrow;
    public bool slowMoveHandsDown;
    public GameObject TExt;
    public Slider ChangeWeaponSLider;

    [HideInInspector]
    public bool reholster;

    public Transform rifleTransform;
    public Transform rifleHolster;
    public Transform rifleBackHolster;


    private PhotonAnimatorView photonAnim;

    AnimatorClipInfo[] m_CurrentClipInfo;
    AnimatorClipInfo[] photon_CurrentClipInfo;

    public Transform realRifle;
    public Transform masterbackCOntainer;
    public GameObject dummyRifle;

    string m_ClipName;

    private Vector3 RiflePosFire;
    private Vector3 RifleTurnFire;

    Vector3 startX;
    Vector3 startY;
    Vector3 startZ;

    public float returnHandsDownSlowly;

    Vector3 position1;
    Vector3 rotation;
    void Start()
    {
        photonView = GetComponent<PhotonView>();


        anim = GetComponent<Animator>();
        head = anim.GetBoneTransform(HumanBodyBones.Head);
        audioSource = GetComponent<AudioSource>();

        // rifleTransform.transform.position = rifleTransform.transform.position+ new Vector3(0, 0, 0);

        // Vector3 rot = new Vector3(0, Mathf.Lerp(0, 90f, 1f), 0);
        //practiceRifle.eulerAngles = rot;
        RiflePosFire = new Vector3(-0.16f, 0.398f, 0.296f);
        RifleTurnFire = new Vector3(5.104f, 86.328f, 1.415f);

        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(3);

        //CHecking now

    }

    public void LateUpdate()
    {



        if (ChangeWeaponSLider.value == 0 && slowMoveHandsDown == true)
        {
            if (currentWeight <= 0.99f)
            {
                Debug.Log("Up");
                currentWeight = Mathf.Lerp(currentWeight, 1.0f, Time.deltaTime * 6f);
                anim.SetLayerWeight(1, currentWeight);
            }

        }
        //else if (ChangeWeaponSLider.value == 0 && slowMoveHandsDown == false)
        //{
        //    Debug.Log("Down");
        //     // float currentWeight1 = Mathf.Lerp(1.0f, currentWeight, Time.deltaTime * 6f);
        //      //  anim.SetLayerWeight(1, currentWeight1);

        //}


        bool d = anim.GetBool("IsShooting");
        if (liftArm || d == true)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Quaternion rotation = Quaternion.LookRotation(fwd);
            head.transform.rotation = rotation;

        }


    }

    //this function is directly called from the ButtonGraph
    public void PlayerShooting()
    {

        if (ChangeWeaponSLider.value == 0)
        {
            liftArm = true;
            liftArm2 = false;
            slowMoveHandsDown = true;
            Invoke("LaunchBullet", 1f);

            photonView.RPC("ChangeWeapons", RpcTarget.All);
        }

        if (ChangeWeaponSLider.value == 1)
        {

            slowMoveHandsDown = false;
            anim.SetBool("IsShooting", true);

            photonView.RPC("RotateRifleFire", RpcTarget.All);
            // photonView.StartCoroutine(RotateRifleFire());
            Invoke("LaunchBullet", 1.5f);

        }
    }


    public void PlayerNotShooting()
    {

        if (ChangeWeaponSLider.value == 0)
        {
            currentWeight = 0;
            slowMoveHandsDown = false;
            CancelInvoke("LaunchBullet");
            liftArm = false;
            anim.SetLayerWeight(1, 0);
        }

        if (ChangeWeaponSLider.value == 1)
        {

            anim.SetBool("IsShooting", false);
            CancelInvoke("LaunchBullet");
            photonView.RPC("ReturnRifleRotate", RpcTarget.All);

        }
    }
    /// <summary>
    /// //////////
    /// </summary>
    public void Test()
    {
        if (ChangeWeaponSLider.value == 0)
        {

            slowMoveHandsDown = false;
            photonView.RPC("ChangeWeapons", RpcTarget.All);
        }

        if (ChangeWeaponSLider.value == 1)
        {

            photonView.RPC("ChangeWeapons", RpcTarget.All);
        }

    }

    [PunRPC]
    public void ChangeWeapons()
    {



        if (photonView.IsMine)
        {
            if (ChangeWeaponSLider.value == 1)
            {

                anim.SetLayerWeight(3, 1);
                anim.SetBool("IsDrawing", true);
                //StartCoroutine("RiflePaste1");
                photonView.RPC("RiflePaste1", RpcTarget.All);

                reholster = true;
            }



            if (ChangeWeaponSLider.value == 0 && reholster == true)
            {
                liftArm2 = true;
                //returnHandsDownSlowly = 0;
                // slowMoveHandsDown = true;
                //anim.SetLayerWeight(3, 0);
                anim.SetBool("IsDrawing", false);

                photonView.RPC("ReturnRifleToBack", RpcTarget.All);

                #region first
                //    //Vector3 position  = new Vector3(rifleTransform.transform.position.x, -0.18f, rifleTransform.transform.position.z);
                //    // rifleTransform.transform.position = rifleHolster.transform.position;
                //    //rifleTransform.transform.position = position;
                //    // rifleTransform.transform.SetParent(rifleHolster);
                //    //--------------------------------------
                //    //rifleTransform.transform.position = rifleBackHolster.transform.position ;

                //    //rifleTransform.transform.position = rifleHolster.transform.position;
                //    // Vector3 transPos = new Vector3(-0.023f, -0.21f, -0.185f);
                //    // dummRifleContainer.localPosition = transPos;
                //    // Vector3 rot = new Vector3(0.983f, -29.674f, -22.601f);
                //    //dummRifleContainer.localEulerAngles = rot;
                //    // Quaternion x = Quaternion.Euler(rot);
                //    // dummRifleContainer.localRotation = x;
                //    //dummyRifle.GetComponent<MeshRenderer>().enabled = false;
                //    //HIde first rifle
                #endregion first


            }




        }
    }



    void FixedUpdate()
    {
        #region TurnVector
        // m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(3);
        // m_ClipName = this.m_CurrentClipInfo[3].clip.name;
        //anim.SetBool("IsShooting", true);


        //Vector3 rot = new Vector3(0, Mathf.Lerp(0, 90f, 2.0f * Time.deltaTime), 0);
        //practiceRifle.eulerAngles = rot;

        //Quaternion rotatio = Quaternion.Euler(0, 30.0f * Time.deltaTime, 0);
        //practiceRifle.rotation = rotatio;
        //practiceRifle.transform.rotation = rotatio;
        // Mathf.Clamp(20.0f * Time.deltaTime, 0, 30f),
        //practiceRifle.transform.position = new Vector3(-0.75f, 0.0f, 0.0f);

        //works with hesitation
        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.x, 90.0f, -30.0F);
        //practiceRifle.transform.Rotate(rotationX * Time.deltaTime, 0.0f, 0.0f, Space.World);

        //wroks for y
        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.y, 90.0f, -30.0F);
        //practiceRifle.transform.Rotate(0.0f,rotationX * Time.deltaTime,  0.0f, Space.World);

        //wroks for z
        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, 90.0f, -30.0F);
        //practiceRifle.transform.Rotate(0.0f,  0.0f, rotationX * Time.deltaTime, Space.World);

        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, 90.0f, 30.0F);
        //practiceRifle.transform.Rotate(0.0f, 0.0f, rotationX * Time.deltaTime, Space.World);

        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, -90.0f, 30.0F);
        //practiceRifle.transform.Rotate(0.0f, 0.0f, rotationX * Time.deltaTime, Space.Self);

        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, -90.0f, -30.0F);
        //practiceRifle.transform.Rotate(0.0f, 0.0f, rotationX * Time.deltaTime, Space.Self);

        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, -30.0f, 30.0F);
        //practiceRifle.transform.Rotate(0.0f, 0.0f, rotationX * Time.deltaTime, Space.Self);

        //float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.z, practiceRifle.eulerAngles.z - 45.0f, -10.0F);
        //practiceRifle.transform.Rotate(0.0f, 0.0f, rotationX * Time.deltaTime, Space.Self);



        //practiceRifle.x = Mathf.Clamp(pivotRotation.x, -90.0F, 0.0F);
        // practiceRifle.eulerAngles = pivotRotation;

        // float rotationX = Mathf.Clamp(practiceRifle.eulerAngles.x, -90.0F, 0.0F);
        //practiceRifle.rotation = Quaternion.Euler(rotationX*  Time.deltaTime, transform.eulerAngles.y, transform.eulerAngles.z);

        //practiceRifle.transform.rotation = Quaternion.FromToRotation(Vector3.right * Time.deltaTime * 2.0f, transform.forward );

        //float angle = 30.0f;
        //Vector3 axis = Vector3.right;
        //practiceRifle.transform.rotation.ToAngleAxis(out angle, out axis);

        //practiceRifle.transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, timeCount);
        //float timeCount = timeCount + Time.deltaTime;
        #endregion
        //photonView = GetComponent<PhotonView>();
        //if (photonView.IsMine)
        //{
        //    if (photonView.gameObject.activeInHierarchy)
        //    {


        //        if (ChangeWeaponSLider.value == 1)
        //        {

        //            StartCoroutine(RiflePaste1());
        //            // photonView.RPC(StartCoroutine("RiflePaste1"), RpcTarget.All);
        //            //photonView.StartCoroutine()
        //            //photonView.RPC("Loadrifle", RpcTarget.AllBuffered);
        //            //StartCoroutine("RiflePaste1");

        //            anim.SetLayerWeight(3, 1);
        //            anim.SetBool("IsDrawing", true);


        //            // rifleTransform.transform.SetParent(rifleHolster, true);
        //            // rifleTransform.transform.position = rifleHolster.transform.position; //set the rifle to the hand that is drwaing rifle         
        //            // Vector3 rotation1 = new Vector3(rifleTransform.transform.position.x, rifleTransform.transform.position.y, 90.175f);
        //            // rifleTransform.transform.eulerAngles = rotation1;


        //            //Loadrifle();
        //            // Debug.Log(anim.GetCurrentAnimatorStateInfo(3));

        //            //if (m_ClipName == "Rifle_idle")
        //            //{
        //            //    //realRifle.transform.position = dummRifle.transform.position;
        //            //}
        //            reholster = true;
        //        }
        //        if (ChangeWeaponSLider.value == 0 && reholster == true)
        //        {
        //            // anim.SetLayerWeight(3, 0); 
        //            anim.SetBool("IsDrawing", false);
        //            Vector3 rotation = new Vector3(rifleTransform.transform.position.x, rifleTransform.transform.position.y, rifleTransform.transform.position.z);
        //            rifleTransform.transform.eulerAngles = rotation;

        //            Vector3 rot2 = new Vector3(118.832f, 34.0f, 47.11f);
        //            rifleTransform.transform.SetParent(rifleHolster, true);
        //            rifleTransform.transform.localPosition = new Vector3(-0.062f, -0.131f, -0.071f);
        //            rifleTransform.transform.localRotation = Quaternion.Euler(rot2);
        //            dummyRifle.GetComponent<MeshRenderer>().enabled = false;
        //            //st a cour-touine to call this
        //            StartCoroutine("RiflePaste");

        //            rifleTransform.transform.SetParent(null);
        //            //Vector3 rot = new Vector3(0.0f, 0.0f, 53.74f);
        //            //rifleTransform.transform.SetParent(rifleBackHolster);
        //            //rifleTransform.transform.localPosition = new Vector3(0.087f, 0.134f, 0.079f);
        //            //rifleTransform.transform.localRotation = Quaternion.Euler(rot);



        //            #region first
        //            //Vector3 position  = new Vector3(rifleTransform.transform.position.x, -0.18f, rifleTransform.transform.position.z);
        //            // rifleTransform.transform.position = rifleHolster.transform.position;
        //            //rifleTransform.transform.position = position;
        //            // rifleTransform.transform.SetParent(rifleHolster);
        //            //--------------------------------------
        //            //rifleTransform.transform.position = rifleBackHolster.transform.position ;

        //            //rifleTransform.transform.position = rifleHolster.transform.position;
        //            // Vector3 transPos = new Vector3(-0.023f, -0.21f, -0.185f);
        //            // dummRifleContainer.localPosition = transPos;
        //            // Vector3 rot = new Vector3(0.983f, -29.674f, -22.601f);
        //            //dummRifleContainer.localEulerAngles = rot;
        //            // Quaternion x = Quaternion.Euler(rot);
        //            // dummRifleContainer.localRotation = x;
        //            //dummyRifle.GetComponent<MeshRenderer>().enabled = false;
        //            //HIde first rifle
        //            #endregion first
        //            rifleTransform.gameObject.SetActive(true);

        //            //----------------------------------------

        //            reholster = false;


        //        }

        //        if (ChangeWeaponSLider.value == 0 && reholster == false)
        //        {
        //            if (liftArm && currentWeight <= 0.99)
        //            {

        //                currentWeight = Mathf.Lerp(currentWeight, 1.0f, Time.deltaTime * 6f);
        //                anim.SetLayerWeight(1, currentWeight);

        //                //Debug.Log(" liftArm ");
        //                //photonAnim.SetLayerSynchronized(1, currentWeight);

        //                // rifleHolster.transform.SetParent(rifleBackHolster);


        //            }
        //            if (liftArm == false)
        //            {
        //                currentWeight = Mathf.Lerp(currentWeight, 0.0f, Time.deltaTime * 6f);
        //                anim.SetLayerWeight(1, currentWeight);
        //                if (ChangeWeaponSLider.value == 1)
        //                {
        //                    anim.SetBool("IsShooting", false);
        //                }
        //                CancelInvoke("LaunchBullet");
        //            }
        //        }
        //        //Rifle fire
        //        for (int i = 0; i < m_CurrentClipInfo.Count(); i++)
        //        {
        //            if (ChangeWeaponSLider.value == 1 && m_CurrentClipInfo[i].clip.name == "FiringRifle")
        //            {
        //                // transform.position = new Vector3(Mathf.Lerp(minimum, maximum, t), 0, 0);
        //                startX = new Vector3(Mathf.Lerp(dummRifleContainer.localPosition.x, RiflePosFire.x + 0.09f, 60f * Time.deltaTime), 0, 0);
        //                startY = new Vector3(0, Mathf.Lerp(dummRifleContainer.localPosition.y, RiflePosFire.y + 0.1f, 60f * Time.deltaTime), 0);
        //                startZ = new Vector3(0, 0, Mathf.Lerp(dummRifleContainer.localPosition.z, RiflePosFire.z, 60f * Time.deltaTime));

        //                dummRifleContainer.transform.localPosition = (startX + startY + startZ);
        //                dummRifleContainer.transform.localRotation = Quaternion.Euler(RifleTurnFire);

        //            }
        //        }
        //    }
        //}
    }

    //load rifle to hand
    [PunRPC]
    async void RiflePaste1()
    {

        await Task.Delay(TimeSpan.FromSeconds(1.0f));
        // yield return new WaitForSeconds(1.5f);


        //    anim.SetLayerWeight(3, 1);
        //    anim.SetBool("IsDrawing", true);

        if (this.gameObject.GetComponent<Animator>().avatar.name != "RobaAssasinAvatar")
        {
            rifleTransform.transform.SetParent(rifleHolster.GetComponent<PhotonView>().transform, true);

            rifleTransform.transform.position = rifleHolster.GetComponent<PhotonView>().transform.position; //set the rifle to the hand that is drwaing rifle         

            position1 = new Vector3(0.0442615f, 0.148982f, 0.07681919f);

            rifleTransform.transform.localPosition = position1;

            rotation = new Vector3(-110.738f, -141.905f, -44.22501f);

            rifleTransform.transform.localRotation = Quaternion.Euler(rotation);
        }
        if (this.gameObject.GetComponent<Animator>().avatar.name == "RobaAssasinAvatar")
        {
            rifleTransform.transform.SetParent(rifleHolster.GetComponent<PhotonView>().transform, true);

            rifleTransform.transform.position = rifleHolster.GetComponent<PhotonView>().transform.position;
            position1 = new Vector3(0.096f, 0.263f, 0.032f);

            rifleTransform.transform.localPosition = position1;

            rotation = new Vector3(-132.661f, -46.228f, -128.237f);

            rifleTransform.transform.localRotation = Quaternion.Euler(rotation);

        }





    }


    //turn the rifle as it fires
    [PunRPC]
    async void RotateRifleFire()
    {

        position1 = new Vector3(-0.029f, 0.096f, -0.009f);

        rifleTransform.transform.localPosition = position1;

        rotation = new Vector3(-129.132f, 67.356f, -186.644f);

        rifleTransform.transform.localRotation = Quaternion.Euler(rotation);

        await Task.Delay(TimeSpan.FromSeconds(3.0f));

        //rifleTransform.transform.SetParent(rifleHolster.GetComponent<PhotonView>().transform, true);


        // rifleTransform.transform.SetParent(masterbackCOntainer, true);



    }





    //return rifle to idle position after he shoots
    [PunRPC]
    public void ReturnRifleRotate()
    {

        if (this.gameObject.GetComponent<Animator>().avatar.name != "RobaAssasinAvatar")
        {
            position1 = new Vector3(0.0442615f, 0.148982f, 0.07681919f);


            rifleTransform.transform.localPosition = position1;

            rotation = new Vector3(-110.738f, -141.905f, -44.22501f);

            rifleTransform.transform.localRotation = Quaternion.Euler(rotation);
        }

        if (this.gameObject.GetComponent<Animator>().avatar.name == "RobaAssasinAvatar")
        {
            //0.075f            0.242f  -0.011f
            //-149.998f   -81.98401f     -100.508f
            position1 = new Vector3(0.075f, 0.242f, -0.011f);


            rifleTransform.transform.localPosition = position1;

            rotation = new Vector3(-149.998f, -81.98401f, -100.508f);

            rifleTransform.transform.localRotation = Quaternion.Euler(rotation);

        }
    }



    [PunRPC]
    async void ReturnRifleToBack()
    {



        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        // rifleTransform.transform.localPosition = new Vector3(rifleTransform.transform.localPosition .x - 0.01f, rifleTransform.transform.localPosition.y +0.134f, rifleTransform.transform.localPosition.z+ 1.079f);
        // rifleTransform.transform.localRotation =  Quaternion.Euler(0, 0, 0);

        rifleTransform.transform.localPosition = new Vector3(masterbackCOntainer.transform.localPosition.x + 0.11f, masterbackCOntainer.transform.localPosition.y - 0.098f, masterbackCOntainer.transform.localPosition.z + 0.046f);
        rifleTransform.transform.localRotation = Quaternion.Euler(masterbackCOntainer.transform.localRotation.x - 0.924f, masterbackCOntainer.transform.localPosition.y - 0.929f, masterbackCOntainer.transform.localPosition.z - 48.309f);


        rifleTransform.transform.SetParent(masterbackCOntainer, false);
        //Player.transform.eulerAngles = new Vector3(Player.transform.eulerAngles.x, -CameraHead.transform.eulerAngles.y, Player.transform.eulerAngles.z);



    }
    public void LaunchBullet()
    {


        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (scene.buildIndex == 3)
        {

            SoundManagerSingle.instance.PlayerSHoot();
            GameObject obj = ObjectPooler.current.GetPooledObject();
            obj.SetActive(true);
            obj.transform.position = shooter.transform.position;
            obj.transform.rotation = shooter.transform.rotation;
            obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<Rigidbody>().velocity = transform.forward * 25f;

            Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());


            if (obj == null) return;
        }



        if (photonView.IsMine)
        {
            //ObjectPooler.current.Practice();
            SoundManagerSingle.instance.PlayerSHoot();


            float s = anim.GetFloat("Vertical");
            int rounded = Convert.ToInt32(s);

            bulletPrefab.SetActive(true);
            bulletPrefab.GetComponent<Light>().enabled = true;

            GameObject g = PhotonNetwork.Instantiate("bulletPrefab", shooter.transform.position, shooter.transform.rotation);

            g.GetComponent<Rigidbody>().velocity = transform.forward * 10f;


            Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());





        }
        #region comment
        //GameObject obj = ObjectPooler.current.GetPooledObjectPhoton();
        //obj.SetActive(true);

        //obj.transform.position = shooter.transform.position;
        //obj.transform.rotation = shooter.transform.rotation;
        //obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        //obj.GetComponent<Rigidbody>().useGravity = false;
        //obj.GetComponent<Rigidbody>().velocity = transform.forward * 25f;

        //Physics.IgnoreCollision(obj.GetComponent<Collider>(), GetComponent<Collider>());


        // if (obj == null) return;

        //if (scene.buildIndex == 2)
        //{


        //    float s = anim.GetFloat("Vertical");
        //    int rounded = Convert.ToInt32(s);
        //    bulletPrefab.SetActive(true);
        //    GameObject g = PhotonNetwork.Instantiate("bulletPrefab", shooter.transform.position, shooter.transform.rotation);
        //    g.transform.position = shooter.transform.position;
        //    g.transform.rotation = shooter.transform.rotation;
        //    Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
        //    if (rounded == 1)
        //    {
        //        g.GetComponent<Rigidbody>().velocity = transform.forward * 15f;
        //    }
        //    else
        //    {
        //        g.GetComponent<Rigidbody>().velocity = transform.forward * 5f;
        //    }

        //}
        #endregion
        CancelInvoke("LaunchBullet");
    }
    public void SoundStop()
    {
        audioSource.Stop();
    }

    public GameObject GetPooledObjectPhoton()
    {

        if (photonView.IsMine)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    //return pooledObjects[i];
                    for (int ii = 0; i < poolAmount; ii++)
                    {
                        // Debug.Log("activated GamePhoton ");
                        pooledObjects[ii].SetActive(true);
                        return pooledObjects[ii];

                    }
                    //return PhotonNetwork.Instantiate(pooledObjects[i].name, transform.position, Quaternion.identity);

                }
            }

            if (willGrow)
            {
                // GameObject obj = Instantiate(poolObject);
                //  PhotonNetwork.Instantiate(obj.name, transform.position, Quaternion.identity);
                // pooledObjects.Add(obj);
                // return obj;
            }
        }
        return null;

    }
}
