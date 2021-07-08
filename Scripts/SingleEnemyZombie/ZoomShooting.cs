using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomShooting : MonoBehaviour
{
    public float maximDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;
    float timer = 0f;
    public GameObject projectile;
    public Transform shooter;

    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLine;
    private Light laserShotLIght;
    private SphereCollider col;
    private Transform player;
    private Health playerHealth;
    public bool shooting;
    private float scaleDamage;
    private EnemyInSIghtZombie enemyZombieSight;

    Transform chest;
    Transform rightHand;
    public Vector3 offset;

    [HideInInspector]
    public Vector3 direction;
    public RaycastHit hit;
    int layerMask = 1 << 8;
    public LayerMask myLayermask;

    public bool getUpB;
    int w;
    string[] clipName;

    public static ZoomShooting instance;
    public GameObject hitPrefab;
    void Awake()
    {
        instance = this;
        enemyZombieSight = GetComponent<EnemyInSIghtZombie>();
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLIght = GetComponentInChildren<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();

        laserShotLine.enabled = false;
        laserShotLIght.intensity = 0f;
        scaleDamage = maximDamage - minimumDamage;

        chest = anim.GetBoneTransform(HumanBodyBones.Chest);
        rightHand = anim.GetBoneTransform(HumanBodyBones.RightIndexDistal);
    }

    private void LateUpdate()
    {
       
        if (shooting == true)
        {
            //Rotate the enemy right hand
            Vector3 t = player.transform.position - rightHand.transform.position;
            Quaternion look = Quaternion.LookRotation(t, Vector3.one);
            rightHand.transform.rotation = look;
            chest.LookAt(player.transform.position);

            //rotate the shooter
            Vector3 shooter1 = player.transform.position - shooter.transform.position;
            Quaternion shotL = Quaternion.LookRotation(shooter1, Vector3.forward);
            shooter.transform.rotation = shotL;

        }
    }
    void FixedUpdate()
    {
      
        if (ZombiAI.instance.detection == true  )
        {
           
            direction = player.transform.position - transform.position;
       
            if (Physics.Raycast(transform.position, direction, out hit, col.radius, myLayermask))
            {
               
         
                if (hit.transform.tag == "Player")
                {
                    

              ///  Debug.Log("hit.collider.transform " + hit.transform);
                   // Debug.DrawRay(transform.position + new Vector3(0, 1, 0), direction, Color.red, col.radius);
                    timer += Time.deltaTime;
                   
                    //Debug.Log("EnemyInSIghtZombie.instance.playerInsightFromEnemy " + EnemyInSIghtZombie.instance.playerInsightFromEnemy);
                    //Debug.Log("RobotZOombieHealth.instance.deathEnemy " + RobotZOombieHealth.instance.deathEnemy);
                    //Debug.DrawRay(transform.localPosition, player.transform.position, Color.blue);
                    // Debug.DrawRay(transform.position + Vector3.zero, direction * hit.distance, Color.red, myLayermask);
                    w= anim.GetCurrentAnimatorClipInfo(0).Length;
                  clipName = new string[w];
                    for (int i = 0; i < w; i += 1)
                    {
                        clipName[i] = anim.GetCurrentAnimatorClipInfo(0)[i].clip.name;
                        if (clipName[i].ToString() == "getUp")
                        {
                            getUpB = true;
                        }
                        else if (clipName[i].ToString() != "getUp")
                        {
                            getUpB = false;
                        }
                       // Debug.Log(clipName[i].ToString());
                    }

                    if (timer > 1.3f && getUpB==false)
                    {
                                            
                            shooting = true;
                            StartCoroutine("ShootCorotine");
                            anim.SetBool("PlayerInSight", true);
                            anim.SetLayerWeight(1, 1);
                            anim.SetBool("Lower", true);

                            timer = 0;
                            laserShotLIght.intensity = Mathf.Lerp(laserShotLIght.intensity, 0f, fadeSpeed * Time.deltaTime);
                     
                    }
                }
            }
         


            }

            if ( SinglePlayerHealth.singleHealth.PlayerDeath == true || RobotZOombieHealth.instance.deathEnemy)//true
            {
            
               anim.SetLayerWeight(1, 0);
            //   // AudioSource.PlayClipAtPoint(shotClip, laserShotLIght.transform.position);
                StopCoroutine("ShootCorotine");
               shooting = false;
               laserShotLIght.enabled = false;

          
          }

        }

    void OnAnimatorIK(int n)
    {
       
        if (shooting) {
            
           // float aimWeight = anim.GetFloat(hash.aimWeightFloat1);
          //  anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
          //  anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
        }
        if (shooting == false)
        {
            anim.SetIKPosition(AvatarIKGoal.RightHand, this.transform.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    
    }
    public IEnumerator ShootCorotine()
    {
     
        yield return new WaitForSeconds(1.75f);

        SoundManagerSingle.instance.EnemyShoot();
        GameObject bullet = Instantiate(projectile, shooter.transform.position, shooter.transform.rotation) as GameObject;
       
      Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.GetComponent<Rigidbody>().AddForce(shooter.transform.forward * 400f);
        DestroyPRefab();


    }

    public void DestroyPRefab()
    {
        Destroy(hitPrefab,2);
    }
    public void Shoot()
    {
      
        float fractionalDIstance = (col.radius- Vector3.Distance(transform.position, player.position))/ col.radius;
        float damage = scaleDamage * fractionalDIstance + minimumDamage;
        //ShotEffect();
    }


 
}
