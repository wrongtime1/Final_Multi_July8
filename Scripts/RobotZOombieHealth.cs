using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotZOombieHealth : MonoBehaviour
{
    public bool deathEnemy;
    private CapsuleCollider col;
    private Animator anim;
   
    [Range(0,5)]
    public static int hitAmount=0;
    private EnemyInSIghtZombie enemyZombieSight;
    public static RobotZOombieHealth instance;
    //int injury = Animator.StringToHash("INjury Layer.injury1");
    AnimatorClipInfo[] m_CurrentClipInfo;
    // Start is called before the first frame update
    //public ParticleSystem ps;
    string m_ClipName;
    public ParticleSystem Particle;
    void Awake()
    {
       instance= this;
    }
    void Start()
    {
       /// ps.Play();
       // ps = GetComponentInChildren<ParticleSystem>();
        enemyZombieSight = GetComponent<EnemyInSIghtZombie>();
        col = GetComponentInParent<CapsuleCollider>();
        anim = GetComponentInParent<Animator>();
      // m_CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        //anim.GetCurrentAnimatorStateInfo(0).IsName("StandUp");
       // m_ClipName = m_CurrentClipInfo[1].clip.name;
    }
  
   

    public void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "SinglePlayerBullet")
        {
            hitAmount++;
         //  Debug.Log(col.gameObject.tag);
            int random = Random.Range(0, 2);

            Particle.transform.position = col.gameObject.transform.position;
            Particle.Play();
                    
            if (hitAmount < 4)
            {
              Injury(random);
            }
             if(hitAmount ==5)
            {

              
                deathEnemy = true;
                anim.SetBool("PlayerInSight", false);
                anim.SetLayerWeight(1, 0);
                anim.SetBool("Lower", false);
                StartCoroutine("Death");
            }
           // Debug.Log("static hit " + hitAmount);
         col.gameObject.SetActive(false);
        } 
    }

  

    private void Injury( int injury)
    {
       // ps.Stop();
        switch (injury)
        {
            case 0:
                anim.SetBool("Injury1b", true);
                break;
            case 1:
                anim.SetBool("Injury2b", true);
                break;
            case 2:
                anim.SetBool("Injury3b", true);
                break;

        }
        Invoke("Reset1",1.0f);

    }

    public void Reset1()
    {
        anim.SetBool("Injury1b", false);
        anim.SetBool("Injury2b", false);
        anim.SetBool("Injury3b", false);
    }

    public IEnumerator Death()
    {

        //Disable alarm sound
        SoundManagerSingle.instance.AlarmOff();


        anim.SetTrigger("DeathTrigger");
        yield return new WaitForSeconds(2f);
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return  new  WaitForSeconds(5f);
        enemyZombieSight.detec = false;
        hitAmount = 0;


        StartCoroutine("GetUp");

    }

    public IEnumerator GetUp()
    {
        anim.SetBool("Injury1b", false);
        anim.SetBool("Injury2b", false);
        anim.SetBool("Injury3b", false);
        yield return new WaitForSeconds(10f);
        deathEnemy = false;
        hitAmount = 0;
       
        anim.SetTrigger("StandUp");
        anim.SetLayerWeight(1, 0);
        //anim.SetBool("Lower", true);
        ZombiAI.instance.detection = false;
        ZombiAI.instance.playerInsightFromEnemy = false;
        ZombiAI.instance.counter = false;
        yield return new WaitForSeconds(6f);
        GetComponent<SphereCollider>().enabled = true;
        GetComponentInParent<CapsuleCollider>().enabled = true;
      

    }
}
