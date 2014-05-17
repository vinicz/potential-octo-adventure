public class NormalGameModeLogic : GameModeLogic       
{
   
    private GameHandlerScript gameMaster;
    private LevelRecord level;
    private bool twoRewardsAvailable = true;

    public NormalGameModeLogic(GameHandlerScript gameMaster)
    {
        this.gameMaster = gameMaster;
        this.level = gameMaster.getCurrentLevelRecord();

        gameMaster.setGameTimeLeft(level.timeToSecondReward);
    }

    public void initGame()
    {

    }


    public void update()
    {

        if (gameMaster.getEnemyCount() <= 0 && gameMaster.getCollectedDiamonds() >= gameMaster.getRequiredDiamondCount())
        {
            gameMaster.setGameState(GameHandlerScript.GameState.POSTGAME);
        }

        if (gameMaster.getGameTimeLeft() <= 0 && twoRewardsAvailable)
        {
            gameMaster.setGameTimeLeft(level.timeToFirstReward-gameMaster.getElapsedTime());
            twoRewardsAvailable = false;
        }
       
    }

    public void determineGameResult()
    {
        if (gameMaster.getCollectedDiamonds() == gameMaster.getRequiredDiamondCount())
        {
            gameMaster.levelPassed();
            
        } else
        {
            gameMaster.levelFailed();
        }
    }


    public int calculateReward()
    {
        float twoRewardTreshold = level.timeToFirstReward;
        float threeRewardTreshold = level.timeToSecondReward;
        int collectedRewards = 1;
        float elapsedTime = gameMaster.getElapsedTime();
        
        if (elapsedTime <= twoRewardTreshold)
        {
            collectedRewards++;
        }
        if (elapsedTime <= threeRewardTreshold)
        {
            collectedRewards++;
        }

        return collectedRewards;
    }

}

