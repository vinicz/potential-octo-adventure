using UnityEngine;
using System.Collections;

public class RestartLevelButton : MonoBehaviour {

    public void OnClick()
    {
        GameServiceLayer.serviceLayer.gameMaster.restartLevel();
    }
}
