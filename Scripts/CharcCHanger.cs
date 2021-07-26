using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class CharcCHanger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler { 

    public static CharcCHanger charChanger;

    [Header("Get PlayerForms")]
    public int _playerMultChar; //Main red guy
  

    
    public List<GameObject> ImageList ;
    
    [Header("Slider")]
    public Slider slide;
    public static int getSlideValue;

    public int x;
    public delegate void onCharactarImageChange();
    public static event onCharactarImageChange changeEvent;

    [HideInInspector]
    public Scene scene;
    GameObject[] g;
     void Awake()
    {
        charChanger = this;
       
         
    }
    void Start()
    {
        //g = GameObject.FindGameObjectsWithTag("ImageChangeChar");


      

        for (int i = 1; i < ImageList.Count; i++)
        {
           // item.SetActive(false);
            ImageList[i].SetActive(false);
        }

        
        PlayerPrefs.DeleteKey("zero");
        PlayerPrefs.DeleteKey("first");
        PlayerPrefs.DeleteKey("second");
        PlayerPrefs.DeleteKey("third");
        PlayerPrefs.DeleteKey("fourth");
        PlayerPrefs.DeleteKey("fifth");
    }



    public void CharacterChange0()
    {
        Debug.Log(" CharacterChange0 ");
        //PlayerPrefs.SetInt("zero", 0);
        _playerMultChar = 0;
        SceneManager.LoadScene("SelectionScene");
    }
    public void CharacterChange1()
    {
       // Debug.Log(" CharacterChange1 ");
        //PlayerPrefs.SetInt("first", 1);
        _playerMultChar = 1;
        SceneManager.LoadScene("SelectionScene");
    }

    public void CharacterChange2()
    {

        // PlayerPrefs.SetInt("second", 2);
        _playerMultChar = 2;
        SceneManager.LoadScene("SelectionScene");


    }
    public void CharacterChange3()
    {

        //PlayerPrefs.SetInt("third", 3);
        _playerMultChar = 3;
        SceneManager.LoadScene("SelectionScene");

    }
    public void CharacterChange4()
    {

        //PlayerPrefs.SetInt("fourth", 4);
        _playerMultChar = 4;
        SceneManager.LoadScene("SelectionScene");

    }
    public void CharacterChange5()
    {

        //PlayerPrefs.SetInt("fifth", 5);
        _playerMultChar = 5;
        SceneManager.LoadScene("SelectionScene");
    }



    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("OnEndDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(getSlideValue);

        SelectI();
    }
    public void SelectI()
    {
        x = getSlideValue;
        Debug.Log(getSlideValue = (int)slide.value);
     
        for (int i = 0; i < ImageList.Count; i++)
        {
            if (i == getSlideValue)
            {

                ImageList[i].SetActive(true);

            }
            else
            {
                if (i != getSlideValue)
                {

                    ImageList[i].SetActive(false);
                }
            }

        }

     

    }

 
}
