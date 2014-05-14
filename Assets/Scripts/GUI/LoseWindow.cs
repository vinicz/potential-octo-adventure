using UnityEngine;
using System.Collections;

public class LoseWindow : UIWindow {

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
        GameServiceLayer.serviceLayer.gameMaster.LevelFailed  += onLevelFailed;
        GameServiceLayer.serviceLayer.gameMaster.GameResumed += onGameResumed;
        this.gameObject.SetActive(false);
        inited = true;
    }
    
    void onLevelFailed()
    {    
        this.gameObject.SetActive(true);
    }

    void onGameResumed()
    {    
        this.gameObject.SetActive(false);
    }


    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.LevelFailed -= onLevelFailed;
        GameServiceLayer.serviceLayer.gameMaster.GameResumed -= onGameResumed;
    }
}
