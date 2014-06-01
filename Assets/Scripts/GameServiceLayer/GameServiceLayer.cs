using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameServiceLayer : MonoBehaviour {
	
	
	public static GameServiceLayer serviceLayer;
	
	public GameHandlerScript gameMaster;
	public LevelService levelService;
	public ItemService itemService;
	public OptionsService optionsService;
	public BackgroundMusicPlayer backgroundMusicPlayer;
	public List<PlayerSpawner> playerSpawnerList;
	
	public delegate void LevelConfigurationFinishedHandler();
	public event LevelConfigurationFinishedHandler LevelConfigurationFinished;
	
	void Awake () {
		
		if (serviceLayer == null)
		{
			DontDestroyOnLoad(gameObject);
			serviceLayer = this;
		} else if (serviceLayer != this)
		{
			Destroy(gameObject);
		}

		playerSpawnerList = new List<PlayerSpawner> ();

	}
	
	public void setCurrentGameMaster(GameHandlerScript master)
	{
		this.gameMaster = master;
		
		if(LevelConfigurationFinished != null)
		{
			LevelConfigurationFinished();
		}
	}
}
