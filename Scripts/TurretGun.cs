using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class TurretGun : MonoBehaviour
{
    private Animator anim;
    public GameObject head;
    [HideInInspector]
    public Vector3 targetDirection;
    private float speed=3f;
    [HideInInspector]
    public Transform target;
    float singleStep=3f;

    [SerializeField]
    public bool activate;

    public ParticleSystem ps;
    public Light light;

    public Transform bullet;
    public Transform shooter;
    private TrailRenderer tr;
    float timer=1f;
    public bool EnemyinCollider;
    // Start is called before the first frame update

    public static TurretGun instance;
    private AudioSource audioSOurce;
  
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    public static bool activateEngine;

    public bool activateAlarm;

    Scene scene;
    private PhotonView photonView;

    [ExecuteInEditMode]
    void Start()
    {
       
    
        if (instance == null)
        {
            instance = this;
        }

        // light.enabled = false;
        scene = SceneManager.GetActiveScene();
          anim = GetComponent<Animator>();
        audioSOurce = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        photonView = GetComponent<PhotonView>();
    }


    void Update()
    {

        ///photonView.RPC("DetectPlayter", RpcTarget.AllBuffered);
        DetectPlayter();
    }

   
    public void DetectPlayter()
    {
     
        RaycastHit hit;
      
        //Debug.Log(distance);
        #region target
        Collider[] targetsinViewRadius = Physics.OverlapSphere(transform.position, 180f, targetMask);
        for (int i = 0; i < targetsinViewRadius.Length; i++)
        {
            Transform target = targetsinViewRadius[i].transform;

          
            targetDirection = (target.position + Vector3.up) - head.transform.position;
            float distance = Vector3.Distance(transform.forward, targetDirection);

            #endregion
            bool connect = (!Physics.Linecast(transform.position, target.position, out hit, obstacleMask));

            //Debug.DrawLine(transform.position, target.position, Color.red);
        if (connect && distance <= 12f)
        {
            activate = true;
            activateEngine = true;
         


        }

        if (!connect)
        {

            OffTurret();
        }


        if (distance >= 13)
        {
            if (audioSOurce.isPlaying)
            {
                audioSOurce.Stop();
            }
            OffTurret();
        }


        if (activate == true)
            {
                if (!audioSOurce.isPlaying)
                {
                    audioSOurce.Play();
                }

                //  Vector3 direction = player.transform.position - transform.position;

                float angle = Vector3.Angle(targetDirection, transform.forward);

                Vector3 rotationVector = new Vector3(head.GetComponent<Transform>().eulerAngles.x, head.GetComponent<Transform>().eulerAngles.y, head.GetComponent<Transform>().eulerAngles.z);

                head.GetComponent<Transform>().eulerAngles = rotationVector;

                singleStep = speed * Time.deltaTime;
                targetDirection = Vector3.RotateTowards(head.transform.forward, targetDirection + Vector3.down, singleStep, 0.0f);
                //head.transform.position = new Vector3(Mathf.Clamp(0,0,0), head.transform.position.y, head.transform.position.z);
                head.transform.rotation = Quaternion.LookRotation(targetDirection);

                light.enabled = true;
                timer += Time.deltaTime;
                light.intensity += 0.1f * Time.deltaTime;




                if (timer > 1.5f)//&& SinglePlayerHealth.singleHealth.PlayerDeath ==false
                {

                    light.intensity = 0.0f;


                    if (scene.name == "SinglePlayerSce1SciFi 1")
                    {
                        ZombiAI.instance.RunToTurret(this.transform);
                    }

                    if (SinglePlayerHealth.singleHealth.turretGunTurnOffSHooting == false)
                    {
                        StartCoroutine(Shoot());
                    }
                    timer = 0;



                }

         

        }
        }
    }
    public void ActivateSound()
    {
       
            audioSOurce.Play();
      
       
    }

    public void OffTurret()
    {
        StopCoroutine(Shoot());
        ps.Stop();
        ps.gameObject.SetActive(false);
        activate = false;
        light.intensity = 0;
    }
    public IEnumerator Shoot()
    {
       
      
        yield return new WaitForSeconds(2.5f);
        ps.gameObject.SetActive(true);
        ps.Play();
        GameObject obj = TurretObject.currentTurrent.GetPooledObject();
   
        obj.SetActive(true);
        obj.transform.position = this.shooter.transform.position;
        obj.transform.rotation = this.shooter.transform.rotation;

        SoundManagerSingle.instance.TurretShoot();

       
        obj.GetComponent<Rigidbody>().velocity = this.shooter.transform.TransformDirection(Vector3.forward * 15f);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            {
              
                if (scene.name == "SinglePlayerSce1SciFi 1")
                {
                    ZombiAI.instance.RunToTurret(this.transform);
                }
                SoundManagerSingle.instance.Alarm();

                //  activate = true;
                SoundManagerSingle.instance.TurretSound();


            }
      
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SinglePlayerBullet" )
        {
            collision.gameObject.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioSOurce.Stop();
            ps.Stop();
            activate = false;
            EnemyinCollider = true;
            anim.SetTrigger("ShootTrigger");
        }
    }


}
