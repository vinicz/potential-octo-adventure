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
    }

    void onPlayerConnected(string name)
    {
        messageLabel.text = name + " joined the game";
        notificationTimeToLive = notificationLength;
        this.gameObject.SetActive(true);
    }


}
