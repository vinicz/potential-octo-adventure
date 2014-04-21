using UnityEngine;
using System.Collections;

public class GameHandlerScript : MonoBehaviour
{

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
    public static GameState gameState = GameState.PREGAME;
    protected LevelRecord levelRecord;

    // Use this for initialization
    protected virtual void Start()
    {
        collectedDiamondCount = 0;
        gameState = GameState.PREGAME;
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
                gameState = GameState.POSTGAME;
            }
        }
    }

    void OnGUI()
    {
        GUIHelper.helper.adjustGUIMatrix();
        
        if (gameState == GameState.PREGAME)
        {
            createPreGameMenu();
        }

        createMapSpecificGUI();
        
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
            gameState = GameState.POSTGAME;
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
    }

    public void createWinMenu()
    {
        //levelRecord.collectedDiamonds = collectedDiamondCount;
        //levelRecord.bestTime = elapsedTime;
        GameServiceLayer.serviceLayer.levelService.setLevelResult(Application.loadedLevel,elapsedTime);

        GUI.Box(GUIHelper.helper.getRectInTheMiddle(GUIHelper.helper.smallWindowWidht, GUIHelper.helper.smallWindowHeight), "Your winner!!!!4");

        if (GUI.Button(GUIHelper.helper.getRectInTeTopMiddle(
            GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, GUIHelper.helper.originalHeight / 2.0f - GUIHelper.helper.getLineSize() * 2),
            "Next level"))
        {
            Application.LoadLevel(GameServiceLayer.serviceLayer.levelService.getNextLevelSceneIndex(Application.loadedLevel));
        }

        createEndGameMenu();
    }

    public void createLoseMenu()
    {
        GUI.Box(GUIHelper.helper.getRectInTheMiddle(GUIHelper.helper.smallWindowWidht, GUIHelper.helper.smallWindowHeight), "Lose!!!!4");
        createEndGameMenu();

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

    void createPreGameMenu()
    {

        GUI.Box(GUIHelper.helper.getRectInTeTopMiddle(
            GUIHelper.helper.bigWindowWidht, GUIHelper.helper.bigWindowHeight, 40), levelRecord.levelName);
        GUI.Label(new Rect(200, 70, 600, 30), 
            "Completed: " + levelRecord.isLevelCompleted + " Best time: " + levelRecord.bestTime );
        GUI.Label(new Rect(200, 110, 600, 300), preGameString);
        if (GUI.Button(GUIHelper.helper.getRectInTeBottomMiddle(
            GUIHelper.helper.buttonWidth, GUIHelper.helper.buttonHeight, 70), "Start"))
        {
            gameState = GameState.GAME;
        }
    }
}
