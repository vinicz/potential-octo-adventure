using UnityEngine;
using System.Collections;

public abstract class GameHandlerScript : MonoBehaviour
{
	public delegate void CollectedDiamondCountChangedHandler();
	public event CollectedDiamondCountChangedHandler CollectedDiamondCountChanged;
	
	public delegate void KilledEnemyCountChangedHandler();
	public event KilledEnemyCountChangedHandler KilledEnemyCountChanged;
	
	public delegate void LevelPassedHandler();
	public event LevelPassedHandler LevelPassed;
	
	public delegate void LevelFailedHandler();
	public event LevelFailedHandler LevelFailed;
	
	public delegate void GameStateChangedHandler();
	public event GameStateChangedHandler GameStateChanged;
	
	public delegate void GamePausedHandler();
	public event GamePausedHandler GamePaused;
	
	public delegate void GameResumedHandler();
	public event GameResumedHandler GameResumed;
	
	public int ballCount;
	public int enemyCount;
	public float gameTimeLeft;
	public string preGameString;
	public PlayerSpawner playerSpawner;
	public int continueCost;
	
	public enum GameState
	{
		INTRO,
		PREGAME,
		GAME,
		PAUSE,
		POSTGAME}
	;
	
	const string DIAMOND_TAG_NAME = "Diamond";
	protected int requiredDiamondCount;
	protected int collectedDiamondCount;
	protected float elapsedTime = 0;
	protected bool isTimeUp;
	protected LevelRecord levelRecord;
	protected GameState gameState = GameState.PREGAME;
	protected GameModeLogic gameModeLogic;
	
	
	
	// Use this for initialization
	protected virtual void Start()
	{
		levelRecord = GameServiceLayer.serviceLayer.levelService.getLevelRecordForScene(Application.loadedLevel);
		requiredDiamondCount = GameObject.FindGameObjectsWithTag(DIAMOND_TAG_NAME).Length;
		
		GameServiceLayer.serviceLayer.setCurrentGameMaster(this);
		
		collectedDiamondCount = 0;
		setGameState(GameState.PREGAME);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Time.timeScale = 1;
		
		gameModeLogic = GameModeLogicFactory.createGameModeLogic(this, levelRecord);
		gameModeLogic.initGame();
		
	}
	
	protected virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getMainMenuIndex()); 
		
		if (gameState == GameState.GAME)
		{
			elapsedTime += Time.deltaTime;
			
			if (gameTimeLeft > 0)
			{
				gameTimeLeft -= Time.deltaTime;
			} else
			{
				gameTimeLeft = 0;
				isTimeUp = true;
			}
			
			gameModeLogic.update();
		}
		
		levelSpecificGameLogic();
		
		
	}
	
	public abstract void levelSpecificGameLogic();
	
	public virtual void killOneBall(GameObject ball)
	{
		ball.SetActive(false);
		ballCount--;
		
		if (ballCount <= 0)
		{
			setGameState(GameState.POSTGAME);
		}
	}
	
	public virtual void killOneEnemy(GameObject enemy)
	{
		enemy.SetActive(false);
		enemyCount--;
		
		if (KilledEnemyCountChanged != null)
		{
			KilledEnemyCountChanged();
		}
	}
	
	public virtual void collectOneDiamond(GameObject diamond)
	{
		
		collectedDiamondCount++;
		diamond.GetComponent<DiamondActivator> ().DeactivateDiamond ();
		
		if (CollectedDiamondCountChanged != null)
		{
			CollectedDiamondCountChanged();
		}
		
		
	}
	
	public void loadNextLevel()
	{
		Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getNextLevelSceneIndex(Application.loadedLevel));
	}
	
	public void loadMainMenu()
	{
		Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getMainMenuIndex());
	}
	
	public void restartLevel()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void levelPassed()
	{
		int collectedRewardCount = gameModeLogic.calculateReward();
		
		GameServiceLayer.serviceLayer.levelService.setLevelResult(levelRecord, collectedRewardCount, elapsedTime);
		
		if (LevelPassed != null)
		{
			LevelPassed();
		}
		
	}
	
	public void levelFailed()
	{
		
		if (LevelFailed != null)
		{
			LevelFailed();
		}
		
	}
	
	public void pauseLevel()
	{
		Time.timeScale = 0;
		setGameState(GameState.PAUSE);
		
		if (GamePaused != null)
		{
			GamePaused();
		}
		
	}
	
	public void resumeLevel()
	{
		Time.timeScale = 1;
		setGameState(GameState.GAME);
		
		if (GameResumed != null)
		{
			GameResumed();
		}
	}
	
	public void continueLevel()
	{
		GameServiceLayer.serviceLayer.itemService.spendTokens(continueCost);
		
		if (ballCount == 0)
		{
			ballCount++;
		}
		
		gameModeLogic.initGame();
		resumeLevel();
		playerSpawner.spawnPlayer();
	}
	
	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus && gameState == GameState.GAME)
		{
			pauseLevel();
		}
	}
	
	void OnApplicationFocus(bool focusStatus)
	{
		if (!focusStatus && gameState == GameState.GAME)
		{
			pauseLevel();
		} 
	}
	
	public int getCollectedDiamonds()
	{
		return collectedDiamondCount;
	}
	
	public int getRequiredDiamondCount()
	{
		return requiredDiamondCount;
	}
	
	public GameState getGameState()
	{
		return gameState;
	}
	
	public void setGameState(GameState state)
	{
		if (gameState == GameState.GAME && state == GameState.POSTGAME)
		{
			gameModeLogic.determineGameResult();
		}
		
		gameState = state;
		
		if (GameStateChanged != null)
		{
			GameStateChanged();
		}
	}
	
	public float getElapsedTime()
	{
		return elapsedTime;
	}
	
	public LevelRecord getCurrentLevelRecord()
	{
		return levelRecord;
	}
	
	public int getEnemyCount()
	{
		return enemyCount;
	}
	
	public float getGameTimeLeft()
	{
		return gameTimeLeft;
	}
	
	public void setGameTimeLeft(float time)
	{
		gameTimeLeft = time;
	}
	
	public int getBallCount()
	{
		return ballCount;
	}
	
	public void setBallCount(int balls)
	{
		ballCount = balls;
	}
	
	
}
