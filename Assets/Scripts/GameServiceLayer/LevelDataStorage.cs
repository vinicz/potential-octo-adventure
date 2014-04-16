using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LevelDataStorage : MonoBehaviour  {

    public abstract void saveLevelList(List<LevelRecord> levelList);
    public abstract List<LevelRecord> loadLevelList();
   
}
