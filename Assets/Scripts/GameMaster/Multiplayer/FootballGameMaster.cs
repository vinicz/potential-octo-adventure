using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FootballGameMaster : MultiGameMaster
{
    public delegate void GameScoreChangedHandler();
    public event GameScoreChangedHandler GameScoreChanged;


    public int requiredGoalCount = 10;
    private Vector2 gameScore;
    private MultiPlayerBallSpawner ballSpawner;

    public void addGoalToTeam(int team, GameObject ball)
    {
        networkView.RPC("addGoalToTeamRemote", RPCMode.AllBuffered, team);
        ballSpawner.resetBall(ball);
    }

    [RPC]
    void addGoalToTeamRemote(int team)
    {
        int gameScoreTeamIndex = team - 1;
        gameScore[gameScoreTeamIndex]++;

        if (GameScoreChanged != null)
        {
            GameScoreChanged();
        }
    }

    protected override int determineRandomTeam()
    {
        return 1;
    }
    
    public override List<int> getPossibleTeams()
    {

        return new List<int> { 1,2 };
    }

    protected override GameModeLogic createGameModeLogic()
    {
        return new NormalFootballGameModeLogic(this,10);
        
    }

    public Vector2 getGameScore()
    {
        return gameScore;
    }

    public void setBallSpawner(MultiPlayerBallSpawner ballSpawner)
    {
        this.ballSpawner = ballSpawner;
    }

    public void spawnNewBall()
    {
        ballSpawner.spawnBall();
    }
}
