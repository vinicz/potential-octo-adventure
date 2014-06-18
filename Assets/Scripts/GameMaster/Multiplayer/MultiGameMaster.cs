using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class MultiGameMaster : GameHandlerScript
{

		public delegate void PlayerListChangedHandler ();

		public event PlayerListChangedHandler PlayerListChanged;

		public class MultiPlayerStruct
		{
				public string name;
				public int team;
				public NetworkPlayer networkPlayer;

				public MultiPlayerStruct (string name, int team, NetworkPlayer guid)
				{
						this.name = name;
						this.team = team;
						this.networkPlayer = guid;
				}
		

		};

		private NetworkPlayer currentPlayer;
		private string currentPlayerName;
		private List<MultiPlayerStruct> playerList = new List<MultiPlayerStruct> ();

		public void serverInitialized (string playerName)
		{
				currentPlayerName = playerName;

		}

		public void playerConnectedToServer (NetworkPlayer player, string name)
		{
				currentPlayer = player;
				currentPlayerName = name;
		}

		public void playerJoinedRandom ()
		{
				networkView.RPC ("addPLayerRandomOnServer", RPCMode.Server, currentPlayerName, currentPlayer);	
		}

		public void playerJoinedToTeam (int team)
		{
				networkView.RPC ("addPLayerWithTeamOnServer", RPCMode.Server, currentPlayerName, team, currentPlayer);
		}

		[RPC]
		void addPLayerRandomOnServer (string name, NetworkPlayer networkPlayer)
		{
				int team = determineRandomTeam ();

				addPlayerOnserver (name, team, networkPlayer);
		}

		[RPC]
		void addPLayerWithTeamOnServer (string name, int team, NetworkPlayer networkPlayer)
		{
				addPlayerOnserver (name, team, networkPlayer);
		}

		void addPlayerOnserver (string name, int team, NetworkPlayer networkPlayer)
		{
				playerList.Add (new MultiPlayerStruct (name, team, networkPlayer));

				if (PlayerListChanged != null) {
						PlayerListChanged ();
				}

				networkView.RPC ("addPlayerOnClient", RPCMode.OthersBuffered, name, team, networkPlayer);
		}

		[RPC]
		void addPlayerOnClient (string name, int team, NetworkPlayer networkPlayer)
		{
				playerList.Add (new MultiPlayerStruct (name, team, networkPlayer));

				if (PlayerListChanged != null) {
						PlayerListChanged ();
				}
		}

		public override void setGameState (GameState state)
		{
				if (isPlayerServer ()) {
						networkView.RPC ("setRemoteGameState", RPCMode.AllBuffered, state.ToString ());
				}
		}

		[RPC]
		void setRemoteGameState (string stringState)
		{
				GameState state = (GameState)Enum.Parse (typeof(GameState), stringState);

				base.setGameState (state);
		}

		public void startGame ()
		{
				if (isPlayerServer ()) {
						networkView.RPC ("setRemoteGameState", RPCMode.AllBuffered, GameState.GAME.ToString ());
						networkView.RPC ("spawnRemotePlayers", RPCMode.AllBuffered);
				}
		}
		
		[RPC]
		void spawnRemotePlayers ()
		{
				foreach (PlayerSpawner playerSpawner in playerSpawnerList) {
						playerSpawner.spawnPlayer ();
				}
		}
	
		protected abstract int determineRandomTeam ();

		public abstract List<int> getPossibleTeams ();

		public List<MultiPlayerStruct> getPlayerList ()
		{
				return playerList;
		}

		public bool isPlayerServer ()
		{
				return Network.isServer;
		}


}
