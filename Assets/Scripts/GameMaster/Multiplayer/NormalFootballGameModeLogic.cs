using UnityEngine;
using System.Collections;

public class NormalFootballGameModeLogic :GameModeLogic
{
    private FootballGameMaster gameMaster;
    private float suddenDeathBallSpawnTime;

    public NormalFootballGameModeLogic(FootballGameMaster gameMaster, float suddenDeathBallSpawnTime)
    {
        this.suddenDeathBallSpawnTime = suddenDeathBallSpawnTime;
        this.gameMaster = gameMaster;
    }
    


    public void initGame()
    {

    }

    public void update()
    {
        endGameIfRequiredGoalReached();
        spawnSuddenDeathBall();
    }

    public void determineGameResult()
    {
        MultiGameMaster.MultiPlayerStruct player = gameMaster.getCurrentPlayerInfo();
        int playerTeamIndex = player.team - 1;
        float playersTeamScore = gameMaster.getGameScore() [playerTeamIndex];

        if (playersTeamScore >= gameMaster.requiredGoalCount)
        {
            gameMaster.levelPassed();
        } else
        {
            gameMaster.levelFailed();
        }
    
    }

    public int calculateReward()
    {
        return 0;
    }
    
    void endGameIfRequiredGoalReached()
    {
        Vector2 gameScore = gameMaster.getGameScore();
        if (gameScore[0] >= gameMaster.requiredGoalCount || gameScore[1] >= gameMaster.requiredGoalCount)
        {
            gameMaster.setGameState(GameHandlerScript.GameState.POSTGAME);
        }
    }

    void spawnSuddenDeathBall()
    {
        if (gameMaster.getGameTimeLeft() <= 0)
        {
            gameMaster.spawnNewBall();
            gameMaster.setGameTimeLeft(suddenDeathBallSpawnTime);
        }
    }
}
