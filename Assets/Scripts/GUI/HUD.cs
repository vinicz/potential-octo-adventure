using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

    private bool inited = false;

	// Use this for initialization
	void Update() {
	    
        if (!inited)
        {
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged += onGameStateChanged;
            onGameStateChanged();
            inited = true;
        }
	}
	
    public void onGameStateChanged()
    {
        GameHandlerScript.GameState gameState =  GameServiceLayer.serviceLayer.gameMaster.getGameState();
        if (gameState == GameHandlerScript.GameState.GAME)
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}
