using UnityEngine;
using System.Collections;

public class BackTargetSetter : MonoBehaviour {

    public GameObject openedWindowBackButton;
    public GameObject targetWindow;

    private BackKeyHandler backKeyHandler;
    private WindowOpenerScript windowOpener;

    void Start()
    {
        backKeyHandler = openedWindowBackButton.GetComponent<BackKeyHandler>();
        windowOpener = openedWindowBackButton.GetComponent<WindowOpenerScript>();
    }


    public void OnClick()
    {
        backKeyHandler.targetWindow = targetWindow;
        windowOpener.nextWindow = targetWindow;
    }
}
