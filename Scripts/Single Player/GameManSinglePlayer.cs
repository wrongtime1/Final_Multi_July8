using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon.StructWrapping;
using System;

public class GameManSinglePlayer : MonoBehaviour
{

[SerializeField]
public Text pointText;

    [Header("Placements")]
    public List<GameObject> placements = new List<GameObject>();



    [SerializeField]
    public GameObject MuRedman;  //multiplayer body - no model
    [SerializeField]
    public GameObject MuRobo;
    [SerializeField]
    public GameObject MuDark;
    [SerializeField]
    public GameObject MuRedFemale;
    [SerializeField]
    public GameObject MuLewis;
    [SerializeField]
    public GameObject MuSexyWar;


    [HideInInspector]
    PlayerMoveSingle playerSingle;

    [Header("Main Body")]
    [SerializeField]
    public GameObject _MainBody;

    [Header("CHangeCharacter")]
    [SerializeField]
    public GameObject _ChangeCharacter0;
    [SerializeField]
    public GameObject _ChangeCharacter1;
    [SerializeField]
    public GameObject _ChangeCharacter2;
    [SerializeField]
    public GameObject _ChangeCharacter3;
    [SerializeField]
    public GameObject _ChangeCharacter4;
    [SerializeField]
    public GameObject _ChangeCharacter5;

    //PhotonView photon;
    Scene scene;
    GameObject b;
    GameObject a;

    public Avatar Aavat;
    public Avatar Bavat;
    public Avatar Cavat;
    public Avatar Davat;
    public Avatar Eavat;
    public Avatar Favat;
    [HideInInspector]
    public static GameManSinglePlayer gameManSIgleInstance;
    [HideInInspector]
    public PhotonView photonView;

    [Header("spawninfor")]
    int SpawnPicker;
    public Transform[] SpawnPointTransforms;

    void Awake()
    {

        gameManSIgleInstance = this;
        photonView = GetComponent<PhotonView>();

    }



    void Start()
    {

        if (SelectionScript.selectionScript.MultiBool)
        {


            SelectMulti();

        }

        if (SelectionScript.selectionScript.SingleBool)
        {
            Select();
        }





    }

    public void SelectMulti()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {     //playerMulti != null && _ChangeCharacter1 != null
            if (MuRedman != null || MuRobo != null || MuDark != null || MuRedFemale != null || MuLewis != null || MuSexyWar != null)// && _MainBody.GetComponent<PhotonView>().IsMine&& _ChangeCharacter1.GetComponent<PhotonView>().IsMine
            {

                //Makes the GameObject "newParent" the parent of the GameObject "player".
                // player.transform.parent = newParent.transform;

                //GameObject x = Instantiate(d, UIcontrols.transform.position, UIcontrols.transform.rotation);
                Vector3 position1 = new Vector3(UnityEngine.Random.Range(-30.0f, 30.0f), 0.0f, 0.0f);
                
                if (CharcCHanger.charChanger._playerMultChar == 0)
                {
                    
                    foreach (var item in placements)
                    {
                        placements.Shuffle();
                        
                        
                            GameObject a = Instantiate(item);
                         
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            
                            SpawnPicker= UnityEngine.Random.Range(0, SpawnPointTransforms.Length);

                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuRedman.name, SpawnPointTransforms[SpawnPicker].position, SpawnPointTransforms[SpawnPicker].rotation,0);
                            //ab.transform.position = pos;
                            //a.transform.position = ab.transform.position;
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }

                           
                            // ab.transform.SetParent(a.transform, true);
                            //a.transform.position = ab.transform.position;
                            //item.transform.SetParent(a.transform, true);
                            //  Debug.Log(item.transform.position.ToString());
                            // Debug.Log(item.transform.name.ToString());

                            //a.transform.position = item.transform.position;


                            //}

                      
                    }
                }

                if (CharcCHanger.charChanger._playerMultChar == 1)
                {


                   
                   // PhotonNetwork.Instantiate(MuRobo.name, position1, Quaternion.identity);
                    foreach (var item in placements)
                    {
                        placements.Shuffle();
                       

                          
                            GameObject a = Instantiate(item);
                         
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            SpawnPicker= UnityEngine.Random.Range(0, SpawnPointTransforms.Length);
                           
                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuRobo.name, SpawnPointTransforms[SpawnPicker].position, SpawnPointTransforms[SpawnPicker].rotation,0);
                           // ab.transform.position = pos;
                      
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }


                            // ab.transform.SetParent(a.transform, true);
                            //a.transform.position = ab.transform.position;
                            //item.transform.SetParent(a.transform, true);
                            //  Debug.Log(item.transform.position.ToString());
                            // Debug.Log(item.transform.name.ToString());

                            //a.transform.position = item.transform.position;


                            //}

                       
                    }


                }
                if (CharcCHanger.charChanger._playerMultChar == 2)
                {
                 
                        foreach (var item in placements)
                    {
                        placements.Shuffle();
                        if (item.transform.childCount == 0)
                        {

                            //if(item.transform.name== "firstPlaceMarlon")
                            //{
                            // Debug.Log(item.transform.position.ToString());


                            GameObject a = Instantiate(item);
                            //Vector3 pos = item.transform.position;
                            //-4.251999
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            //Debug.Log("pos " + a.transform.position);
                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuRedFemale.name, pos, Quaternion.identity);
                            ab.transform.position = pos;
                            
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }


                            // ab.transform.SetParent(a.transform, true);
                            //a.transform.position = ab.transform.position;
                            //item.transform.SetParent(a.transform, true);
                            //  Debug.Log(item.transform.position.ToString());
                            // Debug.Log(item.transform.name.ToString());

                            //a.transform.position = item.transform.position;


                            //}

                        }
                        else
                        {
                            return;
                        }
                    }

                }
                if (CharcCHanger.charChanger._playerMultChar == 3)
                {
                    foreach (var item in placements)
                    {
                        placements.Shuffle();
                        if (item.transform.childCount == 0)
                        {

                            //if(item.transform.name== "firstPlaceMarlon")
                            //{
                            // Debug.Log(item.transform.position.ToString());


                            GameObject a = Instantiate(item);
                            //Vector3 pos = item.transform.position;
                            //-4.251999
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            //Debug.Log("pos " + a.transform.position);
                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuDark.name, pos, Quaternion.identity);
                            ab.transform.position = pos;
                            //a.transform.position = ab.transform.position;
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }

                   
                        

                        }
                        else
                        {
                            return;
                        }
                    }

                    //a = PhotonNetwork.Instantiate(MuRobo.name, position1, Quaternion.identity);
                    //a.transform.position = new Vector3(0, 0, 0);
                    //b = Instantiate(_ChangeCharacter3, a.transform.position, transform.rotation);
                    //b.transform.SetParent(a.transform, true);
                    //a.GetComponent<Animator>().avatar = Davat;

                }

                if (CharcCHanger.charChanger._playerMultChar == 4)
                {

                   
                         foreach (var item in placements)
                    {
                        placements.Shuffle();
                        if (item.transform.childCount == 0)
                        {

                            //if(item.transform.name== "firstPlaceMarlon")
                            //{
                            // Debug.Log(item.transform.position.ToString());


                            GameObject a = Instantiate(item);
                            //Vector3 pos = item.transform.position;
                            //-4.251999
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            //Debug.Log("pos " + a.transform.position);
                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuLewis.name, pos, Quaternion.identity);
                            ab.transform.position = pos;
                            //a.transform.position = ab.transform.position;
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }




                        }
                        else
                        {
                            return;
                        }
                    }

                    //a = PhotonNetwork.Instantiate(MuRobo.name, position1, Quaternion.identity);
                    //a.transform.position = new Vector3(0, 0, 0);
                    //b = Instantiate(_ChangeCharacter4, a.transform.position, transform.rotation);
                    //b.transform.SetParent(a.transform, true);
                    //a.GetComponent<Animator>().avatar = Eavat;

                }
                if (CharcCHanger.charChanger._playerMultChar == 5)
                {

                    // MuSexyWar
                    foreach (var item in placements)
                    {
                        placements.Shuffle();
                        if (item.transform.childCount == 0)
                        {

                            //if(item.transform.name== "firstPlaceMarlon")
                            //{
                            // Debug.Log(item.transform.position.ToString());


                            GameObject a = Instantiate(item);
                            //Vector3 pos = item.transform.position;
                            //-4.251999
                            Vector3 pos = new Vector3(a.transform.position.x, -4.251999f, a.transform.position.z);
                            //Debug.Log("pos " + a.transform.position);
                            a.transform.position = pos;
                            GameObject ab = PhotonNetwork.Instantiate(MuSexyWar.name, pos, Quaternion.identity);
                            ab.transform.position = pos;
                            //a.transform.position = ab.transform.position;
                            if (a.transform.position == null)
                            {
                                ab.transform.SetParent(a.transform, true);
                                break;
                            }
                            else if (a.transform.position != null)
                            {
                                return;
                            }




                        }
                        else
                        {
                            return;
                        }
                    }

                    //a = PhotonNetwork.Instantiate(MuRobo.name, position1, Quaternion.identity);
                    //a.transform.position = new Vector3(0, 0, 0);
                    //b = Instantiate(_ChangeCharacter5, a.transform.position, transform.rotation);
                    //b.transform.SetParent(a.transform, true);
                    //a.GetComponent<Animator>().avatar = Favat;

                }

            }
            else
            {
                Debug.Log("place player");
            }

        }
    }

    public void Select()
    {

        if (PlayerPrefs.GetInt("first") == 1)
        {
            PlayerPrefs.DeleteKey("zero");
            a = Instantiate(_MainBody, transform.position, transform.rotation);
            b = Instantiate(_ChangeCharacter1, transform.position + new Vector3(0, -0.91f, 0), transform.rotation);
            b.transform.SetParent(a.transform, true);

            a.GetComponent<Animator>().avatar = Bavat;

        }
        if (PlayerPrefs.GetInt("second") == 2)
        {
            a = Instantiate(_MainBody, transform.position, transform.rotation);
            b = Instantiate(_ChangeCharacter2, transform.position + new Vector3(0, -0.91f, 0), transform.rotation);
            b.transform.SetParent(a.transform, true);
            a.GetComponent<Animator>().avatar = Cavat;
        }
        if (PlayerPrefs.GetInt("third") == 3)
        {
            a = Instantiate(_MainBody, transform.position, transform.rotation);
            b = Instantiate(_ChangeCharacter3, transform.position + new Vector3(0, -0.91f, 0), transform.rotation);
            b.transform.SetParent(a.transform, true);
            a.GetComponent<Animator>().avatar = Davat;
        }
        if (PlayerPrefs.GetInt("fourth") == 4)
        {
            a = Instantiate(_MainBody, transform.position, transform.rotation);
            b = Instantiate(_ChangeCharacter4, transform.position + new Vector3(0, -0.91f, 0), transform.rotation);
            b.transform.SetParent(a.transform, true);
            a.GetComponent<Animator>().avatar = Eavat;
        }
        if (PlayerPrefs.GetInt("fifth") == 5)
        {
            a = Instantiate(_MainBody, transform.position, transform.rotation);
            b = Instantiate(_ChangeCharacter5, transform.position + new Vector3(0, -0.91f, 0), transform.rotation);
            b.transform.SetParent(a.transform, true);
            a.GetComponent<Animator>().avatar = Favat;
        }

    }

    public IEnumerator Lanch()
    {

        yield return new WaitForSeconds(0.2f);

        //a.GetComponent<Animator>().avatar = avat;

        //a.SetActive(true);
        //b.SetActive(true);

    }


[PunRPC]
    public void Get_Total(int amount, int ID)
    {
        Debug.Log(" FROM GAME SINGLE amount " + amount + " id "  + ID);
        //pointText.text= ID.ToString();
       // Time.timeScale=0;
       // PhotonView photonVIEWS = photonVIEWS.FindView

        
    }
    //public void TimerRe()
    //{


    //    x10 = (decimal)timer1;
    //    x10 = System.Math.Round(x10, 1);

    //    timer1 += Time.deltaTime;
    //    timerRemaining.text = x10.ToString();




    //    if (PRizeScore.instance.wonGame)
    //    {
    //       // PauseGame();
    //    }
    //}

    //public void PauseGame()
    //{
    //    Time.timeScale = 0;
    //}

    //public void ResumeGame()
    //{
    //    Time.timeScale = 1;

    //}

    //launch leader boad
    //take input name into leader board
    //save time in PlayerPref variable
    //ushow unlock for next level







}
