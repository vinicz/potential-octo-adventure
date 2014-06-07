using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemService : MonoBehaviour
{
    public delegate void ItemCountChangedHandler();
    public event ItemCountChangedHandler ItemCountChanged;

	public delegate void PurchaseCompletedHandler();
	public event PurchaseCompletedHandler PurchaseCompleted;

	public delegate void PurchaseFailedHandler();
	public event PurchaseFailedHandler PurchaseFailed;

    public ItemStorage itemStorage;
    public IAPService iapService;
    public int rewardsToTokenCount = 50;
    private int rewardCount;
    private int tokenCount;

    public void Awake()
    {  
        refreshItems();
        iapService.PurchaseCompletedSuccesfully += purchaseCompletedSuccesfully;
        iapService.PurchaseFailed += purchaseFailed;
    }

    void Start()
    {
        refreshItems();
    }

    void OnDestroy()
    {
        iapService.PurchaseCompletedSuccesfully -= purchaseCompletedSuccesfully;
        iapService.PurchaseFailed -= purchaseFailed;
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

	public IEnumerable<IAPProduct> getIAPProductsOfType(IAPProduct.ProductType productype)
	{
		return iapService.getProductOfType (productype);
	}

    void purchaseCompletedSuccesfully()
    {
        refreshItems();

		if (PurchaseCompleted != null)
		{
			PurchaseCompleted();
		}
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

        if (ItemCountChanged != null)
        {
            ItemCountChanged();
        }
    }
}
