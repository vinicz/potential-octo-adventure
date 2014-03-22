using UnityEngine;
using System.Collections;

public class MapGroupButtonsGenerator : MonoBehaviour {

    public LevelButtonTool buttonFactory;

	// Use this for initialization
	void Start () {

        int row = 0;
        foreach (string levelGroup in GameDataStorage.storage.getLevelGroups())
        {
            //GUI.Label(GUIHelper.helper.getRectForNormalButton(100,50+row*GUIHelper.helper.getLineSize()),levelGroup);
            
            int column = 0;
            foreach (LevelRecord level in GameDataStorage.storage.getLevelsInGroup(levelGroup))
            {
                
                string levelName = (level.isLevelCompleted? "*":"") + level.levelName;
                
                //if (GUI.Button(GUIHelper.helper.getRectForNormalButton(150+column*GUIHelper.helper.buttonWidth,50+row*GUIHelper.helper.getLineSize()), levelName))
                //{
                 //   Application.LoadLevel(level.getLevelIndex());
                //}

                GameObject newButton = buttonFactory.createButton(this.gameObject,levelName, level.getLevelIndex());
                newButton.transform.localPosition = new Vector3(-300+column*200 ,170 +row*-60,0);

                column++;
                
                if(column>=4)
                {
                    row++;
                    column=0;
                }
                
            }
            row++;
        }
	
	}
	
	
}
