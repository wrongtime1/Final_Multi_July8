using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    [HideInInspector]
    public bool MultiBool;

    [HideInInspector]
    public bool SingleBool;

    public static SelectionScript selectionScript;

    public void Awake()
    {
        selectionScript = this;
    }
    public void LoadSingle()
    {
        SingleBool = true;
        MultiBool = false;
        SceneManager.LoadScene("SinglePlayerSce1SciFi 1", LoadSceneMode.Single);
    }

    public void LoadMulti()
    {
        MultiBool = true;
        SingleBool = false;
        SceneManager.LoadScene("LobbyScene", LoadSceneMode.Single);
    }
}
