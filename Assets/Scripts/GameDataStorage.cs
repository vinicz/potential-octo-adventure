using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;

public class GameDataStorage : MonoBehaviour
{

    public static GameDataStorage storage;
    public string file = "/ballthazar.dat";
    public List<LevelRecord> levelList;
    private Dictionary<string, LevelRecord> levelMap;
    private string fullFilePath;

    void Awake()
    {
        if (storage == null)
        {
            DontDestroyOnLoad(gameObject);
            storage = this;
        } else if (storage != this)
        {
            Destroy(gameObject);
        }
        fullFilePath = Application.persistentDataPath + file;

    }

    void Start()
    {

        levelMap = new Dictionary<string,LevelRecord>();
        load();
    }

    public void setLevelRecord(string levelName, float bestTime)
    {
        LevelRecord level = levelMap [levelName];

        if (level.bestTime > bestTime)
        {
            level.bestTime = bestTime;
           
        }

        level.isLevelCompleted = true;

        save();
    }

    public int getLevelIndex(string levelName)
    {
        return levelMap [levelName].levelIndex;
    }

    public IEnumerable<LevelRecord> getMultiplayerLevels()
    {
        return (from level in levelList where level.isMultiplayer == true select level);

    }

    public IEnumerable<string> getLevelGroups()
    {
        return (from level in levelList where level.levelGroup != "" select level.levelGroup).Distinct();  
    }

    public IEnumerable<LevelRecord> getLevelsInGroup(string levelGroup)
    {
        return (from level in levelList where level.levelGroup == levelGroup orderby level.levelOrder ascending select level );   
    }

    public void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(fullFilePath);

        bf.Serialize(file, levelList);
        file.Close();
    }
    
    public void load()
    {
        if (File.Exists(fullFilePath))
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullFilePath, FileMode.Open);

            List<LevelRecord> persistedList = (List<LevelRecord>)bf.Deserialize(file);
            file.Close();

            mergeLevelChanges(persistedList);


           

        } else
        {
            save();
        }
        levelMap.Clear();

        foreach (var level in levelList)
        {
            levelMap.Add(level.levelName, level);
        }

    }

    void mergeLevelChanges( List<LevelRecord> persistedList)
    {
        foreach (LevelRecord level in levelList)
        {
            foreach (LevelRecord persistedLevel in persistedList)
            {
                if (level.levelName == persistedLevel.levelName)
                {
                    level.bestTime = persistedLevel.bestTime;
                    level.isLevelCompleted = persistedLevel.isLevelCompleted;
                }
            }
        }
        save();
      
    }
}

[Serializable]
public class LevelRecord
{
    public int levelIndex;
    public int levelOrder;
    public string levelName;
    public string levelGroup;
    public bool isMultiplayer;
    public float bestTime;
    public float timeToAward;
    public bool isLevelCompleted;

}
