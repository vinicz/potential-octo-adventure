using UnityEngine;
using System.Collections;

public class LevelButtonTool : MonoBehaviour {

    public UILabel buttonLabel;
    public GameObject loadingWindow;
    public int levelIndex;

	public GameObject createButton(GameObject parent,string label, int level)
    {
        buttonLabel.text = label;
        levelIndex = level;


        return NGUITools.AddChild(parent, this.gameObject);

    }

    public void OnClick()
    {
        this.transform.parent.gameObject.SetActive(false);
        NGUITools.AddChild(this.transform.parent.parent.gameObject, loadingWindow);
        loadingWindow.SetActive(true);
        Application.LoadLevel(levelIndex);
    }


}
