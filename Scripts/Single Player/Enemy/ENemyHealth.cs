using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENemyHealth : MonoBehaviour
{
    //public GameObject HasIDGO;
    private HashIDs hash;

    private Animator anim;
    private Animation animation;

    private EnemyShooting enemyShooting;
    public GameObject explosionPrefab;

    public static int  deadCount=0;
    // Start is called before the first frame update
    void Start()
    {
        deadCount = 0;
             
        enemyShooting = GetComponent<EnemyShooting>();
       
        anim = GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag("GameControllerSingle").GetComponent<HashIDs>();
   
    }

  
   public void OnCollisionEnter(Collision other)
    {
     if (other.gameObject.tag == "SinglePlayerBullet")
        {
            enemyShooting.enabled = false;
         
            ContactPoint contact = other.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab.GetComponent<ParticleSystem>(), pos, rot);
    
            int x = Random.Range(0, 3);
            deadCount++;
            Death(deadCount);
            Debug.Log("death count " + deadCount );
       
            switch (x)
            {
                case 0:
                    anim.SetBool("IsInjured1", true);
                    break;
                case 1:
                    anim.SetBool("IsINjured2", true);
                    break;
                case 2:
                    anim.SetBool("IsInjured3", true);
                    break;

            }
            Destroy(other.gameObject);
            Invoke("ReturnToState", 3f);
        }
    }
        
public void Death(int deathCount)
    {
        if (deadCount == 4)
        {
            anim.SetBool("IsDeath", true);
            ENemySIghting.enemySightStatic.playerInSight = false;
            enemyShooting.enabled = false;
            Debug.Log("death");
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            //ENemySIghting.enemySightStatic.playerInSight = false;
            
            anim.SetLayerWeight(1, 0);
           
            anim.SetBool("IsInjured1", false);
            anim.SetBool("IsINjured2", false);
            anim.SetBool("IsInjured3", false);


            StartCoroutine("Regenerate");
        }
    }

    public IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(4f);
        anim.SetBool("IsDeath", false);
        deadCount = 0;
        yield return new WaitForSeconds(3f);
        anim.SetLayerWeight(1, 1);
        // ENemySIghting.enemySightStatic.playerInSight = false;
        enemyShooting.enabled = true;
        //Debug.Log("death");
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<SphereCollider>().enabled = true;
    }
    public IEnumerator TurnOffParticle(GameObject prefab)
    {
        yield return new WaitForSeconds(2.0f);
        prefab.SetActive(false);
    }
    void ReturnToState()
    {
    

             anim.SetBool("IsInjured1", false);
             anim.SetBool("IsINjured2", false);
             anim.SetBool("IsInjured3", false);
             enemyShooting.enabled = true;
    }

 
}
