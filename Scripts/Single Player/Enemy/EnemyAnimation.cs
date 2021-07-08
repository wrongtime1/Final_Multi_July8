using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    public float deadZone;

    [Header("PRivate stuff")]
    private Transform player;
    private ENemySIghting enemySight;
    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;

    [Header("Script")]
    private AnimatorSetup animSetup;
    private FieldOfView fovscript;
    //private EnemyShooting enemyShoot;
 

   void Awake()
    {
        //enemyShoot = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyShooting>();
        fovscript = GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<FieldOfView>();
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemySight = GetComponent<ENemySIghting>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();

        nav.updateRotation = false;
        animSetup = new AnimatorSetup(anim, hash);
        anim.SetLayerWeight(1, 1f);
        //anim.SetLayerWeight(2, 1f);

        deadZone *= Mathf.Deg2Rad;

    }

    void FixedUpdate()
    {
        NavAnimSetup();
    }

    void OnAnimatorMove()
    {
        nav.velocity = anim.deltaPosition / Time.deltaTime;
        transform.rotation = anim.rootRotation;
    }
    void NavAnimSetup()
    {
        float speed;
        float angle;


        // if (fovscript.playerDetectedinFOV != false)
        //{
        // if (fovscript.playerDetectedinFOV == true)
       
           if (ENemySIghting.enemySightStatic.playerInSight)
        
        {
               // speed = 0f;
          // Debug.Log("True");
                speed = 0;
                angle = FindAngle(transform.forward, (player.position - transform.position).normalized, transform.up);
              
                //call shoot function

            }
       
       
            else
            {
                speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
                angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
                    
                if(Mathf.Abs(angle) < deadZone)
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
