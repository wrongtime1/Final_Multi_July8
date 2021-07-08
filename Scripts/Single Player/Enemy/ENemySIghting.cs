using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ENemySIghting : MonoBehaviour
{
    public float FieldOFView=110;
    public bool playerInSight;
    public Vector3 personLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;

    private Animator anim;
    private LastKnowPosition lastPlayerSighting;
    private GameObject player;
    private Animator playerAnim;

    // private PlayerHealth playerHealth;

    private HashIDs hash;
    private Vector3 previousSighting;
    int layerMask = 1 << 12;
    public static ENemySIghting enemySightStatic;
    void Awake()
    {
        if (enemySightStatic != null)
        {
            //Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            enemySightStatic = this;
        }
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<LastKnowPosition>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();
        personLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;


    }

    void Update()
    {
        if (lastPlayerSighting.position != previousSighting)
            personLastSighting = lastPlayerSighting.position;

        previousSighting = lastPlayerSighting.position;
     

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSight = true;

          
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player exited");
            playerInSight = false;
           // FieldOfView.fieldOFviewInstance.playerDetectedinFOV = false;
            anim.SetBool("PlayerInSightSing", playerInSight);
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        if (nav.enabled)
        {
            nav.CalculatePath(targetPosition, path);
        }
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        float pathLength = 0f;

        for (int i = 0; i <path.corners.Length; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }
        return pathLength;
    }

    public void Detect()
    {
        Debug.Log("Player detected ");
        playerInSight = false;

        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius, ~layerMask))
        {

            if (hit.collider.gameObject == player)
            {
                anim.SetBool("PlayerInSightSing", playerInSight);
                //Debug.Log("Sighted" + playerInSight);
                playerInSight = true;
                lastPlayerSighting.position = player.transform.position;
            }
        }
        
        int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

        if (playerLayerZeroStateHash == hash.locomotionState)
        {
            if (CalculatePathLength(player.transform.position) <= col.radius)
            {
                personLastSighting = player.transform.position;
            }
        }
    }
}
//  Debug.Log("player entered");
//  playerInSight = false;

//  Vector3 direction = other.transform.position - transform.position;
//  float angle = Vector3.Angle(direction, transform.forward);
////  Debug.DrawLine(transform.position + transform.up, transform.forward, Color.red, 100f);
//  if (angle< FieldOFView * 0.5f)
//  {

//      RaycastHit hit;

//      if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius,~layerMask))
//      {

//          if (hit.collider.gameObject == player)
//          {
//              anim.SetBool("PlayerInSightSing", playerInSight);
//              //Debug.Log("Sighted" + playerInSight);
//              playerInSight = true;
//              lastPlayerSighting.position = player.transform.position;
//          }
//      }

//  }
//  int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
//  int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;

//  if (playerLayerZeroStateHash == hash.locomotionState)
//  {
//      if(CalculatePathLength(player.transform.position) <= col.radius)
//      {
//          personLastSighting = player.transform.position;
//      }
//  }
//  Debug.DrawLine(transform.position + transform.up, transform.forward, Color.red, 100f);
//if (angle < FieldOFView * 0.5f)
//{

//    //RaycastHit hit;

//    //if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius, ~layerMask))
//    //{

//    //    if (hit.collider.gameObject == player)
//    //    {
//    //        anim.SetBool("PlayerInSightSing", playerInSight);
//    //        //Debug.Log("Sighted" + playerInSight);
//    //        playerInSight = true;
//    //        lastPlayerSighting.position = player.transform.position;
//    //    }
//    //}

//}