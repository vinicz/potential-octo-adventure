using UnityEngine;
using System;

[Serializable]
public class LevelRecord
{
    public enum LevelType
    {
        NORMAL,
        STRESS,
        BOSS,
        STRESSBOSS}
    ;


    private int levelIndex;
    private int requiredRewards;
    private LevelType levelType = LevelType.NORMAL;
    public string levelName;
    public string levelGroup;
    public bool isMultiplayer;
    public int collectedRewards;
    public float bestTime;
    public float timeToFirstReward = 40;
    public float timeToSecondReward = 20;
    public bool isLevelCompleted = false;

    public LevelRecord()
    {
    }
    
    public LevelRecord(int levelIndex, string levelName, string levelGroup, float timeToFirstAward, 
                       float timeToSecondAward, bool isMultiplayer)
    {
        this.levelIndex = levelIndex;
        this.levelName = levelName;
        this.levelGroup = levelGroup;
        this.timeToFirstReward = timeToFirstAward;
        this.timeToSecondReward = timeToSecondAward;
        this.isMultiplayer = isMultiplayer;
    }
 
    public void setLevelIndex(int index)
    {
        levelIndex = index;
    }

    public int getLevelIndex()
    {
        return levelIndex;
    }

    public int getRequiredRewards()
    {
        return requiredRewards;
    }
  
    public void setRequiredRewards(int reqDiamonds)
    {
        requiredRewards = reqDiamonds;
    }

    public void setLevelType(LevelType type)
    {
        levelType = type;
    }

    public LevelType getLevelType()
    {
        return levelType;
    }
}
