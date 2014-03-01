using UnityEngine;
using System.Collections;

public class GameHandlerScript : MonoBehaviour
{

    public int ballCount;
    public int diamondCount;
    public int enemyCount;
    public GUISkin guiSkin;
    public float gameTimeLeft;
    private int collectedDiamondCount;
    protected bool isGameOver;
    protected bool isTimeUp;
    protected GUIHelper guiHelper;

    // Use this for initialization
    void Start()
    {
        collectedDiamondCount = 0;

        initializeGameHandler();


    }

    protected void initializeGameHandler()
    {
       
        isGameOver = false;
        guiHelper = gameObject.GetComponent<GUIHelper>();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void Update()
    {
        updateGameHandler();
    }

    protected void updateGameHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.LoadLevel(0); 

        if (!isGameOver)
        {

            if (gameTimeLeft > 0)
            {
                gameTimeLeft -= Time.deltaTime;
            } else
            {
                gameTimeLeft = 0;
                isTimeUp = true;
            }
        }

       
    }


    
    // Update is called once per frame
    void OnGUI()
    {
        guiHelper.adjustGUIMatrix();

        GUI.Box(new Rect(200, 10, 500, 30), "Collected Diamonds:" + collectedDiamondCount + "/" + diamondCount);
        if (isGameOver)
        {
            if (collectedDiamondCount == diamondCount)
            {
                createWinMenu();
            } else
            {
                createLoseMenu();
            }

        }

        guiHelper.restoreGUIMatrix();
    }

    public virtual void killOneBall(GameObject ball)
    {
        ball.SetActive(false);

        ballCount--;
        if (ballCount <= 0)
        {
            isGameOver = true;
        }
    }

    public virtual void killOneEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyCount--;
        if (enemyCount <= 0)
        {
            isGameOver = true;
        }
    }

    public void collectOneDiamond(GameObject diamond)
    {

        collectedDiamondCount++;
        diamond.SetActive(false);
        if (collectedDiamondCount == diamondCount)
        {
            isGameOver = true;
        }
    }

    public void createWinMenu()
    {
        GUI.Box(guiHelper.getRectInTheMiddle(guiHelper.smallWindowWidht, guiHelper.smallWindowHeight), "Your winner!!!!4");
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, guiHelper.originalHeight / 2.0f - guiHelper.getLineSize()), "Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, guiHelper.originalHeight / 2.0f), "Back to Main Menu"))
        {
            Application.LoadLevel(0);
        }
    }

    public void createLoseMenu()
    {
        GUI.Box(guiHelper.getRectInTheMiddle(guiHelper.smallWindowWidht, guiHelper.smallWindowHeight), "Lose!!!!4");
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, guiHelper.originalHeight / 2.0f - guiHelper.getLineSize()), "Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
        if (GUI.Button(guiHelper.getRectInTeTopMiddle(guiHelper.buttonWidth, guiHelper.buttonHeight, guiHelper.originalHeight / 2.0f), "Back to Main Menu"))
        {
            Application.LoadLevel(0);
        }
    }
}
