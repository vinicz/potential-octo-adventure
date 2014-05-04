using UnityEngine;
using System.Collections;

public class GameHandlerScript : MonoBehaviour
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


    // Use this for initialization
    protected virtual void Start()
    {
        GameServiceLayer.serviceLayer.gameMaster = this;

        collectedDiamondCount = 0;
        setGameState(GameState.PREGAME);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        levelRecord = GameServiceLayer.serviceLayer.levelService.getLevelRecordForScene(Application.loadedLevel);
        requiredDiamondCount = GameObject.FindGameObjectsWithTag(DIAMOND_TAG_NAME).Length;
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
            
            if (enemyCount <= 0 && collectedDiamondCount >= requiredDiamondCount)
            {
                setGameState(GameState.POSTGAME);

            }
        }

        createMapSpecificGUI();
    }

    void OnGUI()
    {
        GUIHelper.helper.adjustGUIMatrix();


        //createMapSpecificGUI();
        
        GUIHelper.helper.restoreGUIMatrix();

    }

    public virtual void createMapSpecificGUI()
    {
      
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

    public virtual void killOneEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyCount--;
    }

    public virtual void collectOneDiamond(GameObject diamond)
    {

        collectedDiamondCount++;
        diamond.SetActive(false);

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

    public void createWinMenu()
    {
//        //levelRecord.collectedDiamonds = collectedDiamondCount;
//        //levelRecord.bestTime = elapsedTime;
//        GameServiceLayer.serviceLayer.levelService.setLevelResult(Application.loadedLevel,elapsedTime);
//
//        GUI.Box(GUIHelper.helper.getRectInTheMiddle(GUIHelper.helper.smallWindowWidht, GUIHelper.helper.smallWindowHeight), "Your winner!!!!4");
//
//        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(
//            GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, GUIHelper.helper.originalHeight / 2.0f - GUIHelper.helper.getLineSize() * 2),
//            "Next level"))
//        {
//            Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getNextLevelSceneIndex(Application.loadedLevel));
//        }
//
//        createEndGameMenu();

        GameServiceLayer.serviceLayer.levelService.setLevelResult(Application.loadedLevel,elapsedTime);

        if (LevelPassed != null)
        {
            LevelPassed();
        }

    }

    public void createLoseMenu()
    {
//        GUI.Box(GUIHelper.helper.getRectInTheMiddle(GUIHelper.helper.smallWindowWidht, GUIHelper.helper.smallWindowHeight), "Lose!!!!4");
//        createEndGameMenu();

        if (LevelFailed != null)
        {
            LevelFailed();
        }

    }

    void createEndGameMenu()
    {
        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(
            GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, GUIHelper.helper.originalHeight / 2.0f - GUIHelper.helper.getLineSize()), 
            "Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(
            GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, GUIHelper.helper.originalHeight / 2.0f), "Back to Main Menu"))
        {
            Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getMainMenuIndex());
        }
    }
   
}
