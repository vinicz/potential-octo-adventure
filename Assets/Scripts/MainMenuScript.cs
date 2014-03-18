using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour
{
    public List<string> levelList;
    public GUISkin guiSkin;
    private bool isSinglePlayerSelected;
    private bool isMultiplayerSelected;

    void Start()
    {
        isSinglePlayerSelected = false;
        isMultiplayerSelected = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSinglePlayerSelected || isMultiplayerSelected)
            {
                isSinglePlayerSelected = false;
                isMultiplayerSelected = false;
            } else
            {

                Application.Quit(); 
            }


        }
                        

    }

    void OnGUI()
    {
        GUIHelper.helper.adjustGUIMatrix();

        //GUI.skin = guiSkin;
                
        GUI.Box(GUIHelper.helper.getRectInTheMiddle(GUIHelper.helper.bigWindowWidht, GUIHelper.helper.bigWindowHeight), "Main Menu");
        if (!isSinglePlayerSelected && !isMultiplayerSelected)
        {
            showMainMenu();
            
        } else if (isSinglePlayerSelected)
        {

            showSinglePlayerMenu();

        } else if (isMultiplayerSelected)
        {
            showMultiplayerMenu();
        }

        GUIHelper.helper.restoreGUIMatrix();

    }

    private void showMainMenu()
    {
        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, 90), "Single Player"))
        {
            isSinglePlayerSelected = true;
        }
        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, 90 + GUIHelper.helper.getLineSize()), "Multi Player"))
        {
            isMultiplayerSelected = true;
        }


    }

    private void showSinglePlayerMenu()
    {


        int row = 0;
        foreach (string levelGroup in GameDataStorage.storage.getLevelGroups())
        {
            GUI.Label(GUIHelper.helper.getRectForNormalButton(100,50+row*GUIHelper.helper.getLineSize()),levelGroup);

            int column = 0;
            foreach (LevelRecord level in GameDataStorage.storage.getLevelsInGroup(levelGroup))
            {

                string levelName = (level.isLevelCompleted? "*":"") + level.levelName;
                   
                if (GUI.Button(GUIHelper.helper.getRectForNormalButton(150+column*GUIHelper.helper.buttonWidth,50+row*GUIHelper.helper.getLineSize()), levelName))
                {
                    Application.LoadLevel(level.getLevelIndex());
                }
                column++;

                if(column>=5)
                {
                    row++;
                    column=0;
                }

            }
            row++;
        }
                    
              

                
        if (GUI.Button(GUIHelper.helper.getRectInTeBottomMiddle(GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, 80), "Back"))
        {
            isSinglePlayerSelected = false;
        }

    }

    private void showMultiplayerMenu()
    {
      
        foreach (LevelRecord level in GameDataStorage.storage.getMultiplayerLevels())
        {
            if (GUI.Button(GUIHelper.helper.getRectForNormalButton(200,50), level.levelName))
            {
                Application.LoadLevel(level.getLevelIndex());
            }
        }

        if (GUI.Button(GUIHelper.helper.getRectForNormalButton(200,100), "Back"))
        {
            isMultiplayerSelected = false;
        }


    }
}
