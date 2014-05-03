using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class LevelServiceTest
{

    private LevelService testSubject;
    private MemoryLevelDataStorage levelStorage;
    private ItemService itemService;
    private int levelListOffset = 1;

    [SetUp]
    public void setUp()
    {
        setupLevelDataStorage();
        setupItemsService();

        setupLevelService();
        wireAndStartLevelService();

       
    }

    [Test]
    public void shouldReturnOneWorldLevels()
    {
        var testWorldName = "world0";
        IEnumerable<LevelRecord> levelsInTestWorld = testSubject.getLevelsInWorld(testWorldName);

        foreach (var level in levelsInTestWorld)
        {
            Assert.AreEqual(testWorldName, level.levelGroup);
        }
    }

    [Test]
    public void shouldFillPersistentDataToNewData()
    {
        var testWorldName = "world1";
        IEnumerable<LevelRecord> levelsInTestWorld = testSubject.getLevelsInWorld(testWorldName);

        foreach (var level in levelsInTestWorld)
        {
            Assert.AreEqual(1234, level.collectedRewards);
            Assert.AreEqual(30f, level.timeToFirstReward);
        }
    }

    [Test]
    public void shouldHandleAddedLevel()
    {
        var testWorldName = "world0";
        IEnumerable<LevelRecord> levelsInTestWorld = testSubject.getLevelsInWorld(testWorldName);

        int levelIndex = levelListOffset;
        foreach (var level in levelsInTestWorld)
        {
            Assert.AreEqual(levelIndex, level.getLevelIndex());
            levelIndex++;
        }
    }

    [Test]
    public void shouldGetLevelRecordForScene()
    {
        int sceneIndex = 3;

        LevelRecord testLevel = testSubject.getLevelRecordForScene(sceneIndex);

        Assert.AreEqual("test02", testLevel.levelName);

    }

    [Test]
    public void shouldGetOneDiamond()
    {
        int sceneIndex = 3;
        float elapsedTime = 31f;
        int expectedDiamondCount = 1;


        setResultCheckCollectedDiamonds(sceneIndex, elapsedTime, expectedDiamondCount);
    }

    [Test]
    public void shouldGetTwoDiamond()
    {
        int sceneIndex = 3;
        float elapsedTime = 30f;
        int expectedDiamondCount = 2;

        setResultCheckCollectedDiamonds(sceneIndex, elapsedTime, expectedDiamondCount);
    }

    [Test]
    public void shouldGetThreeDiamond()
    {
        int sceneIndex = 3;
        float elapsedTime = 20f;
        int expectedDiamondCount = 3;
        
        setResultCheckCollectedDiamonds(sceneIndex, elapsedTime, expectedDiamondCount);
    }

    [Test]
    public void shouldReturnWorlds()
    {
        List<string> expectedWorldNames = new List<string>();
        expectedWorldNames.Add("world0");
        expectedWorldNames.Add("world1");
        expectedWorldNames.Add("world2");
        expectedWorldNames.Add("world3");

        IEnumerable<string> worldNameList = testSubject.getSinglePlayerWorldNames();

        int worldIndex = 0;
        foreach (string world in worldNameList)
        {
            Assert.AreEqual(expectedWorldNames [worldIndex], world);
            worldIndex++;
        }
    }
    [Test]
    public void shouldGetMultiplayerLevels()
    {
        IEnumerable<LevelRecord> multiplayerLevels = testSubject.getMultiplayerLevels();

        int levelCount = 0;

        foreach (LevelRecord level in multiplayerLevels)
        {
            Assert.IsTrue(level.isMultiplayer);
            levelCount++;
        }

        Assert.AreEqual(90, levelCount);
    }
   

    [Test]
    public void shouldGetAllLevelTypes()
    {
        int testLevelOffset = 5;

        int bossLevelCounter = 1;
        for (int sceneIndex=1; sceneIndex<270; sceneIndex++)
        {

            if (sceneIndex % 5 == 0)
            {
                if (bossLevelCounter == 3)
                {
                    LevelRecord currentLevel = testSubject.getLevelRecordForScene(sceneIndex);
                    bossLevelCounter = 1;

                    Assert.AreEqual(LevelRecord.LevelType.BOSS, currentLevel.getLevelType());
                } else
                {
                    LevelRecord currentLevel = testSubject.getLevelRecordForScene(sceneIndex);

                    Assert.AreEqual(LevelRecord.LevelType.STRESS, currentLevel.getLevelType());
                    bossLevelCounter++;
                }
            } else
            {
                LevelRecord currentLevel = testSubject.getLevelRecordForScene(sceneIndex);

                Assert.AreEqual(LevelRecord.LevelType.NORMAL, currentLevel.getLevelType());
            }
        }
    }

    [Test]
    public void shouldRequireSingeDiamondSteps()
    {
        checkLevelReqirement(4, 3);
    }

    [Test]
    public void shouldRequireStressLevelStep()
    {
        checkLevelReqirement(6, 8);
    }

    [Test]
    public void shouldRequireFirstBossLevelStep()
    {
        checkLevelReqirement(17, 39);
    }

    [Test]
    public void shouldRequireBossLevelStep()
    {
        checkLevelReqirement(32, 96);
    }

    [Test]
    public void shouldEveryFirstLevelOfPartFree()
    {
        checkLevelReqirement(46, 0);
    }

    [Test]
    public void shouldSecondWorldsFirstLevelFree()
    {
        checkLevelReqirement(91, 0);
    }

    [Test]
    public void shouldSecondWorldsSecondLevelMoreThanFirstWorld()
    {
        checkLevelReqirement(92, 324);
    }

    [Test]
    public void shouldMultiplayerLevelBeFree()
    {
        checkLevelReqirement(287, 0);
    }

    [Test]
    public void shouldAddNewDiamondsToItemStorage()
    {
        checkLevelReward(1, 1, 3); 
    }

    [Test]
    public void shouldAddNewStressDiamondsToItemStorage()
    {
        checkLevelReward(5, 1, 6);    
    }

    [Test]
    public void shouldAddNewBossDiamondsToItemStorage()
    {
        checkLevelReward(15, 1, 9);    
    }

    [Test]
    public void shouldSaveResultsToLevelStorage()
    {
        testSubject.setLevelResult(1, 1);
        resetLevelService();
        LevelRecord level = testSubject.getLevelRecordForScene(1);

        Assert.AreEqual(3, level.collectedRewards);
    }

    [Test]
    public void shouldReturnMainMenuIndex()
    {
        Assert.AreEqual(levelListOffset-1, testSubject.getMainMenuIndex());
    }

    [Test]
    public void shouldReturnNextLevel()
    {
        int currentSceneIndex = 6;

        Assert.AreEqual(7, testSubject.getNextLevelSceneIndex(currentSceneIndex));
    }

    [Test]
    public void shouldNotForwardToMultiplayerLevel()
    {
        int currentSceneIndex = 270;
        
        Assert.AreEqual(testSubject.getMainMenuIndex(), testSubject.getNextLevelSceneIndex(currentSceneIndex));
    }

    [Test]
    public void shouldForwardToMainMenuAferLastLevel()
    {
        int currentSceneIndex = 360;
        
        Assert.AreEqual(testSubject.getMainMenuIndex(), testSubject.getNextLevelSceneIndex(currentSceneIndex));
    }




    void setupLevelService()
    {
        testSubject = new LevelService();

        List<LevelRecord> levelRecordList = new List<LevelRecord>();

        LevelRecord testLevelRecordWorld3;
        levelRecordList.AddRange(generateCompleteWorld(0, 0,false));
        levelRecordList.AddRange(generateCompleteWorld(1, 0,false));
        levelRecordList.AddRange(generateCompleteWorld(2, 0,false));
        levelRecordList.AddRange(generateCompleteWorld(3, 0,true));

        testSubject.levelList = levelRecordList;
        testSubject.levelListOffset = levelListOffset;
    }

    void resetLevelService()
    {
        setupLevelService();
        wireAndStartLevelService();
    }

    void setupLevelDataStorage()
    {
        levelStorage = new MemoryLevelDataStorage();

        List<LevelRecord> levelRecordList = new List<LevelRecord>();

        levelRecordList.AddRange(generateCompleteWorld(1, 1234,false));
        
        levelStorage.levelList = levelRecordList;
    }

    void setupItemsService()
    {
        itemService = new ItemService();
        itemService.rewardsToTokenCount = 50;
        itemService.itemStorage = new MemoryItemStorage();
        itemService.iapService = new MockIAPService();
        itemService.Awake();
    }

    void wireAndStartLevelService()
    {
        testSubject.levelStorage = levelStorage;
        testSubject.itemService = itemService;
        testSubject.Start();
    }

    void setResultCheckCollectedDiamonds(int sceneIndex, float elapsedTime, int expectedDiamondCount)
    {
        testSubject.setLevelResult(sceneIndex, elapsedTime);
        LevelRecord testLevel = testSubject.getLevelRecordForScene(sceneIndex);
        Assert.AreEqual(expectedDiamondCount, testLevel.collectedRewards);
        Assert.AreEqual(elapsedTime, testLevel.bestTime);
    }

    List<LevelRecord>  generateCompleteWorld(int worldId, int collectedDiamonds, bool isMultiplayer)
    {
        List<LevelRecord> levelRecordList = new List<LevelRecord>();

        for (int i = 0; i < 90; i++)
        {
            LevelRecord testLevelRecordWorld = new LevelRecord(Random.Range(0, 120), "test" + worldId + i, "world" + worldId,
                                                               30f, 20f, isMultiplayer);
            testLevelRecordWorld.collectedRewards = collectedDiamonds;
            levelRecordList.Add(testLevelRecordWorld);
        }
        return levelRecordList;
    }

    void checkLevelReqirement(int sceneIndex, int expectedRequirement)
    {
        LevelRecord currentLevel = testSubject.getLevelRecordForScene(sceneIndex);
        Assert.AreEqual(expectedRequirement, currentLevel.getRequiredRewards());
    }

    void checkLevelReward(int scheneIndex, int gameResult, int expectedDiamondReward)
    {
        testSubject.setLevelResult(scheneIndex, gameResult);
        int newDiamondCount = itemService.getRewardCount();
        Assert.AreEqual(expectedDiamondReward, newDiamondCount);
    }
}
