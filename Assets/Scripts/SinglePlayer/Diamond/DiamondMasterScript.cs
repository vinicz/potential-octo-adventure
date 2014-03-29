using UnityEngine;
using System.Collections;

public class DiamondMasterScript : GameHandlerScript
{

    public override void createMapSpecificGUI()
    {
        GUI.Box(new Rect(200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + requiredDiamondCount);
        if (gameState == GameState.POSTGAME)
        {
            if (collectedDiamondCount == requiredDiamondCount)
            {
                createWinMenu();
                
            } else
            {
                createLoseMenu();
            }
            
        }
    }
    
  
}
