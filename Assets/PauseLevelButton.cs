using UnityEngine;
using System.Collections;

public class PauseLevelButton : MonoBehaviour {

    public void OnClick()
    {
        GameServiceLayer.serviceLayer.gameMaster.pauseLevel();
    }
}
