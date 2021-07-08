using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EnemyMultiShooting : MonoBehaviour
{
    [Header("public")]
    //public float maxDmage = 120f;
    //public float minimDamage = 45f;
    //public AudioClip shotClip;
    //public float flashIntensity = 3f;
    public float fadeSpeed = 10f;
    float timer = 0f;
    public GameObject projectile;
    public Transform shooter;

    [Header("Private")]
    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLIne;
    private Light laserSHotLight;
    private SphereCollider col;

    private Vector3 direction;
    private Transform player;
    public float speed = 1.0f;
    //public Transform rightHand;
    [HideInInspector]
    PhotonView photon;

    public ParticleSystem ps;
    //List<GameObject> bulletList;
    // Start is called before the first frame update
    void Start()
    {
       // GameObject refrenceTile = (GameObject)Instantiate(Resources.Load(""));
           
        //bulletList = new List<GameObject>();
           //for (int i = 0; i < 8; i++)
           //{
           //    GameObject objBullet = (GameObject)PhotonNetwork.Instantiate("SingleENemyBullet", this.transform.position, this.transform.rotation);
           //    objBullet.SetActive(false);
           //    bulletList.Add(objBullet);
           //}

           photon = GetComponent<PhotonView>();
           anim = GetComponent<Animator>();
        //  laserShotLIne = GetComponentInChildren<LineRenderer>();
          laserSHotLight = GetComponentInChildren<Light>();

        //   col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hash = GameObject.FindGameObjectWithTag("GameControllerMult").GetComponent<HashIDs>();
        // laserShotLIne.enabled = false;
         laserSHotLight.intensity = 0f;
       // rightHand = anim.GetBoneTransform(HumanBodyBones.Chest);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnemyMultiEnemyInSIght.enemySightStatic.playerInSight == true) //shot > 0.5f && !shooting && fow.playerDetectedinFOV 
        {
            // Vector3 i= player.transform.position - shooter.transform.forward;

            // Debug.DrawLine(transform.forward, i, Color.green);

            timer += Time.deltaTime;
            // anim.SetLayerWeight(1, 1);
            // anim.SetBool("PlayerInSightSing", true);
            if (timer > 0.6f)
            {
                //float singleStep = speed * Time.deltaTime;
                //get the transform from the FOW
                //FieldOfView.instanceFOV.bestTarget;
                //direction = player.transform.position - this.transform.position;
                //FieldOfView.instanceFOV.bestTarget.transform.position
                //  direction = player.transform.position - this.transform.forward;
                //Quaternion look = Quaternion.LookRotation(direction);
                // Vector3 temp = shooter.transform.eulerAngles;
                // temp.z =  100f;
                // shooter.eulerAngles = temp;
                //Debug.Log("temp " + temp.ToString());

                //Vector3 newDirection = Vector3.RotateTowards(shooter.transform.forward, direction , singleStep, 0.0f);

                // shooter.transform.rotation = look;

                // Vector3 direction2 = player.transform.position - rightHand.transform.position;
                // Quaternion loo2k = Quaternion.LookRotation(direction2);
                //if (photon.IsMine)
                //{
                //    if (EnemyMultiEnemyInSIght.degreeDetection == true)
                //    {
                //       // ps.Play(); //particle effect
                //       // AudioManager.instance.Play("EnemyShoot");
                //        //  photon.StartCoroutine("ShootCorotine");
                //        photon.RPC("Shoot", RpcTarget.All);
                //    }
                //    else if (EnemyMultiEnemyInSIght.degreeDetection == false)
                //    {
                //        photon.RPC("StopShoot", RpcTarget.All);

                //    }
                //}

                //if radius is less than the amount
                if (EnemyMultiEnemyInSIght.degreeDetection == true)
                {
                    // Debug.Log(EnemyMultiEnemyInSIght.degreeDetection);
                    StartCoroutine("ShootCorotine");
                    timer = 0;
                }
                if (EnemyMultiEnemyInSIght.degreeDetection == false)
                {
                    StopCoroutine("ShootCorotine");
                    // Debug.Log(EnemyMultiEnemyInSIght.degreeDetection);
                }

            }
        }

    }
    [PunRPC]
    public void Shoot()
    {
        StartCoroutine("ShootCorotine");
    }

    [PunRPC]
    public void StopShoot()
    {
        StopCoroutine("ShootCorotine");
    }

    [PunRPC]
    public IEnumerator ShootCorotine()
    {
        
        yield return new WaitForSeconds(0.2f);
   
        ps.Play(); 
        AudioManager.instance.Play("EnemyShoot");
        laserSHotLight.intensity = Mathf.Lerp(laserSHotLight.intensity, 0f, fadeSpeed * Time.deltaTime);

    
        // Debug.DrawRay(shooter.transform.position, FieldOfView.instanceFOV.bestTarget.transform.position, Color.green, 100f);
        shooter.transform.LookAt(FieldOfView.instanceFOV.bestTarget.transform.position + new Vector3(0,1.5f,0));
        GameObject bullet = PhotonNetwork.Instantiate("SingleENemyBullet", shooter.transform.position, shooter.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(shooter.transform.forward * 400f);
        
       
    }

    [PunRPC]
    public void Destroy(GameObject x)
    {
        if (photon.IsMine)
        {
            PhotonNetwork.Destroy(x);
        }
    }
}
