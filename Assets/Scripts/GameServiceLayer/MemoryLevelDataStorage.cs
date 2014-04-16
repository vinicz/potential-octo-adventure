using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryLevelDataStorage : LevelDataStorage
{

    public List<LevelRecord> levelList;

    public override void saveLevelList(List<LevelRecord> levels)
    {
        levelList = new List<LevelRecord>();

        foreach (var level in levels)
        {
            LevelRecord newLevelRecord = new LevelRecord(level.getLevelIndex(), level.levelName, level.levelGroup, 
                                                         level.timeToAward, level.isMultiplayer);
            newLevelRecord.allDiamonds = level.allDiamonds;
            newLevelRecord.bestTime = level.bestTime;
            newLevelRecord.collectedDiamonds = level.collectedDiamonds;
            newLevelRecord.isLevelCompleted = level.isLevelCompleted;
            levelList.Add(newLevelRecord);
        }

    }
    
    public override List<LevelRecord> loadLevelList()
    {
        return levelList;
    }
}
