using UnityEngine;
using System.Collections;

public class MultiplayerHUD : HUD {

    protected override bool isHUDEnabled()
    {
        return GameServiceLayer.serviceLayer.gameMaster.getGameState() == GameHandlerScript.GameState.GAME && playerSpawned;
    }
}
