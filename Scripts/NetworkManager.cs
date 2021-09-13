using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

namespace Com.Marand.FirstPerson
{  
    public class NetworkManager : MonoBehaviourPunCallbacks { 
    [Header("Connection Status")]
    public Text connectionStatusText;

    [Header("Login UI Panel")]
    public InputField playerNameInput;
    public GameObject Login_UI_Panel;

    [Header("game OPtion UI Panel")]
    public GameObject GameOptions_UI_Panel;

    [Header("Create room ui Panel")]
    public GameObject CreateRoom_UI_Panel;
    public InputField roomNameInputField;
    public InputField maxPlayerInputField;
        [Header("Scene selection")]
        public Button Scene1;
        public Button Scene2;
        public Button Scene3;
        [HideInInspector]
        public static int SceneSelection;
        [Header("Inside room IU Panel")]
    public GameObject InsideRoom_UI_Panel;
    public Text roomInfoText;
    public GameObject playerListPrefabl;

    [Header("Room List UI Panel")]
    public GameObject RoomList_UI_Panel;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParentGameObject;
    public GameObject playerListContent;
    public GameObject startGameButton;


    [Header("Join Random Room UI Panel")]
    public GameObject JoinRandomRoom_UI_Panel;

    [Tooltip("The maximum nuber of player per room. WHen a room is fill, it can't be joined by new player and so new ")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    private Dictionary<string, RoomInfo> cahcedRoomList;
    private Dictionary<string, GameObject> roomListGameObjects;
        private Dictionary<int, GameObject> playerListGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        ActivatePanel(Login_UI_Panel.name);
        cahcedRoomList = new Dictionary<string, RoomInfo>();
        roomListGameObjects = new Dictionary<string, GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        connectionStatusText.text = "Connection status: " + PhotonNetwork.NetworkClientState;
    }

        #region Pun Callbacks

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
            ActivatePanel(InsideRoom_UI_Panel.name);
            roomInfoText.text = " Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
                    " Players / Max.player: " +
                    PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                    PhotonNetwork.CurrentRoom.MaxPlayers;

            GameObject playerListGameobject = Instantiate(playerListPrefabl);
            playerListGameobject.transform.SetParent(playerListContent.transform);
            playerListGameobject.transform.localScale = Vector3.one;

            playerListGameobject.transform.Find("PlayerNameText").GetComponent<Text>().text = newPlayer.NickName;

            if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerListGameobject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
            {
                playerListGameobject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
            }
            playerListGameObjects.Add(newPlayer.ActorNumber, playerListGameobject);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
            ActivatePanel(InsideRoom_UI_Panel.name);
            roomInfoText.text = " Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
                    " Players / Max.player: " +
                    PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                    PhotonNetwork.CurrentRoom.MaxPlayers;

            Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
            playerListGameObjects.Remove(otherPlayer.ActorNumber);

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                startGameButton.SetActive(true);
            }
            else
            {

            }
        }

        public override void OnLeftRoom()
        {
            ActivatePanel(GameOptions_UI_Panel.name);

            foreach (GameObject playerListGameobject in playerListGameObjects.Values)
            {
                Destroy(playerListGameobject);
            }
            playerListGameObjects.Clear();
            playerListGameObjects = null;
        }
        public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created ");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
        ActivatePanel(InsideRoom_UI_Panel.name);

            if (PhotonNetwork.LocalPlayer.IsMasterClient)
            {
                startGameButton.SetActive(true);
            }
            else
            {
                startGameButton.SetActive(false);
            }

            roomInfoText.text = " Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
                    " Players / Max.player: " +
                    PhotonNetwork.CurrentRoom.PlayerCount + " / " +
                    PhotonNetwork.CurrentRoom.MaxPlayers;
            if (playerListGameObjects == null)
            {
                playerListGameObjects = new Dictionary<int, GameObject>();
            }
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject playerListGameobject = Instantiate(playerListPrefabl);
                playerListGameobject.transform.SetParent(playerListContent.transform);
                playerListGameobject.transform.localScale = Vector3.one;

                playerListGameobject.transform.Find("PlayerNameText").GetComponent<Text>().text = player.NickName;

                if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    playerListGameobject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
                }
                else
                {
                    playerListGameobject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
                }
                playerListGameObjects.Add(player.ActorNumber, playerListGameobject);
            }

    }
    public override void OnConnected()
    {
        Debug.Log("Connected to Internet ");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to Photon");
        ActivatePanel(GameOptions_UI_Panel.name);

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher : OnDisconnected() was called {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
        {
            //base.OnJoinRandomFailed(returnCode, message);
            Debug.Log(message);
            string roomName = "Room " + Random.Range(1000,10000);

            RoomOptions roomOPtions = new RoomOptions();
            roomOPtions.MaxPlayers = 20;
            PhotonNetwork.CreateRoom(roomName, roomOPtions);

        }


     public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListiew();
        foreach (var roomListGameobject in roomListGameObjects.Values)
        {
            Destroy(roomListGameobject);
        }
        roomListGameObjects.Clear();

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);

            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (cahcedRoomList.ContainsKey(room.Name))
                {
                    cahcedRoomList.Remove(room.Name);
                }
            }
            else
            {
                if (cahcedRoomList.ContainsKey(room.Name))
                {
                    cahcedRoomList[room.Name] = room;
                }
                else
                {
                    cahcedRoomList.Add(room.Name, room);
                }
            }

        }
        foreach (RoomInfo room in cahcedRoomList.Values)
        {
            GameObject roomListEntryGameobject = Instantiate(roomListEntryPrefab);
            roomListEntryGameobject.transform.SetParent(roomListParentGameObject.transform);
            roomListEntryGameobject.transform.localScale = Vector3.one;

            roomListEntryGameobject.transform.Find("RoomNameText").GetComponent<Text>().text = room.Name;
            roomListEntryGameobject.transform.Find("RoomPlayersText").GetComponent<Text>().text = room.PlayerCount + " / " + room.MaxPlayers;
            roomListEntryGameobject.transform.Find("JoinRoomButton").GetComponent<Button>().onClick.AddListener(() => OnJoinRooomButtonClicked(room.Name));

            roomListGameObjects.Add(room.Name, roomListEntryGameobject);
        }

    }

     public override void OnLeftLobby()
        {
            ClearRoomListiew();
            cahcedRoomList.Clear();
        }
        #endregion

     #region Unity callback
        public void OnCancelButtonCLicked()
    {
        ActivatePanel(GameOptions_UI_Panel.name);
    }
   public void OnLeaveButtonClicked()
      {
            PhotonNetwork.LeaveRoom();
      }
    public void OnShowRoomButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }

        ActivatePanel(RoomList_UI_Panel.name);
    }
    public void OnLoginButtonCLicked()
    {
        string playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
                 PhotonNetwork.ConnectUsingSettings();
                // loadBalancingClient.ConnectToRegionMaster("us");
                PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "in";
                PhotonNetwork.PhotonServerSettings.AppSettings.UseNameServer = true;
            }
        else
        {
            Debug.Log("Playername is invalid ");
        }
    }

    public void OnRoomCreateButtonClicked()
    {
        string roomName = roomNameInputField.text;

        if (string.IsNullOrEmpty(roomName))
        {
            roomName = " Room " + Random.Range(1000, 10000);
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)int.Parse(maxPlayerInputField.text);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

        public void OnClickScene1()
        {
            SceneSelection = 1;
            OnRoomCreateButtonClicked();
        }
        public void OnClickScene2()
        {
            SceneSelection = 2;
            OnRoomCreateButtonClicked();
        }

        public void OnClickScene3()
        {
            SceneSelection = 3;
            OnRoomCreateButtonClicked();
        }


        public void OnBackButtonCLicked()
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }
            ActivatePanel(GameOptions_UI_Panel.name);
        }

    public void OnJoinRandomRoomButtonClicked()
        {
            ActivatePanel(JoinRandomRoom_UI_Panel.name);
            PhotonNetwork.JoinRandomRoom();
        }

   
    #endregion

    #region Public MEthods

    public void OnStartGameButtonClicked()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                switch (SceneSelection)
                {
                    case 1:
                        PhotonNetwork.LoadLevel("MultipSce1");
                        break;
                    case 2:
                        PhotonNetwork.LoadLevel("Scene2");
                        break;
                    case 3:
                        PhotonNetwork.LoadLevel("Scene3");
                        break;


                }

            }
        }
    void OnJoinRooomButtonClicked(string _roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(_roomName);
    }
    public void ActivatePanel(string panelToBeActivated)
    {
        Login_UI_Panel.SetActive(panelToBeActivated.Equals(Login_UI_Panel.name));
        GameOptions_UI_Panel.SetActive(panelToBeActivated.Equals(GameOptions_UI_Panel.name));
        CreateRoom_UI_Panel.SetActive(panelToBeActivated.Equals(CreateRoom_UI_Panel.name));
        InsideRoom_UI_Panel.SetActive(panelToBeActivated.Equals(InsideRoom_UI_Panel.name));
        RoomList_UI_Panel.SetActive(panelToBeActivated.Equals(RoomList_UI_Panel.name));
        JoinRandomRoom_UI_Panel.SetActive(panelToBeActivated.Equals(JoinRandomRoom_UI_Panel.name));

    }
    void ClearRoomListiew()
    {
        foreach (var roomListGameobject in roomListGameObjects.Values)
        {
            Destroy(roomListGameobject);
        }
        roomListGameObjects.Clear();
    }

    #endregion
}
}