using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiEnemyShooting : MonoBehaviour
{
    [Header("public")]
    //public float maxDmage = 120f;
    //public float minimDamage = 45f;
    //public AudioClip shotClip;
    //public float flashIntensity = 3f;
    public float fadeSpeed = 10f;
    float timer = 0f;
    public GameObject projectile;
    public Transform shooter;

    [Header("Private")]
    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLIne;
    private Light laserSHotLight;
    private SphereCollider col;
    private Transform player;
    //private bool shooting;
    //private float scaleDamage;
    //private FieldOfView fow;

    void Awake()
    {
       //fow = GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<FieldOfView>();

        //enemyShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyShooting>();
        anim = GetComponent<Animator>();
      //  laserShotLIne = GetComponentInChildren<LineRenderer>();
      //  laserSHotLight = GetComponentInChildren<Light>();

     //   col = GetComponent<SphereCollider>();
      //  player = GameObject.FindGameObjectWithTag("Player").transform;
        hash = GameObject.FindGameObjectWithTag("GameControllerMult").GetComponent<HashIDs>();
        laserShotLIne.enabled = false;
        laserSHotLight.intensity = 0f;

       // scaleDamage = maxDmage - minimDamage;


    }

    void Update()
    {

        #region MyRegion
        
        if (EnemyMultiEnemyInSIght.enemySightStatic.playerInSight == true) //shot > 0.5f && !shooting && fow.playerDetectedinFOV 
        {


            timer += Time.deltaTime;
            anim.SetBool("PlayerInSightSing", true);
            if (timer > 2.4f)
            {
                StartCoroutine("ShootCorotine");
                       timer = 0;

            }

            laserSHotLight.intensity = Mathf.Lerp(laserSHotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
            
        }
        #endregion

        #region Shooting1
        //// if (shot > 0.5f && !shooting)
        // {
        //     Shooting();
        // }
        //// if (shot < 0.5f)
        // {
        //     shooting = false;
        // }
        //// laserSHotLight.intensity = Mathf.Lerp(laserSHotLight.intensity, 0f, fadeSpeed * Time.deltaTime); 
        #endregion
    }

    public IEnumerator ShootCorotine()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject bullet = Instantiate(projectile, shooter.transform.position, shooter.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 400f);
        // laserShotLIne.SetPosition(0, laserShotLIne.transform.position);
        //  laserShotLIne.SetPosition(1, player.position + Vector3.up * 1.5f);
        // laserShotLIne.enabled = true;
       // AudioSource.PlayClipAtPoint(shotClip, laserSHotLight.transform.position);
       // laserSHotLight.intensity = flashIntensity;
    }
    private void Shooting()
    {

      //  shooting = true;
        //float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
       // float damage = scaleDamage * fractionalDistance + minimDamage;


       
    }
    //void OnAnimatorIK(int layerIndex)
    //{
    //  //  float aimWieght = anim.GetFloat(hash.aimWeightFloat);
    //  //  anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
    // //   anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWieght);
    //}
        

  
}

