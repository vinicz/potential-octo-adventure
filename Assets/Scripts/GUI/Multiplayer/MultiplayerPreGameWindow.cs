using UnityEngine;
using System.Collections;

public class MultiplayerPreGameWindow : UIWindow
{


    public UILabel playerNameLabel;
    public UILabel serverNameLabel;
    public UILabel serverPasswordLabel;
    public GameObject lobbyWindow;
    private bool inited;

    void Update()
    {
        if (!inited)
        {
            initWindow();
        }
    }
    
    public override void initWindow()
    {
        if (GameServiceLayer.serviceLayer.gameMaster.getGameState() != GameHandlerScript.GameState.PREGAME)
        {
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged += onGameStateChanged;
            this.gameObject.SetActive(false);    
        } else
        {
            this.gameObject.SetActive(true);   
        }
        
        inited = true;
    }

    void onJoinGamePressed()
    {

        GameServiceLayer.serviceLayer.networkManager.PlayerConnected += onPlayerConnected;
        GameServiceLayer.serviceLayer.networkManager.joinServer(serverNameLabel.text, serverPasswordLabel.text, playerNameLabel.text);

    }

    void onCreateGamePressed()
    {
        GameServiceLayer.serviceLayer.networkManager.PlayerConnected += onPlayerConnected;
        GameServiceLayer.serviceLayer.networkManager.startServer(serverNameLabel.text, serverPasswordLabel.text, playerNameLabel.text);
        
    }
    
    void onGameStateChanged()
    {
        if (GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.PREGAME)
        {
            this.gameObject.SetActive(true);
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged -= onGameStateChanged;
        }
        
    }

    void onPlayerConnected()
    {
        GameServiceLayer.serviceLayer.networkManager.PlayerConnected -= onPlayerConnected;

        this.gameObject.SetActive(false);
        lobbyWindow.SetActive(true);
    }
    
    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.GameStateChanged -= onGameStateChanged;
    }


}
