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
    private int requiredDiamonds;
    public string levelName;
    public string levelGroup;
    public LevelType levelType = LevelType.NORMAL;
    public bool isMultiplayer;
    public int collectedDiamonds;
    public int allDiamonds;
    public float bestTime;
    public Vector2 timeToAward;
    public bool isLevelCompleted = false;

    public LevelRecord()
    {
    }
    
    public LevelRecord(int levelIndex, string levelName, string levelGroup, Vector2 timeToAward, bool isMultiplayer)
    {
        this.levelIndex = levelIndex;
        this.levelName = levelName;
        this.levelGroup = levelGroup;
        this.timeToAward = timeToAward;
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

    public int getRequiredDiamonds()
    {
        return requiredDiamonds;
    }
  
    public void setRequiredDiamonds(int reqDiamonds)
    {
        requiredDiamonds = reqDiamonds;
    }
}
