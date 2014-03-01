using UnityEngine;
using System.Collections;

public class BowlingMasterScript : GameHandlerScript
{


    
    void Start()
    {
        initializeGameHandler();
    }

    void Update()
    {
        updateGameHandler();

        if (isTimeUp)
        {
            isGameOver = true;
        }
        
    }

    void OnGUI()
    {
        guiHelper.adjustGUIMatrix();

        GUI.Box(new Rect(400, 10, 150, 30),  "Enemies remaining: " +enemyCount);

        GUI.Box(new Rect(600, 10, 100, 30), ((int)gameTimeLeft).ToString());

        if (isGameOver)
        {
            if (enemyCount > 0)
            {
                createLoseMenu();
            } else
            {
                createWinMenu();
            }
        }

        guiHelper.restoreGUIMatrix();
    }

    public override void killOneBall(GameObject ball)
    {
        ball.transform.position = transform.position;
        ball.rigidbody.velocity = new Vector3(0,0,0);
        ball.rigidbody.angularVelocity = new Vector3(0,0,0);
    }



}
