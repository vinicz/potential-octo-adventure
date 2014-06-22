using UnityEngine;
using System.Collections;

public class MultiplayerNameHandler : MonoBehaviour
{

    public UILabel nameLabel;
    private MultiGameMaster gameMaster;

    void Start()
    {

        gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster;
        MultiGameMaster.MultiPlayerStruct player = gameMaster.getCurrentPlayerInfo();

        if (networkView.isMine)
        {
            networkView.RPC("setRemotePlayerName", RPCMode.OthersBuffered, player.name, player.team);
            setCurrentNameLabelText(player.name, player.team);
        }
    }

    [RPC]
    void setRemotePlayerName(string name, int team)
    {
        setPlayerNameLabel(name, team);
    }

    void setCurrentNameLabelText(string name, int team)
    {      
        name = ">" + name + "<";
        setPlayerNameLabel(name, team);
    }

    void setPlayerNameLabel(string name, int team)
    {
        switch (team)
        {
            case 1:
                nameLabel.color = Color.red;
                break;
            case 2:
                nameLabel.color = Color.blue;
                break;
        }
        nameLabel.text = name;
    }
}
