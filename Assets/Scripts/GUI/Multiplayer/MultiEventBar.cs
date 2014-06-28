using UnityEngine;
using System.Collections;

public class MultiEventBar : UIWindow {

    public UILabel messageLabel;
    public UISprite messageBackground;
    public float notificationLength = 2f;
    public float notificationTimeToLive = 0f;

    private MultiGameMaster gameMaster;


    void Update()
    {
        if (notificationTimeToLive > 0)
        {
            notificationTimeToLive -= Time.deltaTime;
        } else
        {
            this.gameObject.SetActive(false);
        }
    }


    public override void initWindow()
    {
        gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster;
        gameMaster.PlayerConnectedToServer += onPlayerConnected;
        gameMaster.PlayerDisconnectedFromServer += onPlayerDisconnected;
        gameMaster.LostConnectionToServer += onLostConnectionToServer;
    }

    void onPlayerConnected(string name)
    {  
        showNotification(name+ " joined the game");
    }

    void onPlayerDisconnected(string name)
    {
        showNotification(name+ " disconnected from game");
    }

    void onLostConnectionToServer()
    {
        showNotification("Disconnected from the server!");
    }


    void showNotification(string notificationMessage)
    {
        messageLabel.text =  notificationMessage;
        notificationTimeToLive = notificationLength;
        this.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        gameMaster.PlayerConnectedToServer -= onPlayerConnected;
        gameMaster.PlayerDisconnectedFromServer -= onPlayerDisconnected;
        gameMaster.LostConnectionToServer -= onLostConnectionToServer;
    }
}
