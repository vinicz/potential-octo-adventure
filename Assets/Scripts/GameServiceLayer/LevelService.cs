using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelService : MonoBehaviour
{

    public LevelDataStorage levelStorage;
    public ItemService itemService;
    public List<LevelRecord> levelList;
    public int levelListOffset = 1;
    public int worldPartSize = 15;
    public int stressLevelFrequency = 5;
    public int normalMapReward = 1;
    public int stressMapReward = 2;
    public int bossMapReward = 3;
    public int firstBossStepOffset = 0;
    public int bossStepOffset = 19;

    public void Start()
    {
        List<LevelRecord> persistedList = levelStorage.loadLevelList();
        mergeLevelChanges(persistedList);

        setupLevelIndexes();
        setupLevelTypesAndRequirements();


        levelStorage.saveLevelList(levelList);
    }

    public IEnumerable<string> getSinglePlayerWorldNames()
    {
        return (from level in levelList where level.levelGroup != "" select level.levelGroup).Distinct();  
    }

    public IEnumerable<LevelRecord> getLevelsInWorld(string worldName)
    {
        return (from level in levelList where level.levelGroup == worldName select level);
    }

    public LevelRecord getLevelRecordForScene(int sceneIndex)
    {
        LevelRecord level;
        if (sceneIndex > 0 && sceneIndex < levelList.Count())
        {
            level = levelList [sceneIndex - levelListOffset];
        } else
        {
            level = new LevelRecord();
        }
        
        return level;
    }

    public void setLevelResult(int sceneIndex, float elapsedTime)
    {
        LevelRecord currentLevel = getLevelRecordForScene(sceneIndex);
        float twoDiamondTreshold = currentLevel.timeToAward.x;
        float threeDiamondTreshold = currentLevel.timeToAward.y;
        int collectedDiamonds = 1;

        if (elapsedTime <= twoDiamondTreshold)
        {
            collectedDiamonds++;
        }
        if (elapsedTime <= threeDiamondTreshold)
        {
            collectedDiamonds++;
        }

        if (collectedDiamonds > currentLevel.collectedDiamonds)
        {
            int newDiamonds = collectedDiamonds - currentLevel.collectedDiamonds;
            int rewardMultiplier = 1;

            switch (currentLevel.levelType)
            {
                case LevelRecord.LevelType.STRESS:
                    rewardMultiplier = 2;
                    break;
                case LevelRecord.LevelType.BOSS:
                    rewardMultiplier = 3;
                    break;
            }

            itemService.addDiamonds(newDiamonds*rewardMultiplier);
        }

        currentLevel.collectedDiamonds = collectedDiamonds;

        levelStorage.saveLevelList(levelList);
    }

    private void mergeLevelChanges(List<LevelRecord> persistedList)
    {
        foreach (LevelRecord level in levelList)
        {
            foreach (LevelRecord persistedLevel in persistedList)
            {
                if (level.levelName == persistedLevel.levelName)
                {
                    level.bestTime = persistedLevel.bestTime;
                    level.isLevelCompleted = persistedLevel.isLevelCompleted;
                    level.collectedDiamonds = persistedLevel.collectedDiamonds;
                }
            }
        } 
    }

    private void setupLevelIndexes()
    {
        int index = levelListOffset;
        foreach (var level in levelList)
        {
            level.setLevelIndex(index);
            index++;
        }
    }

    private void setupLevelTypesAndRequirements()
    {
        LevelingRulesGenerator levelingRulesGenerator = new LevelingRulesGenerator();
        levelingRulesGenerator.BossMapReward = bossMapReward;
        levelingRulesGenerator.BossStepOffset = bossStepOffset;
        levelingRulesGenerator.FirstBossStepOffset = firstBossStepOffset;
        levelingRulesGenerator.NormalMapReward = normalMapReward;
        levelingRulesGenerator.StressLevelFrequency = stressLevelFrequency;
        levelingRulesGenerator.StressMapReward = stressMapReward;
        levelingRulesGenerator.WorldPartSize = worldPartSize;

        levelingRulesGenerator.genarateRulesForLevelService(this);
    }
   
}
