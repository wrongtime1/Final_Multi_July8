using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldOfView : MonoBehaviour
{
    [HideInInspector]
    Scene scene;
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    [HideInInspector]
    public bool playerDetectedinFOV;

    //private Transform g;
    public static FieldOfView instanceFOV;
    [HideInInspector]
    public Transform bestTarget = null;
    
    void Awake()
    {
        //prizesList = new List<GameObject>();
        if (instanceFOV != null)
        {
            //Destroy(gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            instanceFOV = this;
        }
    }
    void Start()
    {
      // var g=  GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

       
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
         
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float distToTarget = Vector3.Distance(transform.position, target.position);
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
               
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    if (distToTarget < 32f)
                    {
                       // visibleTargets.Add(g);
                        visibleTargets.Add(target);
                        //Debug.Log("List fov " + target.name);
                        //playerDetectedinFOV = true;
                        //ENemySIghting.enemySightStatic.Detect();
                        //multi Enemy
                        
                        float closestDistanceSqr = Mathf.Infinity;
                        foreach (Transform item in visibleTargets)
                        {
                            if (item != null)
                            {
                                Vector3 directionToTarget = item.transform.position - transform.position;
                                //float d = Vector3.Distance(item.position, transform.position);
                                float dSqrToTarget = directionToTarget.sqrMagnitude;

                                if (dSqrToTarget < closestDistanceSqr)
                                {
                                    closestDistanceSqr = dSqrToTarget;
                                    bestTarget = item;
                                    if (scene.name == "GameScene1Multi")
                                    {
                                        EnemyMultiEnemyInSIght.enemySightStatic.FromFOW(item);
                                    }
                                    OnTriggerExit(bestTarget.GetComponent<Collider>());
                                }
                                //Debug.Log("best target " + bestTarget + " " );
                                // Debug.Log(d + item.name);

                            }
                            else
                            {
                                return;
                            }
                        }
                        if (scene.name == "GameScene1Multi")
                        {
                            EnemyMultiEnemyInSIght.enemySightStatic.playerInSight = true;
                        }
                    }
                  
                    
                }

            }
          
            if (Vector3.Angle(transform.forward, dirToTarget) > viewAngle / 2)
            {
              //  Debug.Log("false");
                visibleTargets.Remove(target);
                //foreach (var item in visibleTargets)
                //{
                //   // Debug.Log(item);
                //}
                if (scene.name == "GameScene1Multi")
                {
                    EnemyMultiEnemyInSIght.enemySightStatic.playerInSight = false;
                }else
                { return; }
            }

        }
    }
    // Start is called before the first frame update
    


        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            if (scene.name == "GameScene1Multi")
            {
                EnemyMultiEnemyInSIght.enemySightStatic.playerInSight = false;
                Debug.Log("enemy signt" + EnemyMultiEnemyInSIght.enemySightStatic.playerInSight);

            }
        }
    }

  
    // Update is called once per frame


}
