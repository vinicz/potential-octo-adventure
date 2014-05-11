using System;

public class GameModeLogicFactory
{
    public static GameModeLogic createGameModeLogic(GameHandlerScript gameMaster, LevelRecord level)
    {
        GameModeLogic newGameModeLogic;

        switch (level.getLevelType())
        {   
            case LevelRecord.LevelType.NORMAL:
                newGameModeLogic = new NormalGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.STRESS:
                newGameModeLogic = new StressGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.BOSS:
                newGameModeLogic = new NormalGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.STRESSBOSS:
                newGameModeLogic = new NormalGameModeLogic(gameMaster);
                break;
            default:
                newGameModeLogic = new NormalGameModeLogic(gameMaster);
                break;
        }

        return newGameModeLogic;
    }
}

