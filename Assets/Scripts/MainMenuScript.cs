using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenuScript : MonoBehaviour
{
		public List<string> levelList;
		public GUISkin guiSkin;
		private bool isSinglePlayerSelected;
		private bool isMultiplayerSelected;
		private GUIConstants guiConstants;

		void Start ()
		{
				isSinglePlayerSelected = false;
				isMultiplayerSelected = false;
				guiConstants = gameObject.GetComponent<GUIConstants> ();
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape))
						Application.Quit (); 

		}

		void OnGUI ()
		{
				GUI.skin = guiSkin;
				
				GUI.Box (guiConstants.getRectInTheMiddle (guiConstants.getBigWindowWidht (), guiConstants.getBigWindowHeight ()), "Main Menu");
				if (!isSinglePlayerSelected && !isMultiplayerSelected) {
						showMainMenu ();
			
				} else if (isSinglePlayerSelected) {

						showSinglePlayerMenu ();

				} else if (isMultiplayerSelected) {
						showMultiplayerMenu ();
				}

		}

		private void showMainMenu ()
		{
				if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), 90), "Single Player")) {
						isSinglePlayerSelected = true;
				}
				if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), 90 + guiConstants.getLineSize ()), "Multi Player")) {
						isMultiplayerSelected = true;
				}


		}

		private void showSinglePlayerMenu ()
		{

				GUILayout.BeginArea (guiConstants.getRectInTheMiddle (guiConstants.getSmallWindowWidht (), guiConstants.getSmallWindowHeight ()));
				for (int i = 1; i < levelList.Count; i++) {
						if (GUILayout.Button (levelList [i])) {

								Application.LoadLevel (i);
						}
		       	
				}
		        	
			
				GUILayout.EndArea ();	
		      

				
				if (GUI.Button (guiConstants.getRectInTeBottomMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), 80), "Back")) {
						isSinglePlayerSelected = false;
				}

		}

		private void showMultiplayerMenu ()
		{
				if (GUI.Button (new Rect (200, 50, guiConstants.getButtonWidth (), guiConstants.getButtonHeight ()), "MultiTestMap")) {
						isMultiplayerSelected = false;
						//Application.LoadLevel (0);
				}
				if (GUI.Button (new Rect (200, 70, guiConstants.getButtonWidth (), guiConstants.getButtonHeight ()), "Back")) {
						isMultiplayerSelected = false;
				}


		}
}
