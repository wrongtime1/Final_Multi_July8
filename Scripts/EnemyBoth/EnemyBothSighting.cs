using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBothSighting : MonoBehaviour
{
    public float fieldOfViewAngle = 110;
    public bool playerInSight;
    public Vector3 personalLastSIghting;
    private LastKnowPosition lastPlayerSighting;
    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private GameObject Player;
    private SinglePlayerHealth playerHealth;
    private Vector3 previousSightings;
    // Start is called before the first frame update
    Vector3 direction;
    RaycastHit hit;
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
       // playerHealth = player.GetComponent<SinglePlayerHealth>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<LastKnowPosition>();
            
    
    }
    
   

    // Update is called once per frame
    void Update()
    {
        
        if(lastPlayerSighting.position != previousSightings)
        {
            personalLastSIghting = lastPlayerSighting.position;

        }

        previousSightings = lastPlayerSighting.position;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInSight = false;//dfaul to false
            Debug.Log(playerInSight);
            direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                Debug.Log("angle " + angle);
                Debug.Log("fieldOfViewAngle" + fieldOfViewAngle);

                if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)){
                    Debug.Log(hit.collider.name);
                        Debug.DrawRay(transform.position + transform.up, direction,  Color.green, 250f);

                    if (hit.collider.gameObject.name == "Player")
                    {

                        playerInSight = true;
                        Debug.Log(playerInSight);
                        lastPlayerSighting.position = Player.transform.position;
                    }
                }
            }
            int playerLayerZeroStateHsh = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            int playerLayerOneStateHas = anim.GetCurrentAnimatorStateInfo(1).fullPathHash;


        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            playerInSight = false;
        }
    }

    float CaculatePathLength (Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
      
            allWayPoints[0] = transform.position;
            allWayPoints[allWayPoints.Length - 1] = targetPosition;


            for(int i=0; i <path.corners.Length; i++)
            {
                allWayPoints[i + 1] = path.corners[i];
            }

            float pathLength = 0f;

            for (int i = 0; i < allWayPoints.Length-1; i++)
            {
                pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);

            }
            return pathLength;
        
       
    }
}
