using UnityEngine;
using System.Collections;

public class PreGameWindow : MonoBehaviour {

    private bool inited = false;

   
	void Update () {

        if (!inited)
        {
            if(GameServiceLayer.serviceLayer.gameMaster.getGameState()!=GameHandlerScript.GameState.PREGAME)
            {
                GameServiceLayer.serviceLayer.gameMaster.GameStateChanged+=onGameStateChanged;
                this.gameObject.SetActive(false);
                inited = true;
            }
        }
	
        if (Input.anyKeyDown)
        {
            GameServiceLayer.serviceLayer.gameMaster.setGameState(GameHandlerScript.GameState.GAME);
            this.gameObject.SetActive(false);
        }
	}


    void onGameStateChanged()
    {
        if(GameServiceLayer.serviceLayer.gameMaster.getGameState()==GameHandlerScript.GameState.PREGAME)
        {
            this.gameObject.SetActive(true);
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged-=onGameStateChanged;
        }

    }
}
