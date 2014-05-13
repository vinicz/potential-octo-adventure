using UnityEngine;
using System.Collections;

public class PauseWindow : UIWindow {

    bool inited = false;
    
    
    void Update () 
    {
        if (!inited)
        {
            initWindow();  
        }
    }
    
    public override void initWindow()
    {
        GameServiceLayer.serviceLayer.gameMaster.GamePaused  += onGamePaused;
        GameServiceLayer.serviceLayer.gameMaster.GameResumed  += onGameResumed;
        this.gameObject.SetActive(false);
        inited = true;
    }
    
    void onGamePaused()
    {    
        this.gameObject.SetActive(true);
    }

    void onGameResumed()
    {    
        this.gameObject.SetActive(false);
    }
}
