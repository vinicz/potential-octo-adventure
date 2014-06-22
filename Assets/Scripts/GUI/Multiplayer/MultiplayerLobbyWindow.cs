using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerLobbyWindow : UIWindow
{

    public UILabel redTeam;
    public UILabel blueTeam;
    public UIButton startButton;
    public UIButton redTeamButton;
    public UIButton blueTeamButtonButton;
    public UIButton randomTeamButton;
    private MultiGameMaster gameMaster;
    private bool inited;
    private bool teamSelected;

    void Start()
    {
        gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster;

        if (gameMaster.isPlayerServer())
        {
            startButton.gameObject.SetActive(true);
            startButton.isEnabled = false;
            setTeamSelectorsEnabled(false);

        } else
        {
            startButton.gameObject.SetActive(false);
        }

        setUpPlayerList();
                
        gameMaster.PlayerListChanged += setUpPlayerList;
        gameMaster.GameStateChanged += onGameStateChanged;
        gameMaster.PlayerConnectedToServer += onPlayerConnectedToServer;
       
    }

    public override void initWindow()
    {
    }

    void onStartButtonPressed()
    {
        gameMaster.startGame();
    }

    void onRedTeamButtonPressed()
    {
        gameMaster.playerJoinedToTeam(1);
        handleTeamSelection();
    }

    void onBlueTeamButtonPressed()
    {
        gameMaster.playerJoinedToTeam(2);
        handleTeamSelection();
    }

    void onRandomTeamButtonPressed()
    {
        gameMaster.playerJoinedRandom();
        handleTeamSelection();
    }

    void onGameStateChanged()
    {
        hideLobbyIfGameStarted();
    }

    void setUpPlayerList()
    {
        redTeam.text = "";
        blueTeam.text = "";
        List<MultiGameMaster.MultiPlayerStruct> playerList = gameMaster.getPlayerList();
        foreach (MultiGameMaster.MultiPlayerStruct player in playerList)
        {
            if (player.team == 1)
            {
                redTeam.text += "\n" + player.name;
            } else
            {
                blueTeam.text += "\n" + player.name;
            }
        }

    }

    void onPlayerConnectedToServer(string playerName)
    {
        setTeamSelectorsEnabled(true);
    }

    void setTeamSelectorsEnabled(bool enabled)
    {
        redTeamButton.isEnabled = enabled;
        blueTeamButtonButton.isEnabled = enabled;
        randomTeamButton.isEnabled = enabled;

    }

    void handleTeamSelection()
    {
        setTeamSelectorsEnabled(false);
        startButton.isEnabled = true;
        teamSelected = true;
    }

    void hideLobbyIfGameStarted()
    {
        if (gameMaster.getGameState() == GameHandlerScript.GameState.GAME && teamSelected)
        {
            this.gameObject.SetActive(false);
        }
    }
    
    void OnDestroy()
    {
        gameMaster.GameStateChanged -= onGameStateChanged;
        gameMaster.PlayerListChanged -= setUpPlayerList;
    }
}
