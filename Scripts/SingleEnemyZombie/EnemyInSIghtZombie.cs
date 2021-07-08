using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyInSIghtZombie : MonoBehaviour
{
    [Header("public variables")]
    public float fieldOfView = 190;
    
    public Vector3 personalLastSighting;
    public GameObject player;

    [HideInInspector]
    public bool outsideTrigger;

    [Header("PRivate variables")]
    private NavMeshAgent nav;
    private SphereCollider col;
    private Animator anim;
    private LastKnowPosition lastPlayerSIghting;
   
    
    private Animator playerAnim;
    private Health playerHealth;
    private HashIDs hash;
    //private Vector3 previousSighitng;
    //private Vector3 lastKnownSighting;

    //public float chaseSpeed;
    //public bool detecFromBullet;
    public bool detec;

    public LayerMask mask;
    // Start is called before the first frame update

    public static EnemyInSIghtZombie instance;
    void Start()
    {
        instance = this;
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        anim = GetComponent<Animator>();
        lastPlayerSIghting = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<LastKnowPosition>();
        playerAnim = player.GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();

        
    
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "SinglePlayerBulletOne")
    //    {
    //        Debug.Log(other.gameObject.tag);
    //       detecFromBullet = true;
    //      playerInsightFromEnemy = true;
            
    //    //rotate towards the player's bullet origin


    //        //get players

    //        //point raycast at that direction

    //        //if no player from raycast return to patrol

    //        //else  shoot at that direction


    //    }

    //    if (other.gameObject.tag == "Player")
    //    {
    //       // IOV(player);
    //        // playerInsightFromEnemy = true;
    //        // ZombiAI.instance.detectionZombie = true;
    //        // Debug.Log("ZombiAI from trigger Enter " + ZombiAI.instance.detectionZombie);
    //    }
    //}

 

  

   
}
