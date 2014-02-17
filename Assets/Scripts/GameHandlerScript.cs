using UnityEngine;
using System.Collections;

public class GameHandlerScript : MonoBehaviour
{

		public int ballCount;
		public int diamondCount;
		public GUISkin guiSkin;
		private int collectedDiamondCount;
		private bool isGameOver;
		private GUIConstants guiConstants;

		// Use this for initialization
		void Start ()
		{
				collectedDiamondCount = 0;
				isGameOver = false;

				guiConstants = gameObject.GetComponent<GUIConstants> ();

				Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape))
						Application.LoadLevel (0); 

		}
	
		// Update is called once per frame
		void OnGUI ()
		{
				GUI.Box (new Rect (200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + diamondCount);
				if (isGameOver) {
						if (collectedDiamondCount == diamondCount) {
								GUI.Box (guiConstants.getRectInTheMiddle (guiConstants.getSmallWindowWidht (), guiConstants.getSmallWindowHeight ()), "Your winner!!!!4");
								if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), Screen.height / 2.0f - guiConstants.getLineSize ()), "Restart")) {
										Application.LoadLevel (1);
								}
								if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), Screen.height / 2.0f), "Back to Main Menu")) {
										Application.LoadLevel (0);
								}
						} else {
								GUI.Box (guiConstants.getRectInTheMiddle (guiConstants.getSmallWindowWidht (), guiConstants.getSmallWindowHeight ()), "Lose!!!!4");
								if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), Screen.height / 2.0f - guiConstants.getLineSize ()), "Restart")) {
										Application.LoadLevel (1);
								}
								if (GUI.Button (guiConstants.getRectInTeTopMiddle (guiConstants.getButtonWidth (), guiConstants.getButtonHeight (), Screen.height / 2.0f), "Back to Main Menu")) {
										Application.LoadLevel (0);
								}
						}

				}
		}

		public void killOneBall (GameObject ball)
		{
				ball.SetActive (false);

				ballCount--;
				if (ballCount <= 0) {
						isGameOver = true;
				}
		}

		public void collectOneDiamond (GameObject diamond)
		{

				collectedDiamondCount++;
				diamond.SetActive (false);
				if (collectedDiamondCount == diamondCount) {
						isGameOver = true;
				}
		}
}
