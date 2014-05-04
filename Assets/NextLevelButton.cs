using UnityEngine;
using System.Collections;

public class NextLevelButton : MonoBehaviour {

    public void OnClick()
    {
        GameServiceLayer.serviceLayer.gameMaster.loadNextLevel();
    }
}
