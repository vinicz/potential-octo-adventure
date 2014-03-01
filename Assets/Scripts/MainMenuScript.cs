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

        GUILayout.BeginArea(guiHelper.getRectInTheMiddle(guiHelper.smallWindowWidht, guiHelper.smallWindowHeight));
        for (int i = 1; i < levelList.Count; i++)
        {
            if (GUILayout.Button(levelList [i]))
            {

                Application.LoadLevel(i);
            }
                
        }
                    
            
        GUILayout.EndArea();    
              

                
        if (GUI.Button(guiHelper.getRectInTeBottomMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, 80), "Back"))
        {
            isSinglePlayerSelected = false;
        }

    }

    private void showMultiplayerMenu()
    {
        if (GUI.Button(guiHelper.getRectForNormalButton(200,50), "MultiTestMap"))
        {
            Application.LoadLevel(3);
        }
        if (GUI.Button(guiHelper.getRectForNormalButton(200,100), "Back"))
        {
            isMultiplayerSelected = false;
        }


    }
}
