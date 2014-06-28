using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerPlayerList : MonoBehaviour {


    public UILabel redTeam;
    public UILabel blueTeam;
    private MultiGameMaster gameMaster;

    void Start()
    {
        gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster;

        setUpPlayerList();
        
        gameMaster.PlayerListChanged += setUpPlayerList;
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

    void OnDestroy()
    {
        gameMaster.PlayerListChanged -= setUpPlayerList;
    }
}
