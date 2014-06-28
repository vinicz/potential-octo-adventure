using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class MultiGameMaster : GameHandlerScript
{

    public delegate void PlayerListChangedHandler();
    public event PlayerListChangedHandler PlayerListChanged;

    public delegate void PlayerConnectedToServerHandler(string playerName);
    public event PlayerConnectedToServerHandler PlayerConnectedToServer;

    public delegate void PlayerDisconnectedFromServerHandler(string playerName);
    public event PlayerDisconnectedFromServerHandler PlayerDisconnectedFromServer;

    public delegate void LostConnectionToServerHandler();
    public event LostConnectionToServerHandler LostConnectionToServer;

    public class MultiPlayerStruct
    {
        public string name;
        public int team;
        public NetworkPlayer networkPlayer;

        public MultiPlayerStruct(string name, int team, NetworkPlayer guid)
        {
            this.name = name;
            this.team = team;
            this.networkPlayer = guid;
        }
    };

    private NetworkPlayer currentPlayer;
    private string currentPlayerName;
    private List<MultiPlayerStruct> playerList = new List<MultiPlayerStruct>();

    public void serverInitialized(string playerName)
    {
        currentPlayerName = playerName;

    }

    public void playerConnectedToServer(NetworkPlayer player, string name)
    {
        currentPlayer = player;
        currentPlayerName = name;

        networkView.RPC("onPlayerConnectedToServer", RPCMode.All, currentPlayerName);  
    }

    public void playerJoinedRandom()
    {
        networkView.RPC("addPlayerRandomOnServer", RPCMode.Server, currentPlayerName, currentPlayer);   
    }

    public void playerJoinedToTeam(int team)
    {
        networkView.RPC("addPlayerWithTeamOnServer", RPCMode.Server, currentPlayerName, team, currentPlayer);
    }

    public void startGame()
    {
        if (isPlayerServer())
        {
            networkView.RPC("setRemoteGameState", RPCMode.AllBuffered, GameState.GAME.ToString());
            networkView.RPC("spawnRemotePlayers", RPCMode.All);
        }
    }

    public void playerDisconnected(NetworkPlayer player)
    {
        networkView.RPC("removePlayer", RPCMode.AllBuffered, player);
    }

    public void lostConnectionToServer()
    {
        base.setGameState(GameState.POSTGAME);

        if (LostConnectionToServer != null)
        {
            LostConnectionToServer();
        }
    }

    [RPC]
    void onPlayerConnectedToServer(string name)
    {
        if (PlayerConnectedToServer != null)
        {
            PlayerConnectedToServer(name);
        }

    }

    [RPC]
    void addPlayerRandomOnServer(string name, NetworkPlayer networkPlayer)
    {
        int team = determineRandomTeam();

        addPlayerOnserver(name, team, networkPlayer);
    }

    [RPC]
    void addPlayerWithTeamOnServer(string name, int team, NetworkPlayer networkPlayer)
    {
        addPlayerOnserver(name, team, networkPlayer);
    }

    void addPlayerOnserver(string name, int team, NetworkPlayer networkPlayer)
    {
        playerList.Add(new MultiPlayerStruct(name, team, networkPlayer));

        if (PlayerListChanged != null)
        {
            PlayerListChanged();
        }

        networkView.RPC("addPlayerOnClient", RPCMode.OthersBuffered, name, team, networkPlayer);
    }

    [RPC]
    void addPlayerOnClient(string name, int team, NetworkPlayer networkPlayer)
    {
        playerList.Add(new MultiPlayerStruct(name, team, networkPlayer));

        if (PlayerListChanged != null)
        {
            PlayerListChanged();
        }

        if (gameState == GameState.GAME && networkPlayer.guid.Equals(currentPlayer.guid))
        {
            spawnPlayer();
        }
    }

    [RPC]
    void removePlayer(NetworkPlayer networkPlayer)
    {
        foreach (MultiPlayerStruct player in playerList)
        {
            if(player.networkPlayer.guid.Equals(networkPlayer.guid))
            {
                playerList.Remove(player);

                if (PlayerDisconnectedFromServer != null)
                {
                    PlayerDisconnectedFromServer(player.name);
                }

                break;
            }
        }
    }

    public override void setGameState(GameState state)
    {
        if (isPlayerServer())
        {
            networkView.RPC("setRemoteGameState", RPCMode.AllBuffered, state.ToString());
        }
    }

    [RPC]
    void setRemoteGameState(string stringState)
    {
        GameState state = (GameState)Enum.Parse(typeof(GameState), stringState);

        base.setGameState(state);
    }
        
    [RPC]
    void spawnRemotePlayers()
    {
        base.spawnPlayer();
    }

    protected override void handleEscapeKey()
    {
        Network.Disconnect();
        base.handleEscapeKey();
    }
    
    protected abstract int determineRandomTeam();

    public abstract List<int> getPossibleTeams();

    public List<MultiPlayerStruct> getPlayerList()
    {
        return playerList;
    }

    public bool isPlayerServer()
    {
        return Network.isServer;
    }

    public MultiPlayerStruct getPlayerInfo(string guid)
    {
        MultiPlayerStruct playerInfo = null;

        foreach (MultiPlayerStruct player in playerList)
        {
            if(player.networkPlayer.guid.Equals(guid))
            {
                playerInfo = player;
                break;
            }
        }

        return playerInfo;
    }

    public MultiPlayerStruct getCurrentPlayerInfo()
    {
        return getPlayerInfo(currentPlayer.guid);
    }


}
