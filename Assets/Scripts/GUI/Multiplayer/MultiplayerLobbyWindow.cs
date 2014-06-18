using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiplayerLobbyWindow : UIWindow
{

		public UILabel redTeam;
		public UILabel blueTeam;
		public UIButton startButton;
		private MultiGameMaster gameMaster;
		private bool inited;

		void Start ()
		{
				gameMaster = (MultiGameMaster)GameServiceLayer.serviceLayer.gameMaster;

				if (gameMaster.isPlayerServer ()) {
						startButton.gameObject.SetActive (true);
				} else {
						startButton.gameObject.SetActive (false);
				}

				setUpPlayerList ();
				
				gameMaster.PlayerListChanged += setUpPlayerList;

				gameMaster.GameStateChanged += onGameStateChanged;
		}

		public override void initWindow ()
		{
		}

		void onStartButtonPressed ()
		{
				gameMaster.startGame ();
		}

		void onRedTeamButtonPressed ()
		{
				gameMaster.playerJoinedToTeam (1);
		}

		void onBlueTeamButtonPressed ()
		{
				gameMaster.playerJoinedToTeam (2);
		}

		void onRandomTeamButtonPressed ()
		{
				gameMaster.playerJoinedRandom ();
		}

		void onGameStateChanged ()
		{
				if (gameMaster.getGameState () == GameHandlerScript.GameState.GAME) {
						this.gameObject.SetActive (false);
				}
		}

		void setUpPlayerList ()
		{
				redTeam.text = "";
				blueTeam.text = "";
				List<MultiGameMaster.MultiPlayerStruct> playerList = gameMaster.getPlayerList ();
				foreach (MultiGameMaster.MultiPlayerStruct player in playerList) {
						if (player.team == 1) {
								redTeam.text += "\n" + player.name;
						} else {
								blueTeam.text += "\n" + player.name;
						}
				}
		}
	
		void OnDestroy ()
		{
				gameMaster.GameStateChanged -= onGameStateChanged;
				gameMaster.PlayerListChanged -= setUpPlayerList;
		}
}
