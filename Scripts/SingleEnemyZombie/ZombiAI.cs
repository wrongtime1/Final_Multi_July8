using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombiAI : MonoBehaviour
{
    public float patrolSpeed = 0.5f;
    [SerializeField]
    public float chaseSpeed =3f;
    public float chaseWaitTime = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    private EnemyInSIghtZombie enemyInSIght;
    private NavMeshAgent nav;
    private Transform player;
    private Health playerHealth;
    private LastKnowPosition lastPlayerSIghting;

    private float chaseTimer;
    private float patrolTimer;
    private int wayPointIndex;

    private SphereCollider col;
    public LayerMask layerMask;
    private RaycastHit hit;
    float colrad;
    public enum State { chasing, idle, attack, patrolling };
    public State state;
    // Start is called before the first frame update

    //sphere castes
    private Vector3 origin;
    private Vector3 direction;
    public float currentDistance;

    public static ZombiAI instance;
    public bool detectionZombie;
    private Animator anim;


    public LayerMask mask;
 
    [SerializeField]
    public bool detection;
    [SerializeField]
    public bool playerInsightFromEnemy;
    //reset
    float distance;
   
    public bool playerInsightFOrTurning;
    public bool counter;
    public bool playerExitTrigger;

    public bool DetectFromTurret;

    public Transform secondBody;
    void Start()
    {
     
             


        instance = this;
     
        anim = GetComponent<Animator>();
        col = GetComponent<SphereCollider>();
           enemyInSIght = GetComponent<EnemyInSIghtZombie>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //lastPlayerSIghting= GameObject.FindGameObjectWithTag()
        lastPlayerSIghting = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<LastKnowPosition>();
        //colrad = col.radius;
              
}


    void FixedUpdate()
    {
   
     

        if (playerInsightFromEnemy == true || playerExitTrigger==true || DetectFromTurret==true) //player detected
            {
           // Debug.Log("playerInsightFromEnemy " + playerInsightFromEnemy);
            StartCoroutine("Delay");  

            }

   
        if (playerInsightFromEnemy == false && detection == false && ZoomShooting.instance.shooting == false) //and is not shooting
        {
           
            state = State.patrolling;
            // Debug.Log(state);
            Patrolling();
            nav.isStopped = false;
        }
      
        if (SinglePlayerHealth.singleHealth.PlayerDeath && RobotZOombieHealth.hitAmount !=5)
        {
            nav.isStopped = false;
            anim.SetTrigger("PlayerDeath");
         



        }

    }

    public IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
    
            if (detection == true || DetectFromTurret==true)
            {
         playerInsightFromEnemy = false;
            // nav.SetDestination(player.transform.position);
           // Debug.Log("playerInENmyDeteciotn " + playerInsightFromEnemy);
            Chasing();
            }
            
       
    }
    public void Chasing()
    {

        float distance = Vector3.Distance(player.transform.position, transform.position);
    // Debug.Log("distnace " + distance);
        if(nav.remainingDistance >= 3f )
        {
         

            state = State.chasing;
    
            nav.speed = chaseSpeed;
           anim.SetFloat("Speed", chaseSpeed);
            nav.SetDestination(player.transform.position + player.TransformDirection(transform.forward));




        }

        else if (nav.remainingDistance >= 2f && nav.remainingDistance <=4)
        {
            playerInsightFromEnemy = true;
            //nav.SetDestination(this.transform.position);
            state = State.idle;
            // nav.isStopped = true;
            anim.SetFloat("Speed", 0.0f);
         
        }
       


        #region stuff
        // throw new NotImplementedException();

        //if (sightinDeltaPos.sqrMagnitude > 4)
        //{
        //    nav.destination = enemyInSIght.personalLastSighting;
        //}
        //nav.speed = chaseSpeed;

        //if (nav.remainingDistance < nav.stoppingDistance)
        //{

        //    Debug.Log("nav.remainingDistance" + nav.remainingDistance);

        //    Debug.Log("nav.stoppingDistance" + nav.stoppingDistance);
        //    chaseTimer += Time.deltaTime;
        //    if (chaseTimer > chaseWaitTime)
        //    {
        //        Debug.Log(" stop chasing");
        //        lastPlayerSIghting.position = lastPlayerSIghting.resetPosition;
        //        enemyInSIght.personalLastSighting = lastPlayerSIghting.resetPosition;
        //        chaseTimer = 0f;

        //    }
        //}
        //else
        //    chaseTimer = 0f;
        #endregion
    }

    public void Patrolling()
    {

       
        //EnemyInSIghtZombie.instance.personalLastSighting = new Vector3(1000, 1000, 1000);
        //nav.speed = patrolSpeed;
        anim.SetFloat("Speed", nav.speed = patrolSpeed);
        if (nav.remainingDistance <= nav.stoppingDistance && playerInsightFromEnemy==false)
        {

            patrolTimer += Time.deltaTime;

            if (patrolTimer >= patrolWaitTime)
            {
                if (wayPointIndex == patrolWayPoints.Length - 1)

                    wayPointIndex = 0;


                else

                    wayPointIndex++;
                patrolTimer = 0f;
            }
        }
        else
            patrolTimer = 0f;

        nav.destination = patrolWayPoints[wayPointIndex].position;
        //Debug.Log("nav.name " + patrolWayPoints[wayPointIndex].name);

    }
    //void CalculatePath1(NavMeshAgent nav, Vector3 navdestination)
    //{
    //    NavMeshPath path = new NavMeshPath();
    //    if (nav.enabled)
    //    {
    //        nav.CalculatePath(navdestination, path);
    //    }

    //    Vector3[] allwayPoints = new Vector3[path.corners.Length + 2]; // 2 allows for enemy and player position
    //    allwayPoints[0] = transform.position;
    //    allwayPoints[allwayPoints.Length - 1] = navdestination;

    //    for (int i = 0; i < path.corners.Length; i++)
    //    {
    //        allwayPoints[i + 1] = path.corners[i];
    //        Debug.Log("allwayPoints" +allwayPoints[i + 1]);
           
    //        if (allwayPoints[i + 1] != null)
    //        {
    //           // RotateTowards(allwayPoints[i + 1]);
    //        }
    //        else
    //        {
    //            return;
    //        }
    //    }
        
    //    //create a float to store th path length
    //    float pathLength = 0f;

    //    for (int i = 0; i < allwayPoints.Length-1; i++)
    //    {
    //        pathLength += Vector3.Distance(allwayPoints[i], allwayPoints[i + 1]);
    //    }
       
      
     
    //    //Debug.Log(" path length to player " + pathLength);
    //    // Debug.Log("  path.status " + path.status);

    //}

    void RotateTowards(Vector3 pos)
    {
            Quaternion d = Quaternion.LookRotation(pos, Vector3.up);
       transform.rotation = d;
    }
   
    
    //void CalculatePath()
    //{
  
    //    float distance = Vector3.Distance(player.transform.position, this.transform.position);
    //    if (nav.remainingDistance < 2)
    //    {
    //        if (distance > col.radius)
    //        {
    //            if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), player.transform.position, out hit))
    //            {
    //                if (hit.collider.name != "Player")
    //                {
    //                   // ZombiAI.instance.playerInsightFromEnemy = false;
    //                    detection = false;
    //                }
    //            }
    //        }
    //    }
    //}

    public void RunToTurret(Transform turrentCurrent)
    {

         detection = true;
        //

    
      //  Debug.Log("ZoomShooting.instance.shooting " + ZoomShooting.instance.shooting);
        if (ZoomShooting.instance.shooting ==false )
        {
              nav.speed = chaseSpeed;
        anim.SetFloat("Speed", chaseSpeed);
            nav.SetDestination(turrentCurrent.position);
            //detection = false;
          
        }
       
        //playerInsightFromEnemy = true;
       
      
     
        StartCoroutine("ReturnToPatrol");
    }

    public IEnumerator ReturnToPatrol()
    {
        yield return new WaitForSeconds(15f);
        //Debug.Log(playerInsightFromEnemy);
        if (TurretGun.instance.activate==false )
        {
            Vector3 fwd = player.transform.position - this.transform.position;
            float a = Vector3.Angle(fwd, transform.forward);
       
            float s = Vector3.SqrMagnitude(fwd);
          
     
           
            if (a > 50f && Mathf.Sqrt(s) > 13f) //Mathf.Sqrt(s) > 16f
            {
                SoundManagerSingle.instance.AlarmOff();
                detection = false;
               
            }
           // Patrolling();
        }
       
    }
    public void OnTriggerEnter(Collider other)
    {
        //ounter = false;
        if (other.gameObject.tag == "SinglePlayerBullet" )
        {
            counter = true;
         Debug.Log("bullet " + other.transform.name);
            //the player bullet will move the enemy to the last players popsition of the bullet release
           // RunTOLaskKNownPlayerposition();
         IOV(other.gameObject);
        }
       
        else if(other.gameObject.tag == "Player" && counter ==false)
        {
           
            
         IOV(other.gameObject);
        }
     
    }

    public void RunTOLaskKNownPlayerposition()
    {
       
        nav.SetDestination(player.transform.position + player.InverseTransformDirection(transform.forward));
      
    }

    public void IOV(GameObject other)
    {
        
        Vector3 direction = other.transform.position - transform.position;
        // Vector3 fwd = transform.TransformDirection(Vector3.forward);

        float angle = Vector3.Angle(direction, transform.forward);

        
        RaycastHit hit;
       

        if (angle <= 180f)
       {
           

            if (Physics.Raycast(transform.position, direction, out hit, mask))
            {
               
                playerInsightFromEnemy = true;
                detection = true;
              //  Debug.Log(" angle greater than");
             RunTOLaskKNownPlayerposition();


            }
        }

        if (counter == true)
        {
          //  Debug.Log(" angle greater than");
            if (Physics.Raycast(transform.position, direction, out hit, mask))
            {

                playerInsightFromEnemy = true;
                detection = true;
               // Debug.Log(" hit " + hit.collider.gameObject);
            
            }
        }


    }
  

    public void OnTriggerStay(Collider other)
    {

        float d = Vector3.Distance(player.transform.position, this.transform.position);
       if (d <=3f)
      {
            if (other.gameObject.tag == "Player")
            {
            // Debug.Log("Player stay");
               // playerInsightFOrTurning = true;
            //  IOV(other.gameObject);
                //detection = true;
              playerInsightFromEnemy = true;
                //playerInsightFOrTurning = true;

            }
       }
        else
        {
            return;
        }
    }

    public void OnTriggerExit(Collider other)
    {
       
            if (other.gameObject.tag == "Player")
            {
            playerInsightFromEnemy = false;
            // IOV(other.gameObject);

            //Debug.Log("Exited ");
            playerExitTrigger = true;
            nav.SetDestination(player.transform.position + (Vector3.forward * 2));

        }
       
    }
}
