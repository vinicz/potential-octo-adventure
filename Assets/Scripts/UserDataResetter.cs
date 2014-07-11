using UnityEngine;
using System.Collections;

public class UserDataResetter : MonoBehaviour {

	void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 50, 25), "Reset"))
        {
            PlayerPrefs.DeleteAll();
        }

    }
}
