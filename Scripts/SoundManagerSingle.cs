using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerSingle : MonoBehaviour
{
    public static SoundManagerSingle instance;
    private AudioSource[] audiosources;
    public bool IncreaseIntensity;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audiosources = GetComponents<AudioSource>();
        audiosources[0].GetComponent<AudioSource>().enabled = true;
        audiosources[1].GetComponent<AudioSource>().enabled = true;
        audiosources[2].GetComponent<AudioSource>().enabled = true;
        audiosources[3].GetComponent<AudioSource>().enabled = true;
        audiosources[4].GetComponent<AudioSource>().enabled = true;

        //male sound
        audiosources[5].GetComponent<AudioSource>().enabled = true;
        audiosources[6].GetComponent<AudioSource>().enabled = true;
        audiosources[7].GetComponent<AudioSource>().enabled = true;
        audiosources[8].GetComponent<AudioSource>().enabled = true;

        //Alarm sound
        audiosources[9].GetComponent<AudioSource>().enabled = true;
        //print("first sourc e " + audiosources[0].name);
        //print("first sourc e " + audiosources[1].name);
        //print("first sourc e " + audiosources[2].name);
        // audiosources[0].GetComponent<AudioSource>().Play();
        // EnemyShoot();
       
    }

    // Update is called once per frame

  
    public void EnemyShoot()
    {
        audiosources = GetComponents<AudioSource>();
       
        audiosources[1].GetComponent<AudioSource>().enabled = true;
        audiosources[1].GetComponent<AudioSource>().Play();


    }

    public void PlayerSHoot()
    {
        audiosources[1].GetComponent<AudioSource>().Play();
    }

    public void PlayerArmoud()
    {
        audiosources[4].Play();
    }
    public void PlayerArmoudStop()
    {
        audiosources[4].Stop();
    }
    public void TurretShoot()
    {
        audiosources[3].GetComponent<AudioSource>().Play();
        
    }

    public void TurretSound()
    {
        //Debug.Log(" Playing ");
        //audiosources[4].GetComponent<AudioSource>().Play();
    }
    public void PainSound()
    {
      
        //audiosources[5].Play();
        //Debug.Log("pain sound");
        int randomX = Random.Range(0, 3);
        // Debug.Log(randomX);
        switch (randomX)
        {
            case 0:
                audiosources[5].Play();
                break;

            case 1:
                audiosources[6].Play();
                break;

            case 2:
                audiosources[7].Play();
                break;

            default:
                audiosources[8].Play();
                break;
        }
    }

    public void Alarm()
    {

        audiosources[9].Play();
        //audiosources[9].volume = 0;
        IncreaseIntensity = true;
     
    }

    public void AlarmOff()
    {
        audiosources[9].Stop();
    
       
    }


    public void Update()
    {
       // laserShotLIght.intensity = Mathf.Lerp(laserShotLIght.intensity, 0f, fadeSpeed * Time.deltaTime);

        if (IncreaseIntensity)
        {
           
            audiosources[9].volume = Mathf.Lerp(audiosources[9].volume, 1, Time.deltaTime * 0.2f);
           
        }
    }

}
