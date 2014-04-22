using UnityEngine;
using System.Collections;

public class WorldWindow : MonoBehaviour
{

    public UIGrid parentGrid;
    public LevelItem levelItem;
    public string wordName;
    public WindowOpenerScript backButton;
    public UILabel windowTitle;

    // Use this for initialization
    void Start()
    {
        int collectedDiamonds = 0;
        int allDiamonds = 0;
        int levelCounter = 15;
        GameObject currentParentElement = null;
        int rowCounter = 0;
        int columnCounter = 0;

        foreach (LevelRecord level in GameServiceLayer.serviceLayer.levelService.getLevelsInWorld(wordName))
        {
            if(columnCounter==5)
            {
                columnCounter = 0;
                rowCounter++;
            }

            if(levelCounter==15)
            {
                currentParentElement = NGUITools.AddChild(parentGrid.gameObject, new GameObject());
                levelCounter = 0;
                rowCounter = 0;
            }

            GameObject newLevelItem = levelItem.createLevelItem(currentParentElement, level, this.gameObject);
            newLevelItem.transform.position = new Vector3(newLevelItem.transform.position.x+columnCounter*0.25f,
                                                          newLevelItem.transform.position.y,
                                                          newLevelItem.transform.position.z-rowCounter*0.15f+0.25f);

            levelCounter++;
            columnCounter++;
        }

        parentGrid.Reposition();

        windowTitle.text = wordName;

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
