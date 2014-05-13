
using System;

public class StressGameModeLogic : GameModeLogic
{
    private GameHandlerScript gameMaster;
    private LevelRecord level;
    
    public StressGameModeLogic(GameHandlerScript gameMaster)
    {
        this.gameMaster = gameMaster;
        this.level = gameMaster.getCurrentLevelRecord();

        gameMaster.setGameTimeLeft(level.timeToSecondReward);
        gameMaster.CollectedDiamondCountChanged += onDiamondCollected;
    }

    public void update()
    {
        if (gameMaster.getGameTimeLeft() <= 0 || gameMaster.getCollectedDiamonds() >= 3)
        {
            gameMaster.setGameState(GameHandlerScript.GameState.POSTGAME);
        }

    }
    
    void onDiamondCollected()
    {
        float currentGameTimeLeft = gameMaster.getGameTimeLeft();
        gameMaster.setGameTimeLeft(currentGameTimeLeft+level.timeToSecondReward);
    }


    public void determineGameResult()
    {
        if (gameMaster.getBallCount()<=0 || gameMaster.getCollectedDiamonds()<=0)
        {
            gameMaster.levelFailed();
            
        } else
        {
            gameMaster.levelPassed();
        }
    }


    public int calculateReward()
    {
        int collectedRewardCount = gameMaster.getCollectedDiamonds();

        return collectedRewardCount;
    }
}


