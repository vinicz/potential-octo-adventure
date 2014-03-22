using UnityEngine;
using System.Collections;

public class WindowOpenerScript : MonoBehaviour {

    public GameObject nextWindow;

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        nextWindow.SetActive(false);
	}
	
	public void OnClick()
    {
        nextWindow.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }
}
