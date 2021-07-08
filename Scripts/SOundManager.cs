using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SOundManager : MonoBehaviour
{
    public static SOundManager instance;
    private AudioSource[] audiosources;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audiosources = GetComponents<AudioSource>();
        audiosources[0].GetComponent<AudioSource>().enabled = true;
        //print("first sourc e " + audiosources[0].name);
        //print("first sourc e " + audiosources[1].name);
        //print("first sourc e " + audiosources[2].name);
        // audiosources[0].GetComponent<AudioSource>().Play();
        // EnemyShoot();
    }

    // Update is called once per frame
   
    [PunRPC]
    public void EnemyShoot()
    {
        audiosources = GetComponents<AudioSource>();
        Debug.LogWarning("connected ");
        audiosources[0].GetComponent<AudioSource>().enabled = true;
        audiosources[0].GetComponent<AudioSource>().Play();

       
    }

    public void PlayerSHoot()
    {
        audiosources[1].GetComponent<AudioSource>().Play();
    }

    public void PainSound()
    {
        int randomX = Random.Range(0, 3);
        switch (randomX)
        {
            case 0:
                //audiosources[0].GetComponent<AudioSource>().Play();
                break;

            case 1:
                //audiosources[0].GetComponent<AudioSource>().Play();
                break;

            case 2:
                //audiosources[0].GetComponent<AudioSource>().Play();
                break;

            default:
                //audiosources[0].GetComponent<AudioSource>().Play();
                break;
        }
    }
}
