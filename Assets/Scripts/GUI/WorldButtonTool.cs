using UnityEngine;
using System.Collections;

public class WorldButtonTool : MonoBehaviour {

    public GameObject worldWindow;
    public GameObject currentWindow;
  

    public void OnClick()
    {
        currentWindow.SetActive(false);
        worldWindow.SetActive(true);
    }
}
