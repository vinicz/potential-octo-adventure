using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Linq;

public class FileLevelDataStorage : LevelDataStorage 
{

    public static FileLevelDataStorage storage;
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

        levelMap = new Dictionary<string,LevelRecord>();
        load();
        
    }


    public override void saveLevelList(List<LevelRecord> levels)
    {
        levelList = levels;
        save();
    }

    public override List<LevelRecord> loadLevelList()
    {
        load();
        return levelList;
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
            
            levelList = (List<LevelRecord>)bf.Deserialize(file);
            file.Close();
            
        } else
        {
            save();
        } 
    }

}

