using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTIme = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    [Header("Private Fields")]
    private ENemySIghting enemySight;
    private NavMeshAgent nav;
    private Transform player;
    private LastKnowPosition lastPlayerSighting;
    private float chaseTimer;
    private float patrolTImer;
    private int wayPointIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        enemySight = GetComponent<ENemySIghting>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<LastKnowPosition>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (enemySight.playerInSight )
        {
            Shooting();
        }    
        else if( enemySight.personLastSighting != lastPlayerSighting.resetPosition)
        {
           
            Chasing();
        }
        else
        {
          
            Patrolling();
           // Debug.Log("Patrolling");
        }
    }

    void Shooting()
    {
        //Debug.Log("shooting");

        nav.isStopped = true;
       // nav.speed = 0f;
    }
    void Chasing()
    {
       // enemySight.playerInSight = true;
      nav.isStopped = false;
       // Debug.Log("Chasing");
        
        
       // nav.speed = chaseSpeed;
        Vector3 sightingDeltaPos = enemySight.personLastSighting - transform.position;
        if (sightingDeltaPos.sqrMagnitude > 4f)
            nav.destination = enemySight.personLastSighting;
        nav.speed = chaseSpeed;
        if(nav.remainingDistance < nav.stoppingDistance)
        {
            chaseTimer += Time.deltaTime;
            if(chaseTimer > chaseWaitTIme)
            {
                lastPlayerSighting.position = lastPlayerSighting.resetPosition;
                enemySight.personLastSighting = lastPlayerSighting.resetPosition;
                chaseTimer = 0f;
            }
        }
        else
        {
            chaseTimer = 0f;
        }
    }

    void Patrolling()
    {
        //nav.isStopped = false;
        nav.speed = patrolSpeed;
        if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
        {
           // Debug.Log("Patrolling");
            patrolTImer += Time.deltaTime;
           // Debug.Log(patrolTImer);
            if (patrolTImer >= patrolWaitTime)
            {
                //Debug.Log("Patrolling1");
                if (wayPointIndex == patrolWayPoints.Length - 1)

                    wayPointIndex = 0;

                else

                    wayPointIndex++;

               // patrolTImer = 0f;
            }
        }
        else
            patrolTImer = 0f;
            nav.destination = patrolWayPoints[wayPointIndex].position;

            }
        }
    

