using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

	public delegate void PlayerConnectedHandler();
	public event PlayerConnectedHandler PlayerConnected;
	
	public static string gameNamePrefix = appName+".";
	public static string appName = "com.muddictive.ramball";

	private string gameName;
	private string gamePassword;
    private bool gameStarted;
    private bool refreshing;
    private int playerCount;
    public GameObject playerObject;

    // Use this for initialization
    void Start()
    {

        gameStarted = false;
        refreshing = false;
            
        playerCount = 0;


    }

    
    public void startServer(string gameName, string gamePassword)
    {
		this.gameName = gameNamePrefix+gameName;

		Network.incomingPassword = gamePassword;
        Network.InitializeServer(32, 26001, !Network.HavePublicAddress());
                
        Debug.Log("Initializing server");
    }
	

	public void joinServer(string gameName, string gamePassword)
	{
		this.gameName = gameNamePrefix+gameName;
		this.gamePassword = gamePassword;

		MasterServer.RequestHostList(appName);
		refreshing = true;
	}



    void OnServerInitialized()
    {
		MasterServer.RegisterHost(appName, gameName, "");
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

			if(PlayerConnected!=null)
			{
				PlayerConnected();
			}

                        
        } else
            Debug.Log(msEvent);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {

				foreach (HostData host in MasterServer.PollHostList()) {


					if(host.gameName==gameName)
					{

						refreshing = false;
						gameStarted = true;

						if(PlayerConnected!=null)
						{
							PlayerConnected();
						}

					
						Network.Connect(host,gamePassword); 

						break;
					}
								
				}
						         
            }
        }
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
