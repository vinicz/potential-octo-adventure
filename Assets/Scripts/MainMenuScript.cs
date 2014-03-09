using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour
{
    public List<string> levelList;
    public GUISkin guiSkin;
    private bool isSinglePlayerSelected;
    private bool isMultiplayerSelected;
    private GUIHelper guiHelper;

    void Start()
    {
        isSinglePlayerSelected = false;
        isMultiplayerSelected = false;
        guiHelper = gameObject.GetComponent<GUIHelper>();

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
        guiHelper.adjustGUIMatrix();

        GUI.skin = guiSkin;
                
        GUI.Box(guiHelper.getRectInTheMiddle(guiHelper.bigWindowWidht, guiHelper.bigWindowHeight), "Main Menu");
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

        guiHelper.restoreGUIMatrix();

    }

    private void showMainMenu()
    {
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, 90), "Single Player"))
        {
            isSinglePlayerSelected = true;
        }
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, 90 + guiHelper.getLineSize()), "Multi Player"))
        {
            isMultiplayerSelected = true;
        }


    }

    private void showSinglePlayerMenu()
    {


        int row = 0;
        foreach (string levelGroup in GameDataStorage.storage.getLevelGroups())
        {
            GUI.Label(guiHelper.getRectForNormalButton(100,50+row*guiHelper.getLineSize()),levelGroup);

            int column = 0;
            foreach (LevelRecord level in GameDataStorage.storage.getLevelsInGroup(levelGroup))
            {

                string levelName = (level.isLevelCompleted? "*":"") + level.levelName;
                   
                if (GUI.Button(guiHelper.getRectForNormalButton(150+column*guiHelper.buttonWidth,50+row*guiHelper.getLineSize()), levelName))
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
                    
              

                
        if (GUI.Button(guiHelper.getRectInTeBottomMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, 80), "Back"))
        {
            isSinglePlayerSelected = false;
        }

    }

    private void showMultiplayerMenu()
    {
      
        foreach (LevelRecord level in GameDataStorage.storage.getMultiplayerLevels())
        {
            if (GUI.Button(guiHelper.getRectForNormalButton(200,50), level.levelName))
            {
                Application.LoadLevel(level.getLevelIndex());
            }
        }

        if (GUI.Button(guiHelper.getRectForNormalButton(200,100), "Back"))
        {
            isMultiplayerSelected = false;
        }


    }
}
