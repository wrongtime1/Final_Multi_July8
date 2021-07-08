using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("public")]
    public float maxDmage=120f;
    public float minimDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
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
    [HideInInspector]
    public bool shooting;
    private float scaleDamage;
    private FieldOfView fow;
    private Transform rightHand;
    void Awake()
    {

        fow = GameObject.FindGameObjectWithTag("capsulePlayer").GetComponent<FieldOfView>();

        //enemyShooting = GameObject.FindGameObjectWithTag("Player").GetComponent<EnemyShooting>();
        anim = GetComponent<Animator>();
        laserShotLIne = GetComponentInChildren<LineRenderer>();
        laserSHotLight = GetComponentInChildren<Light>();

        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();
        laserShotLIne.enabled = false;
        laserSHotLight.intensity = 0f;

        scaleDamage = maxDmage - minimDamage;

        
    }

    void Update()
    {

       
        #region MyRegion
        if (ENemySIghting.enemySightStatic.playerInSight == true ) //shot > 0.5f && !shooting && fow.playerDetectedinFOV 
        {
                   
          
            timer += Time.deltaTime;
            anim.SetBool("PlayerInSightSing", true);
            if (timer > 2.4f)
            {
                StartCoroutine("ShootCorotine");

                shooting = true;
                timer = 0;

            }

            laserSHotLight.intensity = Mathf.Lerp(laserSHotLight.intensity, 0f, fadeSpeed * Time.deltaTime);
            // }
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
      
        AudioSource.PlayClipAtPoint(shotClip, laserSHotLight.transform.position);
        laserSHotLight.intensity = flashIntensity;
    }
    private void Shooting()
    {
            shooting = true;
        float fractionalDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
        float damage = scaleDamage * fractionalDistance + minimDamage;

  
        //ShotEffects();
    }
    void OnAnimatorIK()
    {
        //Debug.Log("shooting " + shooting);
        // float aimWieght = anim.GetFloat(hash.aimWeightFloat);
        if (shooting == true)
        {
            //Debug.Log("shooting " + shooting);
            anim.SetIKPosition(AvatarIKGoal.RightHand, player.position);
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        }
    }


    private void ShotEffects()
    {
        //laserShotLIne.SetPosition(0, laserShotLIne.transform.position);
        //laserShotLIne.SetPosition(1, player.position + Vector3.up * 1.5f);
        //laserShotLIne.enabled = true;
        //laserSHotLight.intensity = flashIntensity;
        //AudioSource.PlayClipAtPoint(shotClip, laserSHotLight.transform.position);
      


    }

    //private void ShotEffect()
    //{
    //    Debug.Log("shotEffect");
    //    timer += Time.deltaTime;
    //    Debug.Log(timer);
    //    if (timer > 1.000f)
    //    {
    //        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
    //        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
    //        //laserShotLIne.SetPosition(0, laserShotLIne.transform.position);
    //       // laserShotLIne.SetPosition(1, player.position + Vector3.up * 1.5f);
    //        laserShotLIne.enabled = true;
    //        AudioSource.PlayClipAtPoint(shotClip, laserSHotLight.transform.position);
    //        laserSHotLight.intensity = flashIntensity;
    //        // anim.SetBool("EnemyShoot", true);

    //        //StartCoroutine("raiseArm");

    //        //anim.SetLayerWeight(2, Mathf.Lerp(1, 0, Time.deltaTime * 5f));

    //        //  timer = Time.time + 5;
    //        //  GameObject g = Instantiate(EnemyBullet, shooter.transform.position, Quaternion.identity);


    //        // g.GetComponent<Rigidbody>().AddForce(shooter.transform.forward* 500f);
    //        timer = 0;

    //    }



    //}
}
