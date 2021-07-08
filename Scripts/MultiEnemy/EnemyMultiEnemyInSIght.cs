using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMultiEnemyInSIght : MonoBehaviour
{
   // public float FieldOFView = 110;
    public bool playerInSight;
    public Vector3 personLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;

    private Animator anim;
    private LastKnowPosition lastPlayerSighting;
    public GameObject player;
    private Animator playerAnim;

    //private Transform rightHand;
    private Transform chest;
    private Vector3 rotate;
    private Vector3 targetDirection;
    private Vector3 newDirection;
    // private PlayerHealth playerHealth;
    float speed = 1.0f;
    private HashIDs hash;
    private Vector3 previousSighting;
    int layerMask = 1 << 12;
    public static EnemyMultiEnemyInSIght enemySightStatic;

    public Vector3 adjustments;
    float weight = 1f;

    public bool findtarget;
    public Vector3 direction;
    public Transform upperBody;

    [HideInInspector]
    public static bool degreeDetection;
    Quaternion rotation1;

    public Transform head = null;
    public Vector3 lookAtTargetPosition;
    private Vector3 lookAtPosition;
    public bool looking = true;
    private float lookAtWeight = 0.0f;
    public float lookAtCoolTime = 0.2f;
    public float lookAtHeatTime = 0.2f;

    public Transform rightHand = null;
    void Start()
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
        lastPlayerSighting = GameObject.FindGameObjectWithTag("GameControllerMult").GetComponent<LastKnowPosition>();
       // player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerMult").GetComponent<HashIDs>();
        personLastSighting = lastPlayerSighting.resetPosition;
        previousSighting = lastPlayerSighting.resetPosition;
       
       // rightHand = anim.GetBoneTransform(HumanBodyBones.Head);
       // chest = anim.GetBoneTransform(HumanBodyBones.UpperChest);
        degreeDetection = false;

        lookAtTargetPosition = head.position + rightHand.position + transform.forward;
        lookAtPosition = lookAtTargetPosition;
        
    }

    public void OnAnimatorIK(int index)
    {
        if (playerInSight == true)
        {
            //Debug.Log(playerInSight);
            //anim.SetLayerWeight(1, 1);
            // Vector3 direction = player.transform.position - this.transform.position;
            // Quaternion look = Quaternion.LookRotation(direction);
            // rightHand.transform.rotation = look;

            // rightHand.LookAt(player.transform.position);
            //anim.SetLayerWeight(1, 1);

            // rotation1.eulerAngles = new Vector3(AvatarIKGoal.RightHand.transform.position.x, 180, AvatarIKGoal.RightHand.transform.position.y);
            // FieldOfView.instanceFOV.bestTarget.transform.rotation = rotation1;

            // FieldOfView.instanceFOV.bestTarget.transform.position;

            //float yRotation = FieldOfView.instanceFOV.bestTarget.transform.eulerAngles.y;


            if (FieldOfView.instanceFOV.bestTarget == null)
            {
                return;
            }
            else
            {
                //get the vector angle
                direction = FieldOfView.instanceFOV.bestTarget.transform.position - transform.position;
                float s = Vector3.Angle(transform.forward, direction);
                //direction = Vector3.Angle(transform.forward, FieldOfView.instanceFOV.bestTarget.transform.position);

                if (s >= 0 && s <= 60f)
                {
                    //rightHand.LookAt(FieldOfView.instanceFOV.bestTarget.transform.position);
                    degreeDetection = true;
                    // Debug.Log("s " + s);
                    // Debug.Log("field of view Item" + FieldOfView.instanceFOV.bestTarget.transform.name);

                    anim.SetBool("PlayerInSightSing", true);

                     anim.SetIKPosition(AvatarIKGoal.RightHand, FieldOfView.instanceFOV.bestTarget.transform.position + new Vector3(0, 1.5f, 0));
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                    anim.SetLookAtWeight(0.5f,0,0.5f,1);

                    #region head movement

                    lookAtTargetPosition.y = head.position.y + rightHand.position.y;
                    float lookAtTargetWeight = looking ? 1.0f : 0.0f;

                    Vector3 curDir = lookAtPosition - (head.position)-(rightHand.position) ;
                    Vector3 futDir = lookAtTargetPosition - (head.position) - (rightHand.position);

                    curDir = Vector3.RotateTowards(curDir, futDir, 6.28f * Time.deltaTime, float.PositiveInfinity);
                    lookAtPosition = head.position + curDir;

                    float blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
                    lookAtWeight = Mathf.MoveTowards(lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
                    anim.SetLookAtWeight(lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
                    anim.SetLookAtPosition(FieldOfView.instanceFOV.bestTarget.transform.position + new Vector3(0, 1.5f, 0));
                    #endregion


                    //Roate upper body
                    // anim.SetLookAtPosition(FieldOfView.instanceFOV.bestTarget.transform.position);
                    // anim.SetLookAtWeight(1);


                    //upperBody.transform.position;

                    // Vector3 relativePos = FieldOfView.instanceFOV.bestTarget.transform.position - upperBody.transform.position;
                    // Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    // upperBody.transform.rotation = rotation;

                    // anim.SetIKRotation(AvatarIKGoal.RightHand, 5f);
                    //Quaternion handRotation = Quaternion.LookRotation(FieldOfView.instanceFOV.bestTarget.transform.position - transform.position);
                    //Quaternion myRotation = Quaternion.identity;
                    //myRotation.eulerAngles = new Vector3(100, 35, 0);
                    //anim.SetIKRotation(AvatarIKGoal.RightHand, myRotation);

                }
                else if (s >= 61f)
                {
                    degreeDetection = false;
                    anim.SetBool("PlayerInSightSing", false);
                    anim.SetIKPosition(AvatarIKGoal.RightHand, this.transform.position);
                    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
                    // anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 5f);
                    //Quaternion handRotation = Quaternion.LookRotation(objToAimAt.position - transform.position);
                    //Quaternion myRotation = Quaternion.identity;
                    //myRotation.eulerAngles = new Vector3(100, 35, 0);
                    //anim.SetIKRotation(AvatarIKGoal.RightHand, myRotation);
                    anim.SetLookAtWeight(0);

                }
                //anim.SetIKRotation(AvatarIKGoal.RightHand, FieldOfView.instanceFOV.bestTarget.transform.rotation);
            }



        }
        else if (playerInSight == false)
        {
            // Debug.Log(playerInSight);
            anim.SetLayerWeight(1, 0);
            anim.SetIKPosition(AvatarIKGoal.RightHand, this.transform.localPosition);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            anim.SetIKRotation(AvatarIKGoal.RightHand, this.transform.rotation);
            anim.SetLookAtPosition(this.transform.localPosition);
            anim.SetLookAtWeight(0);
        }
    }



    public void FromFOW(Transform x)
    {
        if (playerInSight == true)
        {
            //Vector3 direction = player.transform.position - this.transform.position;
            Debug.DrawLine(transform.position, x.position, Color.green);
            //Debug.DrawLine(transform.position, newDirection, Color.red, 20f);
            //Debug.Log(playerInSight.ToString());
            // anim.SetLayerWeight(1, 1);
           // print(x.name = " From x");
            // Vector3 direction = transform.position - x.position;
            // Quaternion look = Quaternion.LookRotation(direction);
           // rightHand.transform.rotation = look;


            // var direction = player.transform.position - rightHand.position;
            //var toRotation = Quaternion.FromToRotation(rightHand.up, direction);
            //rightHand.rotation = Quaternion.Lerp(rightHand.rotation, toRotation, Time.deltaTime * 5);


            // Vector3 direction = player.transform.position - rightHand.transform.position;
            // Quaternion look = Quaternion.LookRotation(direction);
            //  Vector3 targetDirection = player.transform.position + Vector3.up - rightHand.transform.position;
            //  Vector3 direction = player.transform.position - transform.position;
            // Quaternion look = Quaternion.LookRotation(direction);
            //rightHand.transform.rotation = look;

            //  rightHand.transform.LookAt(player.transform.position * Time.deltaTime * 1.5f);
            //   rightHand.transform.rotation = rightHand.transform.rotation * Quaternion.Euler(targetDirection);

            // rightHand.transform.rotation = look;

            // Vector3 targetDirection = player.transform.position - rightHand.transform.position;

           // transform.Rotate(targetDirection * Time.deltaTime * 1, Space.Self);
            //float singleStep = 1.0f * Time.deltaTime;

            //Vector3 newDirection = Vector3.RotateTowards(rightHand.transform.forward, targetDirection, singleStep, 0.0f);
            //Vector3 direction = player.transform.position - this.transform.position;
           // Debug.DrawLine(transform.position, newDirection, Color.red);

           // transform.rotation = Quaternion.LookRotation(direction);

        }

        
    }

    
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInSight = true;
            //roate the tors if the field of fiew is violated

          //  Debug.Log("playerInSight " + playerInSight);
           // float singleStep = speed * Time.deltaTime;
           // targetDirection = player.transform.position - spine.transform.position;
            //newDirection = Vector3.RotateTowards(spine.forward, targetDirection, singleStep, 0.0f);

            ///transform.rotation = Quaternion.LookRotation(newDirection);
            //Debug.DrawRay(spine.transform.position, newDirection, Color.red);

            //rotate the torso

        }
    }

      
    //void FixedUpdate()
    //{
        
        
    //    //Vector3 targetDir = (player.transform.position - transform.position).normalized;

    //    //check squareMagnitude
    //   // float sqrn = targetDir.sqrMagnitude;
        
    //        //Debug.Log("square value is " + sqrn.ToString());

    //    //check distance
    //    //float distance = Vector3.Distance(this.transform.position, targetDir);
        


    //    //float angle = Vector3.Angle(targetDir, transform.forward);
    //    ////Debug.Log("angle " + angle);
    //    //if (angle < 69.0f && distance < 24f)
    //    //{
    //    //   // playerInSight = true;
    //    //    //print(" close ");
    //    //}
    //    //if (angle > 70.0f && distance > 24f)
    //    //{
    //    //   // playerInSight = false;
    //    //   // print(" far ");
    //    //}
    //}
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

        for (int i = 0; i < path.corners.Length; i++)
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
                //anim.SetBool("PlayerInSightSing", playerInSight);
                //Debug.Log("Sighted" + playerInSight);
               
                
                //playerInSight = true;
               
                
                lastPlayerSighting.position = player.transform.position;
            }
        }
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
// Debug.Log("player entered");
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