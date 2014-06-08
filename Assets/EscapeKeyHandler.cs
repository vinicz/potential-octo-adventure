using UnityEngine;
using System.Collections;

public class EscapeKeyHandler : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel (GameServiceLayer.serviceLayer.levelService.getMainMenuIndex()); 
	}
}
