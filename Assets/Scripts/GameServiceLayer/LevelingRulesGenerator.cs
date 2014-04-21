using System;
using System.Collections;
using System.Collections.Generic;

public class LevelingRulesGenerator
{
    private int worldPartSize;
    private int stressLevelFrequency;
    private int normalMapReward;
    private int stressMapReward;
    private int bossMapReward;
    private int firstBossStepOffset;
    private int bossStepOffset;

    private int levelRequirement;
    private bool firstBoss;
    private int mediumLevelResults;
    private int levelTypeCounter;


    public void genarateRulesForLevelService(LevelService levelService)
    {
        IEnumerable<string> worldNames = levelService.getSinglePlayerWorldNames();
        levelRequirement = 0;
        firstBoss = true;
        mediumLevelResults = 0;
  
        foreach (string worldName in worldNames)
        {
            IEnumerable<LevelRecord> levelsInWorld = levelService.getLevelsInWorld(worldName);
            levelTypeCounter = 1;
            
            foreach (LevelRecord level in levelsInWorld)
            {
                if(levelTypeCounter==1 || level.isMultiplayer)
                {
                    level.setRequiredRewards(0);
                }
                else
                {
                    level.setRequiredRewards(levelRequirement);
                }
                
                if (levelTypeCounter == worldPartSize)
                {
                    generateRulesForBossLevel(level);
                      
                } 
                else if (levelTypeCounter % stressLevelFrequency == 0)
                {
                    generateRulesForStressLevel(level);
                    
                } else
                {
                    generateRulesForNormalLevel();
                    
                }
            }
        }
    }

    private void generateRulesForBossLevel(LevelRecord level)
    {
        level.setLevelType(LevelRecord.LevelType.BOSS);
        levelTypeCounter = 1;
        mediumLevelResults += 2 * bossMapReward;

        if (!firstBoss)
        {
            mediumLevelResults += bossStepOffset;
        }
        else
        {
            firstBoss = false;
            mediumLevelResults += firstBossStepOffset;
        }

        levelRequirement = mediumLevelResults;
    }

    private void generateRulesForStressLevel(LevelRecord level)
    {
        level.setLevelType(LevelRecord.LevelType.STRESS);
        levelTypeCounter++;
        levelRequirement += 2 * stressMapReward;
        mediumLevelResults += 2 * stressMapReward;
    }

    void generateRulesForNormalLevel()
    {
        levelTypeCounter++;
        levelRequirement += normalMapReward;
        mediumLevelResults += 2 * normalMapReward;
    }

    public int WorldPartSize {
        get {
            return this.worldPartSize;
        }
        set {
            worldPartSize = value;
        }
    }

    public int StressLevelFrequency {
        get {
            return this.stressLevelFrequency;
        }
        set {
            stressLevelFrequency = value;
        }
    }

    public int NormalMapReward {
        get {
            return this.normalMapReward;
        }
        set {
            normalMapReward = value;
        }
    }

    public int StressMapReward {
        get {
            return this.stressMapReward;
        }
        set {
            stressMapReward = value;
        }
    }

    public int BossMapReward {
        get {
            return this.bossMapReward;
        }
        set {
            bossMapReward = value;
        }
    }

    public int FirstBossStepOffset {
        get {
            return this.firstBossStepOffset;
        }
        set {
            firstBossStepOffset = value;
        }
    }

    public int BossStepOffset {
        get {
            return this.bossStepOffset;
        }
        set {
            bossStepOffset = value;
        }
    }
}