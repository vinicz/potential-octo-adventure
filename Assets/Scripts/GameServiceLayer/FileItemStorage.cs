using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class FileItemStorage : ItemStorage {
   
    [Serializable]
    private class PersistentFileItemStorageData
    {
        public int rewardCount = 0;
        public int tokenCount = 0;

        public PersistentFileItemStorageData(int rewardCount, int tokenCount)
        {
            this.rewardCount = rewardCount;
            this.tokenCount = tokenCount;
        }
        
    }

    public string file = "/ballthazar_item_storage.dat";
    private int rewardCount = 0;
    private int tokenCount = 0;
    private string fullFilePath;

    void Awake()
    {
        fullFilePath = Application.persistentDataPath + file;
        load();
    }
    
    public override void addRewards(int rewards)
    {
        rewardCount += rewards;
        save();
    }
    
    public override int getRewardCount()
    {
        return rewardCount;
    }
    
    public override void addTokens(int tokens)
    {
        tokenCount += tokens;
        save();
    }
    
    public override void removeTokens(int tokens)
    {
        tokenCount -= tokens;
        save();
    }
    
    
    public override int getTokenCount()
    {
        return tokenCount;
    }


    private void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(fullFilePath);
        PersistentFileItemStorageData data = new PersistentFileItemStorageData(rewardCount,tokenCount);

        bf.Serialize(file, data);
        file.Close();
    }
    
    private void load()
    {
        if (File.Exists(fullFilePath))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fullFilePath, FileMode.Open);
            
            PersistentFileItemStorageData data = (PersistentFileItemStorageData)bf.Deserialize(file);
            rewardCount = data.rewardCount;
            tokenCount = data.tokenCount;

            file.Close();
            
        } else
        {
            save();
        } 
    }
}
