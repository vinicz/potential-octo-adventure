using UnityEngine;
using System.Collections;

public class InGameGUI : MonoBehaviour {

    private bool inited = false;

    void Update()
    {
	    if (!inited)
        {
            GameServiceLayer.serviceLayer.gameMaster.GameStateChanged += onGameStateChanged;
            onGameStateChanged();
            inited = true;
        }
	}
	
	void onGameStateChanged()
    {
        GameHandlerScript.GameState currentGameState = GameServiceLayer.serviceLayer.gameMaster.getGameState();

        if (currentGameState == GameHandlerScript.GameState.PREGAME)
        {
            foreach (Transform childTransform in this.transform) {
                UIWindow window = childTransform.gameObject.GetComponent<UIWindow>();
                window.initWindow();
            }
        }
    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.GameStateChanged -= onGameStateChanged;
    }
}
