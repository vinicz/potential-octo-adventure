using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelService : MonoBehaviour
{

    public LevelDataStorage levelStorage;
    public ItemService itemService;
    public int levelListOffset = 1;
    public int worldPartSize = 15;
    public int stressLevelFrequency = 5;
    public int normalMapReward = 1;
    public int stressMapReward = 2;
    public int bossMapReward = 3;
    public int firstBossStepOffset = 0;
    public int bossStepOffset = 19;
    public List<LevelRecord> levelList;

    public void Awake()
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

    public IEnumerable<LevelRecord> getMultiplayerLevels()
    {
        return (from level in levelList where level.isMultiplayer == true select level);
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
        float twoRewardTreshold = currentLevel.timeToFirstReward;
        float threeRewardTreshold = currentLevel.timeToSecondReward;
        int collectedRewards = 1;

        if (elapsedTime <= twoRewardTreshold)
        {
            collectedRewards++;
        }
        if (elapsedTime <= threeRewardTreshold)
        {
            collectedRewards++;
        }

        if (collectedRewards > currentLevel.collectedRewards)
        {
            addNewRewardsToStorage(currentLevel,collectedRewards);
        }

        if (elapsedTime < currentLevel.bestTime || currentLevel.bestTime ==0)
        {
            currentLevel.bestTime = elapsedTime;
        }

        currentLevel.collectedRewards = collectedRewards;


        levelStorage.saveLevelList(levelList);
    }

    public int getMainMenuIndex()
    {
        return levelListOffset;
    }

    public int getNextLevelSceneIndex(int currentSceneIndex)
    {
        int nextLevelIndex = currentSceneIndex - levelListOffset + 1;
        int nextLevelSceneIndex = getMainMenuIndex();

        if (levelList.Count > nextLevelIndex)
        {
            LevelRecord nextLevel = levelList [nextLevelIndex];
            nextLevelSceneIndex = nextLevel.getLevelIndex();
            
            if (nextLevel.isMultiplayer)
            {
                nextLevelSceneIndex = getMainMenuIndex();
            }
        }

        return nextLevelSceneIndex;
    }

    private void addNewRewardsToStorage(LevelRecord currentLevel, int newCollectedRewards)
    {
        int newDiamonds = newCollectedRewards - currentLevel.collectedRewards;
        int rewardMultiplier = 1;
        switch (currentLevel.getLevelType())
        {
            case LevelRecord.LevelType.STRESS:
                rewardMultiplier = 2;
                break;
            case LevelRecord.LevelType.BOSS:
                rewardMultiplier = 3;
                break;
        }
        itemService.addRewards(newDiamonds * rewardMultiplier);
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
                    level.collectedRewards = persistedLevel.collectedRewards;
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
