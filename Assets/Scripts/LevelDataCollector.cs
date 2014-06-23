using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelDataCollector : MonoBehaviour
{
	
		public static  LevelDataCollector levelDebugMonitor;
		private List<float> diamondCollectionTimes = new List<float> ();
		private float gameTime;
		private LevelRecord currentLevel;
		private bool uploadStarted;
		private WWW ramballServerUpload;
	
		void Start ()
		{
		
				if (levelDebugMonitor == null) {
						levelDebugMonitor = this;
						levelDebugMonitor.initLevelDataCollector ();
						DontDestroyOnLoad (this);
			
						GameServiceLayer.serviceLayer.LevelConfigurationFinished += initLevelDataCollector;
			
				} else if (levelDebugMonitor != this) {
						Destroy (this);
				}
		
		}
	
		public void initLevelDataCollector ()
		{
		
				if (GameServiceLayer.serviceLayer.gameMaster != null) {
						if (GameServiceLayer.serviceLayer.gameMaster.GetType () == typeof(DiamondMasterScript)) {
								DiamondMasterScript diamondMaster = (DiamondMasterScript)GameServiceLayer.serviceLayer.gameMaster;
								diamondMaster.CollectedDiamondCountChanged += onCollectedDiamondCountChanged;
						}
						GameServiceLayer.serviceLayer.gameMaster.LevelFailed += onLevelFailed;
						GameServiceLayer.serviceLayer.gameMaster.LevelPassed += onLevelPassed;
						gameTime = GameServiceLayer.serviceLayer.gameMaster.getElapsedTime ();
						currentLevel = GameServiceLayer.serviceLayer.gameMaster.getCurrentLevelRecord ();
						diamondCollectionTimes.Clear ();
						uploadStarted = false;
				}
		
		
		}
	
		void Update ()
		{
				if (GameServiceLayer.serviceLayer.gameMaster != null) {
						gameTime = GameServiceLayer.serviceLayer.gameMaster.getElapsedTime ();
				}
		}
	
		void OnGUI ()
		{
				if (currentLevel != null) {
						GUI.Box (new Rect (0, 0, 150, (diamondCollectionTimes.Count + 3) * 30), currentLevel.levelName);
						GUI.Label (new Rect (0, 30, 150, 30), "Time: " + gameTime.ToString ());
			
						int diamondCounter = 0;
						foreach (float diamondTime in diamondCollectionTimes) {
								GUI.Label (new Rect (0, 60 + diamondCounter * 30, 150, 300), "Diamond" + diamondCounter + ": " + diamondTime);
				
								diamondCounter++;
						}
						if (uploadStarted) {
				
								if (ramballServerUpload.error != null) {
										GUI.Label (new Rect (0, (diamondCollectionTimes.Count + 2) * 30, 150, 300), "Couldnt upload result!");
								} else if (ramballServerUpload.isDone && ramballServerUpload.text.Equals ("OK")) {
										GUI.Label (new Rect (0, (diamondCollectionTimes.Count + 2) * 30, 150, 300), "Result uploaded!");
								} else {
										GUI.Label (new Rect (0, (diamondCollectionTimes.Count + 2) * 30, 150, 300), "Uploading result...");
								}
				
				
						}
				}
		}
	
		void onCollectedDiamondCountChanged ()
		{
				diamondCollectionTimes.Add (GameServiceLayer.serviceLayer.gameMaster.getElapsedTime ());
		
		}
	
		void onLevelPassed ()
		{
				onLevelEnded (true);
		}
	
		void onLevelFailed ()
		{
				onLevelEnded (false);
		}
	
		void onLevelEnded (bool levelPassed)
		{
				string requestURL = "http://ramball-mudstudio.rhcloud.com/rest/LevelData/submit?";
				requestURL += "levelName=" + currentLevel.levelName;
				requestURL += "&levelPassed=" + levelPassed.ToString ();
				requestURL += "&gameTime=" + gameTime.ToString ();

				string ballName = GameServiceLayer.serviceLayer.characterService.getSelectedPlayerCharacter ().productId;

				if (!ballName.Equals ("")) {
						requestURL += "&ballName=" + ballName;
				}

				if (diamondCollectionTimes.Count > 0) {
						requestURL += "&diamondCollections=";

						int counter = 1;
						foreach (float diamondCollectionTime in diamondCollectionTimes) {
								requestURL += diamondCollectionTime.ToString ();

								if (counter != diamondCollectionTimes.Count) {
										requestURL += ",";
								}

								counter++;
			
						}
				}

				ramballServerUpload = new WWW (requestURL);
				uploadStarted = true; 
		}
	
		void OnDestroy ()
		{
				GameServiceLayer.serviceLayer.LevelConfigurationFinished -= initLevelDataCollector;
		}

}
