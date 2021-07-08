using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using Photon.Pun;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(RigidbodyFirstPersonController))]
public class PlayerMovmentController : MonoBehaviourPunCallbacks,IPunObservable
{
    [Header("UI ELements")]
   
    [SerializeField]
    public Joystick joystick;
    [SerializeField]
    private RigidbodyFirstPersonController rigidBodyController;
    [SerializeField]
    public FixedTouchField fixedTouchField;
    //public Button fireButn;

    [SerializeField]
    private Animator animator;
   
   
    private Transform position1;
    //public Camera FPsCamera;
    int layerMask = 1 << 9;

    public GameObject hitEffectPrefa;

    public bool detect;
     

    private AudioSource sm;
    [HideInInspector]
    public Transform playerBtn;
    public GameObject buttonPrefab;
    public Canvas buttonCanvas;

    [HideInInspector]
    public GameObject BulletPrefabMulti;
    [SerializeField]
    public Transform shooter;

    

    [Header("Name Text")]
    public Text textName;

    public float shootForce;
             
    private GameObject clone;

    [Header("Button UI")]
    public Button TriggerButn;

    private GameObject bulletFlash;
    
    
    public ParticleSystem ps;  
    public ParticleSystem bS;

    public static PlayerMovmentController instance;
    private static float amount1 = 0;

    [Header("Jump")]
    private RigidbodyFirstPersonController Rgfps;
    private float verticalVelocity;
    private float gravity;
    private float jumpforce;
    
    private void Awake()
    {
        #region ExtraneaousCode
        //if (PlayerMovmentController.instance == null)
        //{
        //    PlayerMovmentController.instance = this;
        //}
        //else
        //{
        //    if(PlayerMovmentController.instance != this)
        //    {
        //        Destroy(PlayerMovmentController.instance.gameObject);
        //        PlayerMovmentController.instance = this;
        //    }
        //}
        //DontDestroyOnLoad(this.gameObject); 
        #endregion

      //  menuPanel = gameObject.GetComponentInChildren<GameObject>();
      //  menuPanel.SetActive(true);
       // GameObject g = GameObject.Find("PlayerUIMulti(Clone)");
        
        if (instance != null)
        {
           
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        
       //  health = startHealth;
       // healthBar.fillAmount = health / startHealth;
        //healthBar.fillAmount = 1f;
        ///Debug.Log(healthBar.fillAmount);
        position1 = GetComponent<Transform>();
        animator = GetComponent<Animator>();
       
        Rgfps = GetComponent<RigidbodyFirstPersonController>();
        
        sm = GetComponent<AudioSource>();
        if (photonView.IsMine)
        {
            position1 = GetComponent<Transform>();
            animator = GetComponent<Animator>();
            //healthBar.fillAmount = 50f;
            animator.gameObject.SetActive(true);

           
            PhotonView photonView = PhotonView.Get(this);
            //photonView.RPC("Fire1", RpcTarget.All);
    
            rigidBodyController = GetComponent<RigidbodyFirstPersonController>();
            //animator = GetComponent<Animator>();

            sm = this.GetComponent<AudioSource>();
            detect = false;
        }
      
    }
      
    void FixedUpdate()
    {
                photonView.RPC("AnimatorV", RpcTarget.All);

       
    }

  
    [PunRPC]
    public void AnimatorV()
    {
        if (photonView.IsMine)
        {


            if (joystick != null)
            {
                rigidBodyController.joystickInputAxis.x = joystick.Horizontal;
                rigidBodyController.joystickInputAxis.y = joystick.Vertical;

                rigidBodyController.mouseLook.lookInputAxis = fixedTouchField.TouchDist;

                animator.SetFloat("Horizontal", joystick.Horizontal);
                animator.SetFloat("Vertical", joystick.Vertical);

            }

        }


    }

   
    public void Fire1()
    {
        if (photonView.IsMine)
        {
            
            photonView.RPC("RaiseArm", RpcTarget.AllBuffered);
        }
       
    }
      
    public void OnTriggerEnter(Collider other)
    {
      //  Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "SingleENemyBullet")
        {
            photonView.RPC("Blood", RpcTarget.AllBuffered);
           // amount1 -= .02f;
           // Debug.Log("amount1" + amount1);

            //ps.Play();
            // PhotonNetwork.Instantiate("BloodHitMultiPlayer", transform.position / 2, Quaternion.identity);
            //print("First point that collided: " + other.contacts[0].point);
        }
       
    }

[PunRPC]
public void Blood()
    {
        if (photonView.IsMine)
        {
           // Debug.Log(" dog ");
            bS.Play();
           // healthBar.fillAmount -= 0.2f;
          //  Debug.Log("health " + healthBar.fillAmount);
        }
    }

[PunRPC]
public void RaiseArm()
    {
        if (photonView.IsMine)
        {
            
            // GameObject refrenceTile = (GameObject)Instantiate(Resources.Load("MenuPanel"));
            
           /// gameObjec //bS.Play();
            //GameObject refrenceTile = (GameObject)Instantiate(Resources.Load("BulletPrefabMulti"));
            //Debug.Log("hit them ");
            animator.SetLayerWeight(1, 1);
            //animator.SetBool("IsShooting", true);
            // GameObject playerPRef = GameObject.Find("PlayerPRefab(Clone)");
            position1 = GetComponent<Transform>();
            //playerPRef.GetComponentInChildren<Transform>().name = "shooter";
            //Debug.DrawRay(this.shooter.position, shooter.transform.TransformDirection(Vector3.forward) * 100f, Color.red);
            RaycastHit hit;
            //animator.SetLayerWeight(1, 1);
            ps.Play();
            //bulletFlash = PhotonNetwork.Instantiate("bulletFlash", this.shooter.position, Quaternion.identity);
            clone = PhotonNetwork.Instantiate("BulletPrefabMulti", this.shooter.position, Quaternion.identity);
            clone.GetComponent<MeshRenderer>().enabled = false;
            clone.GetComponent<Rigidbody>().AddForce(shooter.TransformDirection(Vector3.forward * 300f));
            sm.Play();

            //animator.SetLayerWeight(1, 1);
            //animator.GetComponent<PhotonView>().GetComponent<Animator>().SetLayerWeight(1, 1);
            if (Physics.Raycast(this.shooter.position, shooter.transform.TransformDirection(Vector3.forward), out hit, 400f, ~layerMask))
            {

                if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    //Debug.Log("hit me ");
                    // StartCoroutine(Des(clone, hitEffectPrefa1));, clone.GetComponent<PhotonView>(), hitEffectPrefa1.GetComponent<PhotonView>()
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 51f);
                }
                #region stuff
                // if (photonView.IsMine)
                // {
                //if (Object.GetComponent().IsMine)
                //{
                //    PhotonNetwork.Destroy(Object);
                //}
                //photonView.RPC("networkDestroy", PhotonTargets.All, 0);
                //photonView.RPC("networkDestroy", PhotonTargets.All, 0);
                // PhotonNetwork.Destroy(clone, 5f);
                //PhotonNetwork.Destroy(hitEffectPrefa1, 5f);

                //if (hit.collider.tag == "World")
                //{
                //    if (photonView.IsMine)
                //    {
                //        PhotonNetwork.Destroy(clone);
                //        PhotonNetwork.Destroy(hitEffectPrefa);
                //    }
                //}
                // StartCoroutine(Des(clone, hitEffectPrefa1));

                //  }
                #endregion
            }
        }
    }

    
    public void Lower()
    {

        photonView.RPC("LowerFromCall", RpcTarget.All);
        //PhotonNetwork.Destroy(bulletFlash);


    }
    [PunRPC]
    public void LowerFromCall()
    {
        animator.SetLayerWeight(1, 0);
       // animator.SetBool("IsShooting", false);
    }


    [PunRPC]
    public void TakeDamage(float damage, PhotonMessageInfo info)
    {

      //  health -= damage;
      
     // healthBar.fillAmount = health / startHealth;
      
        //if (health <= 0)
        //{
        //   Debug.LogWarning("just died");
        //    //Die();
        //    photonView.RPC("Die", RpcTarget.All);
        //    Debug.LogWarning(info.Sender.NickName + " killed " + info.photonView.Owner.NickName);
        //    //disable the button
        //    TriggerButn.enabled = false;

        //}

    }
    [PunRPC]
    public void Die()
    {
        if (photonView.IsMine)
        {
            // Debug.LogWarning("just died 1");
            Debug.Log("Die");
            animator.SetBool("IsDeath", true);
            transform.GetComponent<PlayerMovmentController>().enabled = false;
            StartCoroutine("Respawn");
        }
}

    
    public IEnumerator Respawn()
    {
     
        //float respawnTime = 8.0f;
        //while (respawnTime > 0.0f)
        //{
        yield return new WaitForSeconds(4);
        //  respawnTime -= 1.0f;
        photonView.RPC("REsetHEalth", RpcTarget.All);
        //  }
    }

    [PunRPC]
    public void REsetHEalth()
    {
        animator.SetBool("IsDeath", false);
        animator.SetBool("IsDeathReverse", true);

        transform.GetComponent<PlayerMovmentController>().enabled = true;
       // healthBar.fillAmount = 1f;
       
       // startHealth = 1;
        //health = startHealth;
        //healthBar.fillAmount = health / startHealth;
        //healthBar.fillAmount = health;
    }

//[PunRPC]
//void LaunchBullet()
//{
//    GameObject clone = PhotonNetwork.Instantiate("bulletPrefab", this.shooter.position, Quaternion.identity);
//    Rigidbody Temporary_Rigidbody;
//    Temporary_Rigidbody = clone.GetComponent<Rigidbody>();
//    Temporary_Rigidbody.velocity = shooter.TransformDirection(Vector3.forward * 50);
//    //Destroy(clone, 1f);
//    // StartCoroutine(DelayDestruction(clone));
//    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetComponent<Rigidbody>().position);
            stream.SendNext(GetComponent<Rigidbody>().rotation);
            stream.SendNext(GetComponent<Rigidbody>().velocity);
        }
        else
        {
            GetComponent<Rigidbody>().position = (Vector3)stream.ReceiveNext();
            GetComponent<Rigidbody>().rotation = (Quaternion)stream.ReceiveNext();
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            GetComponent<Rigidbody>().position += GetComponent<Rigidbody>().velocity * lag;
        }
    }
}



//[PunRPC]
//public IEnumerator DelayDestruction(GameObject clone)
//{
//    yield return new WaitForSeconds(.5f);
//    if (clone != null)
//    {
//        PhotonNetwork.Destroy(clone);
//    }
//}
//[PunRPC]
//public IEnumerator Des()
//{
//    yield return new WaitForSeconds(2f);
//    //Destroy(clone);
//    if (clone != null)
//    {
//        PhotonNetwork.Destroy(clone);
//    }
//    // clone.SetActive(false);
//    // hitEffectPrefa.SetActive(false);

//}


//public void DestroyFromWOrld(Vector3 position)
//{
//    GameObject hitEffectPrefa1 = Instantiate(hitEffectPrefa, position, Quaternion.identity);
//    Destroy(hitEffectPrefa1, 0.5f);
//    if (photonView.IsMine)
//    {
//        if (clone != null)
//        {
//            //Debug.Log("destoryFromWorld");
//            PhotonNetwork.Destroy(clone);
//        }
//    }
//} //Rigidbody Temporary_Rigidbody;
//clone.GetComponent<PhotonView>().GetComponent<Rigidbody>().velocity;
//clone.GetComponent<PhotonView>().GetComponent<Rigidbody>().velocity = shooter.TransformDirection(Vector3.forward * 50);
//clone.GetComponent<Rigidbody>().velocity = shooter.TransformDirection(Vector3.forward * 50);


//StartCoroutine(Des());

//Destroy(clone, 1f);
//other.gameObject.GetComponent<PhotonView>()
//photonView.RPC("CreateEffect", RpcTarget.All, hit.point);
//clone.SetActive(true);
//hitEffectPrefa.SetActive(true);
// Invoke("DestroyFromWOrld", 2f);
//photonView.RPC("Des",RpcTarget.All,clone.gameObject);        
//photonView.RPC("CreateEffect", RpcTarget.All, hit.point);
//photonView.RPC("LaunchBullet", RpcTarget.All);
//public void Fire()
//{

//    if (allowShoot == true)
//    {

//        Vector3 bulletpos = BulletSpawnName.transform.position;
//        GameObject Bullet = PhotonNetwork.Instantiate("Bullet", bulletpos, Quaternion.identity, 0);
//        Bullet.GetComponent().AddForce(transform.forward * BulletForce);
//        photonView.RPC("BulletTransit", PhotonTargets.All, bulletpos, BulletForce);
//    }
//}

//[PunRPC]
//public void BulletTransit(Vector3 bulletpos, int BulletForce)
//{
//    bullet.transform.position = bulletpos * Time.deltaTime;
//}

