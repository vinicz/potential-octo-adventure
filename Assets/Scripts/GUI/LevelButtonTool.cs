using UnityEngine;
using System.Collections;

public class LevelButtonTool : MonoBehaviour {

    public UILabel buttonLabel;
    public GameObject loadingWindow;
    public int levelIndex;
    public GameObject currentWindow;
 

    public void OnClick()
    {
        currentWindow.SetActive(false);
        NGUITools.AddChild(currentWindow.transform.parent.gameObject, loadingWindow);
        loadingWindow.SetActive(true);
        Application.LoadLevel(levelIndex);
    }


}
