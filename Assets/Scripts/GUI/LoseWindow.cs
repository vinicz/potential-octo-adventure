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
        this.gameObject.SetActive(false);
        inited = true;
    }
    
    void onLevelFailed()
    {    
        this.gameObject.SetActive(true);
    }
}
