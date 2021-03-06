﻿using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
    const string MASTER_SERVER_ADDRESS = "54.187.114.127";

    public delegate void PlayerConnectedHandler();

    public event PlayerConnectedHandler PlayerConnected;
    
    public static string gameNamePrefix = appName + ".";
    public static string appName = "com.muddictive.ramball";
    private string gameName;
    private string playerName;
    private string gamePassword;
    private bool refreshing;
    private MultiGameMaster gameMaster;
  

    // Use this for initialization
    void Start()
    {

        refreshing = false;
        GameServiceLayer.serviceLayer.networkManager = this;

        gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster; 

    }
    
    public void startServer(string gameName, string gamePassword, string playerName)
    {
        this.gameName = gameNamePrefix + gameName;
        this.playerName = playerName;

        Network.natFacilitatorIP = MASTER_SERVER_ADDRESS;
        Network.incomingPassword = gamePassword;
        Network.InitializeServer(32, 26001, !Network.HavePublicAddress());
                
        Debug.Log("Initializing server");
    }

    public void joinServer(string gameName, string gamePassword, string playerName)
    {
        this.gameName = gameNamePrefix + gameName;
        this.gamePassword = gamePassword;
        this.playerName = playerName;

        Network.natFacilitatorIP = MASTER_SERVER_ADDRESS;
        MasterServer.ipAddress = MASTER_SERVER_ADDRESS;
        MasterServer.port = 23466;
        MasterServer.RequestHostList(appName);
        refreshing = true;
    }

    void OnServerInitialized()
    {

        MasterServer.ipAddress = MASTER_SERVER_ADDRESS;
        MasterServer.RegisterHost(appName, gameName, "");
        Debug.Log("Server initialized");

    }

    void OnFailedToConnect(NetworkConnectionError error)
    {
        Debug.Log("Could not connect to server: " + error);
    }
    
    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Debug.Log("Could not connect to master server: " + info + " " + MasterServer.ipAddress);
                
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Server registered");
        
            if (PlayerConnected != null)
            {
                PlayerConnected();
            }

            gameMaster.serverInitialized(playerName);

                        
        } else
            Debug.Log(msEvent);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("Player connected from " + player.ipAddress + ":" + player.port);
    }

    void OnPlayerDisconnected(NetworkPlayer player) {

        gameMaster.playerDisconnected(player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);

    }
     

    // Update is called once per frame
    void Update()
    {
        if (refreshing)
        {
            if (MasterServer.PollHostList().Length > 0)
            {

                foreach (HostData host in MasterServer.PollHostList())
                {


                    if (host.gameName == gameName)
                    {
                        refreshing = false;

                        Network.Connect(host, gamePassword); 

                        break;
                    }
                                
                }
                                 
            }
        }
    }
   
    void OnConnectedToServer()
    {

        if (PlayerConnected != null)
        {
            PlayerConnected();
        }
        
        gameMaster.playerConnectedToServer(Network.player, playerName);
        
    }

    void OnDisconnectedFromServer(NetworkDisconnection info) {
        gameMaster.lostConnectionToServer();
    }
    
    
    
    
}
