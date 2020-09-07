using System.IO;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{

    //Room Info
    public static PhotonRoom photonRoom;
    private PhotonView PV;

    public bool isGameLoaded;
    public int currentScene;

    //Player Info
    private Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playerInGame;

    //Delayed Start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayer;
    private float timeToStart;

    public TextMeshProUGUI playerName;
    public TextMeshProUGUI player1;
    public TextMeshProUGUI player2;



    private void Awake()
    {
        //setup singleton
        if(PhotonRoom.photonRoom == null)
        {
            PhotonRoom.photonRoom = this;
        }
        else
        {
            if(PhotonRoom.photonRoom != this)
            {
                Destroy(PhotonRoom.photonRoom.gameObject);
                PhotonRoom.photonRoom = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        Debug.Log("OnEnable");
        //subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading; 
    }

    public override void OnDisable()
    {
        Debug.Log("OnDisable");
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneFinishedLoading");
        //called when multiplayer scene is loaded
        currentScene = scene.buildIndex;
        Debug.Log("Current Scene: " + currentScene);
        Debug.Log("MultiplayerScene: " + MultiplayerSettings.multiplayerSettings.multiplayerScene);
        if(currentScene == MultiplayerSettings.multiplayerSettings.multiplayerScene)
        {
            Debug.Log("Third");
            isGameLoaded = true;
            //for delay starty
            if (MultiplayerSettings.multiplayerSettings.delayStart)
            {
                PV.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
            }
            //for no delay start
            else
            {
                RPC_CreatePlayer();
            }
        }
        
    }
    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        Debug.Log("LoadGameScene");
        playerInGame++;
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            PV.RPC("RPC_CreatePlayer", RpcTarget.All);
        }
    }
    [PunRPC]
    private void RPC_CreatePlayer()
    {
        Debug.Log("Create Player");
        PhotonNetwork.Instantiate("PhotonNetworkPlayer",new Vector3(0,0,0),Quaternion.identity,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //set private variables
        PV = GetComponent<PhotonView>();
        readyToCount = false;
        readyToStart = false;
        lessThanMaxPlayers = startingTime;
        atMaxPlayer = 6;
        timeToStart = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        // for delay start only, count down to start
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            if (playersInRoom == 1)
            {
                RestartTimer();
            }
            if (!isGameLoaded)
            {
                if (readyToStart)
                {
                    atMaxPlayer -= Time.deltaTime;
                    lessThanMaxPlayers = atMaxPlayer;
                    timeToStart = atMaxPlayer;
                }
                else if (readyToCount)
                {
                    lessThanMaxPlayers -= Time.deltaTime;
                    timeToStart = lessThanMaxPlayers;
                }
                Debug.Log("Display Time to start to the Player: " + timeToStart);
                if(timeToStart <= 0)
                {
                    StartGame();
                }
            }
        }
    }

    private void RestartTimer()
    {
        lessThanMaxPlayers = startingTime;
        timeToStart = startingTime;
        atMaxPlayer = 6;
        readyToCount = false;
        readyToStart = false;
        Debug.Log("Reset");
    }

    private void StartGame()
    {
        Debug.Log("StartGame");
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
        PhotonNetwork.LoadLevel(MultiplayerSettings.multiplayerSettings.multiplayerScene);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnjoinedRoom");
        //set player data when we join the room
        base.OnJoinedRoom();
        Debug.Log("We Are in a Room! Yeah");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = playerName.text;
        Debug.Log(PhotonNetwork.NickName);
        if (playersInRoom == 1)
        {
            player1.text = PhotonNetwork.NickName;
        }
        else if(playersInRoom == 2)
        {
            player2.text = PhotonNetwork.NickName;
        }


        //for delay start only
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Display players in room out of max players possible (" + playersInRoom +" : " + MultiplayerSettings.multiplayerSettings.maxPlayers+ " )");
            if(playersInRoom > 1)
            {
                readyToCount = true;
            }    
            if(playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
        //for non delay start
        else
        {
            StartGame();
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnplayerEntered");
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new Player has joined the room.");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
        if (MultiplayerSettings.multiplayerSettings.delayStart)
        {
            Debug.Log("Display players in room out of max players possible (" + playersInRoom + " : " + MultiplayerSettings.multiplayerSettings.maxPlayers + " )");
            if (playersInRoom > 1)
            {
                readyToCount = true;
            }
            if (playersInRoom == MultiplayerSettings.multiplayerSettings.maxPlayers)
            {
                readyToStart = true;
                if (!PhotonNetwork.IsMasterClient)
                    return;
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }
}
