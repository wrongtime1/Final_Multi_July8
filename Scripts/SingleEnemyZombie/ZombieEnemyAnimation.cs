using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieEnemyAnimation : MonoBehaviour
{
    public float deadZOne = 10f;

    private Transform player;
    //public GameObject gameManager;
    private EnemyInSIghtZombie enemyZombieSight;

    private NavMeshAgent nav;
    private Animator anim;
    private HashIDs hash;
    private ZombieAnimatorSetup animSetup;

    void Awake()
    {

       
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemyZombieSight = GetComponent<EnemyInSIghtZombie>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        //hash = GetComponent<HashIDs>();
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();
        nav.updateRotation = false;
        animSetup = new ZombieAnimatorSetup(anim, hash);
        anim.SetLayerWeight(1, 1f);
        deadZOne *= Mathf.Deg2Rad;


    
    }
    public void Update()
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
       // Debug.Log(enemyZombieSight.playerInsightFromEnemy);
        if (ZombiAI.instance.playerInsightFromEnemy || ZombiAI.instance.playerInsightFOrTurning)
        {
            
            speed = 0f;
            angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
        }
        else
        {
            speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
            angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
            if (Mathf.Abs(angle) < deadZOne)
            {
                transform.LookAt(transform.position + nav.desiredVelocity);
                angle = 0f;
                    }
        }
        animSetup.Setup(speed, angle);

    }
    // Start is called before the first frame update
   
    float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector)
    {
        if (toVector == Vector3.zero)
        
            return 0f;
        
        float angle = Vector3.Angle(fromVector, toVector);
        Vector3 normal = Vector3.Cross(fromVector, toVector);

        angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
        angle *= Mathf.Deg2Rad;
        return angle;
    }   



}
