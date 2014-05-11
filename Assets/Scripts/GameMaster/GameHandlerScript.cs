using UnityEngine;
using System.Collections;

public abstract class GameHandlerScript : MonoBehaviour
{
    public delegate void CollectedDiamondCountChangedHandler();
    public event CollectedDiamondCountChangedHandler CollectedDiamondCountChanged;

    public delegate void LevelPassedHandler();
    public event LevelPassedHandler LevelPassed;
  
    public delegate void LevelFailedHandler();
    public event LevelPassedHandler LevelFailed;

    public delegate void GameStateChangedHandler();
    public event GameStateChangedHandler GameStateChanged;

    public int ballCount;
    public int enemyCount;
    public GUISkin guiSkin;
    public float gameTimeLeft;
    public string preGameString;


    public enum GameState
    {
        INTRO,
        PREGAME,
        GAME,
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
        GameServiceLayer.serviceLayer.gameMaster = this;

        collectedDiamondCount = 0;
        setGameState(GameState.PREGAME);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        levelRecord = GameServiceLayer.serviceLayer.levelService.getLevelRecordForScene(Application.loadedLevel);
        requiredDiamondCount = GameObject.FindGameObjectsWithTag(DIAMOND_TAG_NAME).Length;
        gameModeLogic = GameModeLogicFactory.createGameModeLogic(this, levelRecord);

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
        }

        levelSpecificGameLogic();
        gameModeLogic.update();
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

        gameModeLogic.onEnemyKilled();
    }

    public virtual void collectOneDiamond(GameObject diamond)
    {

        collectedDiamondCount++;
        diamond.SetActive(false);

        if (CollectedDiamondCountChanged != null)
        {
            CollectedDiamondCountChanged();
        }

        gameModeLogic.onDiamondCollected();
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

        GameServiceLayer.serviceLayer.levelService.setLevelResult(Application.loadedLevel, collectedRewardCount, elapsedTime);
        
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


}
