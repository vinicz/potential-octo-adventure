using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemService : MonoBehaviour
{
    public ItemStorage itemStorage;
    public IAPService iapService;
    public int rewardsToTokenCount = 50;
    private int rewardCount;
    private int tokenCount;

    public void Start()
    {  
        refreshItems();
        iapService.PurchaseCompletedSuccesfully += purchaseCompletedSuccesfully;
        iapService.PurchaseFailed += purchaseFailed;;
    }

    public void addRewards(int rewards)
    {
        int newRewardCount = rewardCount + rewards;
        int addedTokenCount = newRewardCount / rewardsToTokenCount - rewardCount / rewardsToTokenCount;

        tokenCount += addedTokenCount;
        rewardCount = newRewardCount;

        itemStorage.addRewards(rewards);
        itemStorage.addTokens(addedTokenCount);
    }

    public int getRewardCount()
    {
        return rewardCount;
    }

    public int getTokenCount()
    {
        return tokenCount;
    }

    public void spendTokens(int spentTokenCount)
    {
        if (tokenCount >= spentTokenCount)
        {
            itemStorage.removeTokens(spentTokenCount);
            refreshItems();

        } else
        {
            throw new NotEnoughTokensException();
        }
    }

    public void buyIAPProduct(string productId)
    {
        iapService.buyProduct(productId);
    }

    public List<IAPProduct> getIAPProducts()
    {
        return iapService.getProducts();
    }

    void purchaseCompletedSuccesfully()
    {
        refreshItems();
    }

    void purchaseFailed()
    {
        refreshItems();
        throw new NotEnoughTokensException();
    }

    void refreshItems()
    {
        rewardCount = itemStorage.getRewardCount();
        tokenCount = itemStorage.getTokenCount();
    }
}
