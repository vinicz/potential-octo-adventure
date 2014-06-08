using System;

public class DiamondModeLogicFactory
{
    public static GameModeLogic createGameModeLogic(GameHandlerScript gameMaster, LevelRecord level)
    {
        GameModeLogic newGameModeLogic;

        switch (level.getLevelType())
        {   
            case LevelRecord.LevelType.NORMAL:
                newGameModeLogic = new NormalDiamondGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.STRESS:
                newGameModeLogic = new NormalDiamondGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.BOSS:
                newGameModeLogic = new NormalDiamondGameModeLogic(gameMaster);
                break;
            case LevelRecord.LevelType.STRESSBOSS:
                newGameModeLogic = new NormalDiamondGameModeLogic(gameMaster);
                break;
            default:
                newGameModeLogic = new NormalDiamondGameModeLogic(gameMaster);
                break;
        }

        return newGameModeLogic;
    }
}

