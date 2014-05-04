using UnityEngine;
using System.Collections;

public class LoseWindow : MonoBehaviour {

    bool inited = false;
    
    
    void Update () 
    {
        if (!inited)
        {
            GameServiceLayer.serviceLayer.gameMaster.LevelFailed  += onLevelFailed;
            this.gameObject.SetActive(false);
            inited = true;
            
        }
    }
    
    void onLevelFailed()
    {    
        this.gameObject.SetActive(true);
    }
}
