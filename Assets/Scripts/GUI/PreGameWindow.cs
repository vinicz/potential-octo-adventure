using UnityEngine;
using System.Collections;

public class PreGameWindow : UIWindow {

    private bool inited = false;

   
	void Update () {

        if (!inited)
        {
            initWindow();
        }
	
        if (Input.anyKeyDown)
        {
            GameServiceLayer.serviceLayer.gameMaster.setGameState(GameHandlerScript.GameState.GAME);
            this.gameObject.SetActive(false);
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


    void onGameStateChanged()
    {
        if(GameServiceLayer.serviceLayer.gameMaster.getGameState()==GameHandlerScript.GameState.PREGAME)
        {
            this.gameObject.SetActive(true);
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged-=onGameStateChanged;
        }

    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.GameStateChanged -= onGameStateChanged;
    }

}
