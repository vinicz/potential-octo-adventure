using UnityEngine;
using System.Collections;

public class DiamondMasterScript : GameHandlerScript {

    protected void Start()
    {
        initializeGameHandler();
    }
    
    protected void Update()
    {
        updateGameHandler();
        
    }
    
    protected void OnGUI()
    {
        guiHelper.adjustGUIMatrix();
        
        GUI.Box(new Rect(200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + diamondCount);
        if (isGameOver)
        {
            if (collectedDiamondCount == diamondCount)
            {
                createWinMenu();
                
                
            } else
            {
                createLoseMenu();
            }
            
        }
        
        guiHelper.restoreGUIMatrix();
    }
    
  
}
