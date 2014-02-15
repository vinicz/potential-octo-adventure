using UnityEngine;
using System.Collections;

public class GameHandlerScript : MonoBehaviour
{

		public int ballCount;
		public int diamondCount;
		private int collectedDiamondCount;
		private bool isGameOver;

		// Use this for initialization
		void Start ()
		{
				collectedDiamondCount = 0;
				isGameOver = false;

				Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}

		void Update ()
		{
				if (Input.GetKeyDown (KeyCode.Escape))
						Application.Quit (); 

		}
	
		// Update is called once per frame
		void OnGUI ()
		{
				GUI.Box (new Rect (200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + diamondCount);
				if (isGameOver) {
						if (collectedDiamondCount == diamondCount) {
								GUI.Box (new Rect (200, 150, 100, 60), "Your winner!!!!4");
								if (GUI.Button (new Rect (200, 180, 100, 60), "Restart")) {
										Application.LoadLevel (0);
								}
						} else {
								GUI.Box (new Rect (200, 150, 100, 60), "Lose!!!!4");
								if (GUI.Button (new Rect (200, 180, 100, 60), "Restart")) {
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
