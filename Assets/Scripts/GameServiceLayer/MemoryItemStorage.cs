using UnityEngine;
using System.Collections;

public class MemoryItemStorage : ItemStorage {

    int diamondCount = 0;

    public override void addDiamonds(int diamonds)
    {
        diamondCount += diamonds;
    }

    public override int getDiamondCount()
    {
        return diamondCount;
    }
}
