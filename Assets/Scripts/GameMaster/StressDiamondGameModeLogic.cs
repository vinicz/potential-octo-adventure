
using System;

public class StressDiamondGameModeLogic : GameModeLogic
{
    private DiamondMasterScript gameMaster;
    private LevelRecord level;
    
    public StressDiamondGameModeLogic(GameHandlerScript gameMaster)
    {
        this.gameMaster = (DiamondMasterScript) gameMaster;
        this.level = gameMaster.getCurrentLevelRecord();

		this.gameMaster.CollectedDiamondCountChanged += onDiamondCollected;
    }


    public void initGame()
    {
        gameMaster.setGameTimeLeft(level.timeToSecondReward);
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


