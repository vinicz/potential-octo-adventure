using UnityEngine;
using System.Collections;

public class LevelItem : MonoBehaviour {

    public UILabel levelName;
    public UILabel levelInfo;
    public LevelButtonTool levelButton;


    public GameObject createLevelItem(GameObject parent,LevelRecord level, GameObject currentWindow)
    {
        levelName.text = level.levelName;
        levelInfo.text = "Best time: "+level.bestTime.ToString("0.00");
        levelInfo.text += "\nDiamonds: "+level.collectedDiamonds+"/"+level.allDiamonds;

        createLevelButton(level, currentWindow);

        GameObject newLevelItem = createLevelViewItem(parent);

        return newLevelItem;
    }

    void createLevelButton(LevelRecord level, GameObject currentWindow)
    {
        levelButton.levelIndex = level.getLevelIndex();
        levelButton.currentWindow = currentWindow;
    }

    GameObject createLevelViewItem(GameObject parent)
    {
        GameObject newLevelItem = NGUITools.AddChild(parent, this.gameObject);
        newLevelItem.transform.localPosition = this.transform.position;
        newLevelItem.transform.localRotation = this.transform.rotation;
        newLevelItem.transform.localScale = this.transform.localScale;
        return newLevelItem;
    }
}
