using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

    private bool gameStarted;
    private bool refreshing;
    private const string gameName = "com.mudvision.balluniversity.test";
    private int playerCount;
    public GameObject playerObject;

    // Use this for initialization
    void Start()
    {

        gameStarted = false;
        refreshing = false;
            
        playerCount = 0;


    }

    void OnGUI()
    {
        if (!gameStarted)
        {
            if (GUI.Button(new Rect(0, 0, 200, 200), "Start Server"))
            {
                startServer();
            }
            if (GUI.Button(new Rect(200, 0, 200, 200), "Join Server"))
            {
                joinServer();
                            
            }
        } else
        {
            GUI.Label(new Rect(0, 0, 100, 20), playerCount.ToString());
            //if (GUI.Button (new Rect (200, 0, 200, 200), "Spawn Player")) {
            //      spawnPlayer ();
            //}
        }
                      
                      
    }
    
    static void startServer()
    {
        Network.InitializeServer(32, 26001, !Network.HavePublicAddress());
                
        Debug.Log("Initializing server");
    }

    void OnServerInitialized()
    {
        MasterServer.RegisterHost(gameName, "Multiplayer test game", "Multiplayer test game");
        Debug.Log("Server initialized");
        StartCoroutine(spawnPlayer());
            




    }

    void OnConnectedToServer()
    {
        StartCoroutine(spawnPlayer());

    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Server registered");
            gameStarted = true;
                        
        } else
            Debug.Log(msEvent);
    }

    void joinServer()
    {
        MasterServer.RequestHostList(gameName);
        refreshing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshing = false;
                gameStarted = true;
                        
                Network.Connect(MasterServer.PollHostList() [0]);
                                
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0);

               

        
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
    }

    IEnumerator spawnPlayer()
    {
        networkView.RPC("increasePlayerCount", RPCMode.AllBuffered);
        yield return new WaitForSeconds(1);

        GameObject newObject = (GameObject)Network.Instantiate(playerObject, transform.position, Quaternion.identity, 0);
        BallNetworkController ballController = newObject.GetComponent<BallNetworkController>();

        ballController.initializeTeam(playerCount);
                
    }

    [RPC]
    void increasePlayerCount()
    {
        playerCount++;

    }
    
    
    
}
