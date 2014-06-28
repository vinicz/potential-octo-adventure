using UnityEngine;
using System.Collections;

public class HUD : UIWindow
{

    private bool inited = false;
    protected bool playerSpawned = false;

    // Use this for initialization
    void Update()
    {
        
        if (!inited)
        {
            initWindow();
        }
    }

    public override void initWindow()
    {
        GameServiceLayer.serviceLayer.gameMaster.GameStateChanged += onGameStateChanged;
        GameServiceLayer.serviceLayer.gameMaster.PlayerSpawned += onPlayerSpawned;
        onGameStateChanged();
        inited = true;
    }
    
    public void onGameStateChanged()
    {
        if (isHUDEnabled())
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    protected virtual bool isHUDEnabled()
    {
        return GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME;
    }

    void onPlayerSpawned()
    {
        playerSpawned = true;
        onGameStateChanged();
    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.GameStateChanged -= onGameStateChanged;
        GameServiceLayer.serviceLayer.gameMaster.PlayerSpawned -= onPlayerSpawned;
    }
}
