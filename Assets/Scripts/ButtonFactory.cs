using UnityEngine;
using System.Collections;

public class ButtonFactory : MonoBehaviour {

    public UILabel buttonLabel;
    public int levelIndex;

	public GameObject createButton(GameObject parent,string label, int level)
    {
        buttonLabel.text = label;
        levelIndex = level;


        return NGUITools.AddChild(parent, this.gameObject);

    }

    public void OnClick()
    {
        Application.LoadLevel(levelIndex);
    }


}
