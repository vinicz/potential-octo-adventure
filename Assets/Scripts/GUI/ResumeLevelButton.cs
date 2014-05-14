using UnityEngine;
using System.Collections;

public class ResumeLevelButton : MonoBehaviour {

    public void OnClick()
    {
        GameServiceLayer.serviceLayer.gameMaster.resumeLevel();

    }
}
