﻿using UnityEngine;
using System.Collections;

public class BowlingMasterScript : DiamondMasterScript
{


    protected override void Update()
    {
        base.Update();

        if (isTimeUp)
        {
            setGameState(GameState.POSTGAME);
        }

		GUI.Box(new Rect(400, 10, 150, 30), "Enemies remaining: " + enemyCount);
		GUI.Box(new Rect(600, 10, 100, 30), ((int)gameTimeLeft).ToString());
		
		if (gameState == GameState.POSTGAME)
		{
			if (enemyCount > 0)
			{
				levelFailed();
			} else
			{
				levelPassed();
			}
		}
        
    }

  
    public override void killOneBall(GameObject ball)
    {
        ball.transform.position = transform.position;
        ball.rigidbody.velocity = new Vector3(0,0,0);
        ball.rigidbody.angularVelocity = new Vector3(0,0,0);
    }



}
