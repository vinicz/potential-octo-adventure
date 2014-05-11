using UnityEngine;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

    public void OnClick()
    {
        GameServiceLayer.serviceLayer.gameMaster.loadMainMenu();
    }
}
