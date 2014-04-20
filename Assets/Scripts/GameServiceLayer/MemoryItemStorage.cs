using UnityEngine;
using System.Collections;

public class MemoryItemStorage : ItemStorage {

    int diamondCount = 0;
    int tokenCount = 0;

    public override void addDiamonds(int diamonds)
    {
        diamondCount += diamonds;
    }

    public override int getDiamondCount()
    {
        return diamondCount;
    }

    public override void addTokens(int tokens)
    {
        tokenCount += tokens;
    }

    public override int getTokenCount()
    {
        return tokenCount;
    }
}
