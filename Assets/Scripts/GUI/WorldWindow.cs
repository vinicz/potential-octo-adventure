using UnityEngine;
using System.Collections;

public class WorldWindow : MonoBehaviour
{

    public UIGrid parentGrid;
    public LevelItem levelItem;
    public string wordName;
    public WindowOpenerScript backButton;
    public UILabel windowTitle;
    public UILabel collectedDiamondsLabel;

    // Use this for initialization
    void Start()
    {
        int collectedDiamonds = 0;
        int allDiamonds = 0;

        foreach (LevelRecord level in GameServiceLayer.serviceLayer.levelService.getLevelsInWorld(wordName))
        {
            levelItem.createLevelItem(parentGrid.gameObject, level, this.gameObject);
            collectedDiamonds += level.collectedRewards;
            allDiamonds += 3;
        }

        parentGrid.Reposition();

        windowTitle.text = wordName;
        collectedDiamondsLabel.text = collectedDiamonds + "/" + allDiamonds;

    }

    public GameObject createWorldWindow(GameObject parent, string name, GameObject lastWindow)
    {
        wordName = name;
        backButton.nextWindow = lastWindow;
        BackKeyHandler backKeyHandler = GetComponent<BackKeyHandler>();
        backKeyHandler.targetWindow = lastWindow;

        return NGUITools.AddChild(parent, this.gameObject);
    }
}
