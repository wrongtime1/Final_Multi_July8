using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Same as ENEMY AI
public class ENemyMultiMoveTowardsTarget : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chaseWaitTIme = 5f;
    public float patrolWaitTime = 1f;
    public Transform[] patrolWayPoints;

    [Header("Private Fields")]
    private ENemySIghting enemySight;
    private NavMeshAgent agent;
    private Transform player;
    public Transform playerFBX;
    private LastKnowPosition lastPlayerSighting;
    private float chaseTimer;
    private float patrolTImer;
    private Animator anim;
    private NavMeshPath path;
    public List<GameObject> prizesList=new List<GameObject>() ;
    //private int wayPointIndex;
    [HideInInspector]
    public float speed = 1.0f;
    [HideInInspector]
    Vector3 newDirection;
    [HideInInspector]
    Vector3 targetDirection;

    public bool detected;
    // Start is called before the first frame update

    public static ENemyMultiMoveTowardsTarget EMinstance;
    void Awake()
    {
        //prizesList = new List<GameObject>();
        if (EMinstance != null)
        {
            //Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            EMinstance = this;
        }
    }
    void Start()
    {

        anim = GetComponent<Animator>();
        enemySight = GetComponent<ENemySIghting>();
        agent = GetComponent<NavMeshAgent>();
       
     
    }
          
    void FixedUpdate()
    {
        Chasing();
    }

    void Shooting()
    {
     

        agent.isStopped = true;
 
    }
    public void Chasing()
    {
        agent.isStopped = false;
        //ebug.Log("true");
        //List<GameObject> prizesList = new List<GameObject>();
        foreach (var item in prizesList)
        {
           
            if (item.activeInHierarchy)
            {
              
                detected = true; //this will cause the enemy to turn to the prize
                targetDirection = item.transform.position - this.transform.position;
                float singleStep = speed * Time.deltaTime;
                newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 2.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
                agent.SetDestination(item.transform.position);
                //agent.speed = chaseSpeed;
                anim.SetFloat("Speed", 6);
                // anim.SetFloat("AngularSpeed",);
                float distnace = Vector3.Distance(this.transform.position, item.transform.position);

                if (distnace <= 3f)
                {
                    anim.SetFloat("Speed", 0);
                    //agent.destination = this.transform.position;
                    //nav.speed = 0;
                  
                }

                //anim.SetFloat("Speed", agent.speed);
               // StartCoroutine("SetSpeed", item);           
            }
        }


        //nav.destination = player.transform.position;
        // nav.speed = chaseSpeed;
        //if (nav.remainingDistance < nav.stoppingDistance)
        //{
        //    chaseTimer += Time.deltaTime;
        //    if (chaseTimer > chaseWaitTIme)
        //    {
        //       // lastPlayerSighting.position = lastPlayerSighting.resetPosition;
        //       // enemySight.personLastSighting = lastPlayerSighting.resetPosition;
        //       // chaseTimer = 0f;
        //    }
        //}
        //else
        //{
        //   // chaseTimer = 0f;
        //}
    }

    public IEnumerator SetSpeed(GameObject item)
    {
        //prizesList.Add(item);


        path = new NavMeshPath();
        yield return new WaitForSeconds(2f);

        //for (int i = 0; i < path.corners.Length - 1; i++)
        //    Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);

        // NavMeshPath.CalculatePath(item.transform.position, path);
       // NavMesh.CalculatePath(transform.position, item.transform.position, NavMesh.AllAreas, path);

        float distnace = Vector3.Distance(this.transform.position, item.transform.position);
        anim.SetFloat("Speed", 0);

        //clamp Y rotation
        //Quaternion rot = playerFBX.transform.rotation;
        //rot.y = Mathf.Clamp(transform.eulerAngles.y, 0,0);
        ////rot = playerFBX.transform.rotation;

        //transform.Rotate(0, 0, 0, Space.Self);
        //transform.eulerAngles.y = Mathf.Clamp(transform.eulerAngles.y, -90, 90);

 

      //  agent.destination = item.transform.position;
      //  agent.speed = chaseSpeed;
        Debug.Log("distance " + distnace);
       // Debug.Log(agent.destination.ToString());
        if (distnace <= 3f)
        {
            agent.destination = this.transform.position;
            //nav.speed = 0;
            anim.SetFloat("Speed", 0);
        }


        if (path.status == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("can calculate path");
        }
        
        
        
   
        //Debug.Log("the active self " + item.activeSelf);
        // Debug.Log("distance " + distnace);

       

    }



    //void Patrolling()
    //{
       
    //    nav.speed = patrolSpeed;
    //    if (nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
    //    {
            
    //        patrolTImer += Time.deltaTime;
    //        // Debug.Log(patrolTImer);
    //        if (patrolTImer >= patrolWaitTime)
    //        {
    //            //Debug.Log("Patrolling1");
    //            if (wayPointIndex == patrolWayPoints.Length - 1)

    //                wayPointIndex = 0;

    //            else

    //                wayPointIndex++;

    //            patrolTImer = 0f;
    //        }
    //    }
    //    else
    //        patrolTImer = 0f;
    //    nav.destination = patrolWayPoints[wayPointIndex].position;

    //}
}
