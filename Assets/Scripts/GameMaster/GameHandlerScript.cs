using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameHandlerScript : MonoBehaviour
{
    
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

    public delegate void PlayerSpawnedHandler();
    
    public event PlayerSpawnedHandler PlayerSpawned;

    public float gameTimeLeft;
    public string preGameString;
    public int continueCost;
    public int ballCount;
    
    public enum GameState
    {
        INTRO,
        PREGAME,
        GAME,
        PAUSE,
        POSTGAME}
    ;
    
        
    protected float elapsedTime = 0;
    protected bool isTimeUp;
    protected LevelRecord levelRecord;
    protected GameState gameState = GameState.PREGAME;
    protected GameModeLogic gameModeLogic;
    protected List<PlayerSpawner> playerSpawnerList;
    
    
    
    // Use this for initialization
    protected virtual void Start()
    {
        levelRecord = GameServiceLayer.serviceLayer.levelService.getLevelRecordForScene(Application.loadedLevel);
        playerSpawnerList = playerSpawnerList = GameServiceLayer.serviceLayer.playerSpawnerList;
        
        GameServiceLayer.serviceLayer.setCurrentGameMaster(this);

        setGameState(GameState.PREGAME);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1;
        
        gameModeLogic = createGameModeLogic();
        gameModeLogic.initGame();
        
    }

    protected abstract GameModeLogic createGameModeLogic();
    
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            handleEscapeKey(); 
        
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
    }

    protected virtual void handleEscapeKey()
    {
        Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getMainMenuIndex());
    }
    
    public virtual void killOneBall(GameObject ball)
    {
        ball.SetActive(false);
        ballCount--;
        
        if (ballCount <= 0)
        {
            setGameState(GameState.POSTGAME);
        }
    }
    
    public virtual void loadNextLevel()
    {
        Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getNextLevelSceneIndex(Application.loadedLevel));
    }
    
    public virtual void loadMainMenu()
    {
        Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getMainMenuIndex());
    }
    
    public virtual void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public virtual void levelPassed()
    {
        int collectedRewardCount = gameModeLogic.calculateReward();
        
        GameServiceLayer.serviceLayer.levelService.setLevelResult(levelRecord, collectedRewardCount, elapsedTime);
        
        if (LevelPassed != null)
        {
            LevelPassed();
        }
        
    }
    
    public virtual void levelFailed()
    {
        
        if (LevelFailed != null)
        {
            LevelFailed();
        }
        
    }
    
    public virtual void pauseLevel()
    {
        Time.timeScale = 0;
        setGameState(GameState.PAUSE);
        
        if (GamePaused != null)
        {
            GamePaused();
        }
        
    }
    
    public virtual void resumeLevel()
    {
        Time.timeScale = 1;
        setGameState(GameState.GAME);
        
        if (GameResumed != null)
        {
            GameResumed();
        }
    }
    
    public virtual void continueLevel()
    {
        GameServiceLayer.serviceLayer.itemService.spendTokens(continueCost);
        
        if (ballCount == 0)
        {
            ballCount++;
        }
        
        gameModeLogic.initGame();
        resumeLevel();

        spawnPlayer();

    }
    
    protected virtual void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && gameState == GameState.GAME)
        {
            pauseLevel();
        }
    }
    
    protected virtual  void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus && gameState == GameState.GAME)
        {
            pauseLevel();
        } 
    }

    protected virtual void spawnPlayer()
    {
        foreach (PlayerSpawner playerSpawner in playerSpawnerList)
        {
            playerSpawner.spawnPlayer();
        }

        if (PlayerSpawned != null)
        {
            PlayerSpawned();
        }
    }
    
    public virtual GameState getGameState()
    {
        return gameState;
    }
    
    public virtual void setGameState(GameState state)
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
    
    public virtual float getElapsedTime()
    {
        return elapsedTime;
    }
    
    public virtual LevelRecord getCurrentLevelRecord()
    {
        return levelRecord;
    }
    
    public virtual float getGameTimeLeft()
    {
        return gameTimeLeft;
    }
    
    public virtual void setGameTimeLeft(float time)
    {
        gameTimeLeft = time;
    }
    
    public virtual int getBallCount()
    {
        return ballCount;
    }
    
    public virtual void setBallCount(int balls)
    {
        ballCount = balls;
    }
    
    
}
