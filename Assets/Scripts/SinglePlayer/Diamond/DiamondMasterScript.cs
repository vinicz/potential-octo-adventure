using UnityEngine;
using System.Collections;

public class DiamondMasterScript : GameHandlerScript
{

    public override void createMapSpecificGUI()
    {
        GUI.Box(new Rect(200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + diamondCount);
        if (gameState == GameState.POSTGAME)
        {
            if (collectedDiamondCount == diamondCount)
            {
                createWinMenu();
                
            } else
            {
                createLoseMenu();
            }
            
        }
    }
    
  
}
