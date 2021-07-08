using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class EnemyMultiAnimation : MonoBehaviour
{
    public float deadZone = 5f;

    [Header("PRivate stuff")]
    private Transform player;
    private Transform prize;
    private EnemyMultiEnemyInSIght enemySight;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;

    [Header("Script")]
    private AnimatorSetup animSetup;
    private FieldOfView fovscript;
    //private EnemyShooting enemyShoot;
    public static EnemyMultiAnimation enemyAnim ;

    PhotonView photonView;

    

    void Awake()
    {

        if(enemyAnim != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            enemyAnim = this;
        }


        //enemyShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyShooting>();
        fovscript = GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<FieldOfView>();

        //player = GameObject.FindGameObjectWithTag("Player").transform;
        enemySight = GetComponent<EnemyMultiEnemyInSIght>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerMult").GetComponent<HashIDs>();

        nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);
        //anim.SetLayerWeight(1, 1f);
        //anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;

    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    void FixedUpdate()
    {
       
        NavAnimSetup();
    }

    
    void OnAnimatorMove()
    {
        // Debug.Log("nav velocity " + nav.velocity);
        float na = Time.deltaTime;
        //!float.IsNaN();
        if (nav != null)
        {
            try
            {
                //if(!float.IsNaN(Time.deltaTime))
               // Debug.Log("nav velocity INside " + nav.velocity);
                nav.velocity = anim.deltaPosition / Time.deltaTime;
                transform.rotation = anim.rootRotation;
            }
            catch (System.Exception e)
            {
                if (!float.IsNaN(Time.deltaTime))
                {

                    nav.velocity = Vector3.zero;
                }
                
            }
        
        }else if(nav.velocity == null)
        {
            return;
        }
       
      

    }
//take an otpional paramater
  public void NavAnimSetup(params object[] items)
    {
        float speed;
        float angle;
      

        if (enemySight.playerInSight)//|| ENemyMultiMoveTowardsTarget.EMinstance.detected==true

        {
          
            speed = 0;
            prize = GameObject.FindGameObjectWithTag("Prize").transform;
          
            angle = FindAngle(transform.forward, prize.position - transform.position, transform.up);
      

        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

            if (Mathf.Abs(angle) < deadZone)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
            }
        }
        animSetup.Setup(speed, angle);


    }
    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
            return 0;

        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);
        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;

    }




}
