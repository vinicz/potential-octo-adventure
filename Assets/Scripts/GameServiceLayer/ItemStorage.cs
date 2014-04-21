using UnityEngine;
using System.Collections;

public abstract class ItemStorage : MonoBehaviour {

    public abstract void addRewards(int rewardCount);
    public abstract int getRewardCount();
    public abstract void addTokens(int tokenCount);
    public abstract void removeTokens(int spentTokenCount);
    public abstract int getTokenCount();
}
