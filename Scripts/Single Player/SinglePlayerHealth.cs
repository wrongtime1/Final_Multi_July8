using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.Linq;
public class SinglePlayerHealth : MonoBehaviour
{

    [Header("Sliders")]
    public Slider sheildSlider;
    public Slider healthslide;
    [HideInInspector]
    public static float healthHit = 0;
    [Range(0, 10)]
    public static int shieldHit = 0;

    public Text hitValue;
    private CapsuleCollider sphereCollider;
    private Animator anim;

    private float delay = 1f;
    private EnemyInSIghtZombie enemyZombieSight;

    public static SinglePlayerHealth singleHealth;
    public Image fillamount;

    public bool PlayerDeath;

    public ParticleSystem Particle1;

    public bool turretGunTurnOffSHooting;

    public int clampX;
    [HideInInspector]
    public PhotonView photonView;
    [HideInInspector]
    public bool preventFromJumpingBack;
    // Start is called before the first frame update
    Scene scene;
    int x;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        healthHit = 0;
        shieldHit = 0;
        healthslide.value = healthHit;
        sheildSlider.value = shieldHit;
        // Debug.Log(" getinstanceid " + photonView.GetInstanceID());
        singleHealth = this;

        enemyZombieSight = GetComponent<EnemyInSIghtZombie>();

        healthslide.value = healthHit;

        // Debug.Log("health " + healthHit);
        sheildSlider.value = shieldHit;


        anim = GetComponent<Animator>();
        sphereCollider = GetComponent<CapsuleCollider>();
       
    }



    //public void OnTriggerEnter(Collider other)
    //{


    //   // Debug.Log("from trigger " + scene.name + "   " + scene.buildIndex);
    //    if (scene.name == "SinglePlayerSce1SciFi")
    //    {

    //        //other.gameObject.name == "hitEffectPrefa(Clone)" || other.gameObject.name == "bulletPrefab(Clone)" ||
    //        if (other.gameObject.tag == "Bullet")
    //        {

    //            //  other.gameObject.SetActive(false);
    //            int x = Random.Range(0, 3);
    //            shieldHit += 2;
    //            if (shieldHit <= 10)
    //            {


    //                SoundManagerSingle.instance.PlayerArmoud();


    //                sheildSlider.value = shieldHit;
    //                //call bodyarmou sound
    //                Particle1.transform.position = other.gameObject.transform.position;
    //                Particle1.Play();
    //            }
    //            if (shieldHit > 10 && healthHit < 10)
    //            {
    //                //Debug.Log(" healthHit hit");
    //                // SoundManagerSingle.instance.PlayerArmoudStop();
    //                SoundManagerSingle.instance.PainSound();

    //                Particle1.transform.position = other.gameObject.transform.position;
    //                Particle1.Play();
    //                healthHit += 1f;
    //                healthslide.value = healthHit;

    //            }
    //            if (healthHit <= 9)
    //            {
    //                Injury(x);
    //            }

    //            if (healthHit == 10)
    //            {

    //                turretGunTurnOffSHooting = true;
    //                Death();
    //            }
    //        }


    //    }
    //    int currentSceenIndex = SceneManager.GetActiveScene().buildIndex;
    //    //MultipSce1
    //    if (scene.name == "MultipSce1")
    //    {
    //        photonView = GetComponent<PhotonView>();
    //        if (photonView.IsMine)
    //        {
    //            //Debug.Log("ebfore it " + scene.buildIndex);
    //            //Debug.Log(other.gameObject.name);


    //            PhotonView photonView = GetComponent<PhotonView>();

    //            if (other.gameObject.tag == "Bullet" )
    //            {


    //                int x = Random.Range(1, 3);
    //                Debug.Log("x " + x);


    //                shieldHit += 2;
    //                if (shieldHit <= 10)
    //                {


    //                    //SoundManagerSingle.instance.PainSound();
    //                    SoundManagerSingle.instance.PlayerArmoud();

    //                    //// Debug.Log(" shieldHit " + shieldHit);
    //                    sheildSlider.value = shieldHit;
    //                    //call bodyarmou sound
    //                    Particle1.transform.position = other.gameObject.transform.position;
    //                    Particle1.Play();
    //                    photonView.RPC("Injury", RpcTarget.All, x);
    //                    //photonView.RPC("Injury", RpcTarget.AllBufferedViaServer, x);
    //                }
    //                //if (shieldHit > 10 && healthHit < 10)

    //                    if (shieldHit > 10 && healthHit < 10)
    //                {
    //                    //Debug.Log(" healthHit hit");
    //                    // SoundManagerSingle.instance.PlayerArmoudStop();
    //                    SoundManagerSingle.instance.PainSound();

    //                    Particle1.transform.position = other.gameObject.transform.position;
    //                    Particle1.Play();

    //                    healthHit += 1f;
    //                    healthslide.value = healthHit;
    //                    photonView.RPC("Injury", RpcTarget.All, x);

    //                    //hitValue.text = healthHit.ToString();
    //                    //Injury(x);
    //                    //photonView.RPC("Injury", RpcTarget.AllBufferedViaServer, x);
    //                    //Debug.Log(" healthHit.text " + healthHit);
    //                }


    //                if (healthHit == 10)
    //                {

    //                    //turn off turretGun shooting
    //                    turretGunTurnOffSHooting = true;
    //                    Death();
    //                }

    //               //PhotonNetwork.Destroy(other.gameObject);
    //            }


    //        }
    //        if (other.gameObject.tag == "TurretBullet")
    //        {
    //            other.gameObject.SetActive(false);

    //            int x = Random.Range(0, 3);

    //            if (shieldHit <= 10)
    //            {
    //                //SoundManagerSingle.instance.PainSound();
    //                SoundManagerSingle.instance.PlayerArmoud();
    //                shieldHit += 2;
    //                sheildSlider.value = shieldHit;
    //                //call bodyarmou sound

    //            }
    //            if (shieldHit > 10 && healthHit < 10)
    //            {
    //                // SoundManagerSingle.instance.PlayerArmoudStop();
    //                SoundManagerSingle.instance.PainSound();




    //                // Particle1.SetActive(true);

    //                Particle1.transform.position = other.gameObject.transform.position;
    //                Particle1.Play();
    //                healthHit += 1f;
    //                healthslide.value = healthHit;
    //            }

    //            if (healthHit <= 9)
    //            {
    //                Injury(x);
    //            }
    //            if (healthHit == 10)
    //            {

    //                //turn off turretGun shooting
    //                turretGunTurnOffSHooting = true;
    //                Death();
    //            }


    //        }
    //    }
    //}

    public void OnTriggerEnter(Collider collision)
    {
        photonView = GetComponent<PhotonView>();
        if (this.photonView.IsMine) {
            if (collision.gameObject.CompareTag("TurretBullet") || collision.gameObject.CompareTag("Bullet"))
            {

                x = Random.Range(1, 3);
                List<int> Xlist = new List<int>();
                Xlist.Add(x);


                List<int> newList = Xlist.Distinct().ToList();
                int d = newList[0];
                Xlist.Clear();
                if (shieldHit <= 10)
                {
                    //Debug.Log(" shieldHit " + shieldHit);
                    //SoundManagerSingle.instance.PainSound();
                    SoundManagerSingle.instance.PlayerArmoud();
                    shieldHit += 2;
                    sheildSlider.value = shieldHit;
                    //call bodyarmou sound
                    Particle1.transform.position = collision.gameObject.transform.position;
                    Particle1.Play();
                }
                if (shieldHit > 10 && healthHit < 10)
                {
                    Debug.Log("health hit " + healthHit);
                    // SoundManagerSingle.instance.PlayerArmoudStop();
                    SoundManagerSingle.instance.PainSound();

                    // Particle1.SetActive(true);

                    Particle1.transform.position = collision.gameObject.transform.position;
                    Particle1.Play();
                    healthHit += 1f;
                    healthslide.value = healthHit;
                    this.photonView.RPC("Injury", RpcTarget.All, d);
                }

                if (healthHit <= 9)
                {
                    // Debug.Log("Health Hit " + healthHit);
                    //Debug.Log("D before " + d);

                   
                    //Injury(d);
                    newList.Clear();
                }
                if (healthHit == 10)
                {

                    //turn off turretGun shooting
                    turretGunTurnOffSHooting = true;
                    Death();
                }

            }
        }
     
    }
    [PunRPC]
    public void Injury(int x)
    {

        photonView = GetComponent<PhotonView>();
      

            if (this.photonView.IsMine)
        {
            switch (x)
            {
                case 1:
                    anim.SetFloat("injury1", 1);
                    break;
                case 2:
                    anim.SetFloat("injury2", 1);
                    break;
                case 3:
                    anim.SetFloat("injury3", 1);
                    break;
                //case 3:
                //    anim.SetFloat("injury1", 1);
                //    break;
                default:
                    break;



            }

            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            if (x <= 3)
            {
                Invoke("ReturnFromInjru", 0.5f);
            }
        }
    }

    public void Death()
    {
        photonView = GetComponent<PhotonView>();
        if (this.photonView.IsMine)
        {
            anim.SetBool("IsDeath", true);


            #region singlePlayer with Robto
            //anim.SetBool("PlayerInSightFromSingleEnemy", false);
            // ZombiAI.instance.playerInsightFromEnemy = false;
            //TurretGun.instance.activate = false;

            // PlayerDeath = true;

            #endregion
            StartCoroutine("Restart");
        }

    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(10, 10, 100, 30), "Change Scene"))
    //    {
    //        SceneManager.LoadScene(0, LoadSceneMode.Single);
    //    }
    //}

    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(9f);
        healthHit = 0;
        healthslide.value = 0;
        shieldHit = 0;
        //Debug.Log("health  from restat" + healthHit);
        sheildSlider.value = 0;
        PhotonNetwork.Destroy(this.gameObject);
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }


    [PunRPC]
    private void ReturnFromInjru()
    {
        photonView = GetComponent<PhotonView>();
       
            if (this.photonView.IsMine)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            anim.SetFloat("injury1", 0);
            anim.SetFloat("injury2", 0);
            anim.SetFloat("injury3", 0);

        }

    }
}
