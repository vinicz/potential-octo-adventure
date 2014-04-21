using UnityEngine;
using System.Collections;

public class MemoryItemStorage : ItemStorage {

    int rewardCount = 0;
    int tokenCount = 0;

    public override void addRewards(int rewards)
    {
        rewardCount += rewards;
    }

    public override int getRewardCount()
    {
        return rewardCount;
    }

    public override void addTokens(int tokens)
    {
        tokenCount += tokens;
    }

    public override void removeTokens(int tokens)
    {
        tokenCount -= tokens;
    }


    public override int getTokenCount()
    {
        return tokenCount;
    }
}
