using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class FieldOFView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public PhotonView photonView;
    //public static FieldOFView fov;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
       StartCoroutine("FindTargetsWIthDelary", 0.6f);
    }
    IEnumerator FindTargetsWIthDelary(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);


               // FindVisibleTargets();
                }
    }
    void Update()
    {
        FindVisibleTargets();
    }
    void FindVisibleTargets()
    {
            visibleTargets.Clear();
            Collider[] targetsinViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
            for (int i = 0; i < targetsinViewRadius.Length; i++)
            {
                Transform target = targetsinViewRadius[i].transform;
               // Vector3 dirToTarget = (target.position - transform.position).normalized;
              
               // float distanceToTarget = Vector3.Distance(transform.position, target.position);
                RaycastHit hit;
                //Debug.DrawLine(transform.position, target.position, Color.red);
              
                if (!Physics.Linecast(transform.position, target.position, out hit, obstacleMask))
                {


                    if (target.name == "Player" || target.GetComponent<PhotonView>().name=="Player")
                    {
                        //Debug.DrawLine(transform.position, target.position, Color.green);
                        TurretGun.instance.activate = true;
                 
                    }

                  
                }
                if (target.name != "Player")
                {
                    Debug.DrawLine(transform.position, target.position, Color.red);
                    TurretGun.instance.activate = false;
                    //TurretGun.instance.Shoot();
                }
                //if(!Physics.Raycast(transform.position, target.position, out hit, targetMask))
                //{
                //    Debug.DrawLine(transform.position, target.position, Color.yellow);
                //}

                //if (!Physics.Raycast(transform.position, target.position, out hit, obstacleMask))
                //{
                //    Debug.DrawLine(transform.position, target.position, Color.yellow);
                //}

                //if (Vector3.Angle(transform.forward, dirToTarget) < viewRadius / 2)
                //{
                //   // Debug.Log("player connected 2 ");
                //   // float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //        if(!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstacleMask))
                //    {
                //        visibleTargets.Add(target);

                //        //if (visibleTargets.Contains(player.transform))
                //        //{
                //        //    Debug.DrawLine(transform.position, target.position, Color.red);
                //        //    Debug.Log("player connected ");
                //        //}
                //    }


                //}

            }
        }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
