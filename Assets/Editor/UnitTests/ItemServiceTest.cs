using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class ItemServiceTest
{
    const int STORED_TOKEN_COUNT = 7;
    const int STORED_REWARD_COUNT = 32;
    private ItemService testSubject;
    private MemoryItemStorage itemStorage;
    private MockIAPService mockIAPService;

    [SetUp]
    public void setUp()
    {
        setupItemStorage();
        setupIAPService();

        setupItemsService();
    }

    [Test]
    public void shouldBeAbleToAddDiamons()
    {
        int diamondCount = 15;

        testSubject.addRewards(diamondCount);
        int newDiamondsCount = testSubject.getRewardCount();

        Assert.AreEqual(STORED_REWARD_COUNT + 15, newDiamondsCount);
    }

    [Test]
    public void shouldGetStoredDiamonds()
    {
        int diamondsCount = testSubject.getRewardCount();
        
        Assert.AreEqual(STORED_REWARD_COUNT, diamondsCount);
    }

    [Test]
    public void shouldGetTokenAfterDiamonds()
    {
        testSubject.addRewards(50);

        Assert.AreEqual(STORED_TOKEN_COUNT + 1, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGetTokenOnlyOneAfterDiamonds()
    {
        testSubject.addRewards(50);
        testSubject.addRewards(1);
        
        Assert.AreEqual(STORED_TOKEN_COUNT + 1, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGet10TokensAfterDiamonds()
    {
        testSubject.addRewards(500);
        
        Assert.AreEqual(STORED_TOKEN_COUNT + 10, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGetStoredTokens()
    {
        int tokensCount = testSubject.getTokenCount();
        
        Assert.AreEqual(STORED_TOKEN_COUNT, tokensCount);
    }

    [Test]
    public void shouldSaveDiamondAndTokenData()
    {
        testSubject.addRewards(500);

        setupItemsService();
        
        Assert.AreEqual(STORED_TOKEN_COUNT + 10, testSubject.getTokenCount());
        Assert.AreEqual(STORED_REWARD_COUNT + 500, testSubject.getRewardCount());
    }

    [Test]
    public void shouldBeAbleToSpendTokens()
    {
        int spentTokenCount = 5;

        testSubject.spendTokens(spentTokenCount);

        Assert.AreEqual(STORED_TOKEN_COUNT - 5, testSubject.getTokenCount());
    }

    [Test]
    [ExpectedException(typeof(NotEnoughTokensException))]
    public void shouldNotBeAbleToOverSpendTokens()
    {
        int spentTokenCount = 10;
        
        testSubject.spendTokens(spentTokenCount);
    }

    [Test]
    public void shouldSaveSpentTokens()
    {
        int spentTokenCount = 5;

        testSubject.spendTokens(spentTokenCount);

        setupItemsService();

        Assert.AreEqual(STORED_TOKEN_COUNT - 5, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGetAllIAPProdcuts()
    {
        List<IAPProduct> products = testSubject.getIAPProducts();
        
        Assert.AreEqual(7, products.Count);
        Assert.AreEqual(30, products [3].amount);
    }

    [Test]
    public void shouldBeAbleToBuyTokens()
    {
        string productId = "item_id1";

        testSubject.buyIAPProduct(productId);

        Assert.AreEqual(STORED_TOKEN_COUNT + 10, testSubject.getTokenCount());
    }

    [Test]
    public void shouldBeAbleToBuyRewardsForTokens()
    {
        string productId = "TOKEN_TO_REVARD";
        
        testSubject.buyIAPProduct(productId);
        
        Assert.AreEqual(STORED_TOKEN_COUNT - 2, testSubject.getTokenCount());
        Assert.AreEqual(STORED_REWARD_COUNT + 20, testSubject.getRewardCount());
    }

    [Test]
    [ExpectedException(typeof(NotEnoughTokensException))]
    public void shouldNotBeAbleToOverBuyRewardsForTokens()
    {
        string productId = "MUCH_TOKEN_TO_REVARD";
        
        testSubject.buyIAPProduct(productId);
        
    }

    void setupItemsService()
    {
        testSubject = new ItemService();
        testSubject.rewardsToTokenCount = 50;
        testSubject.itemStorage = itemStorage;
        testSubject.iapService = mockIAPService;
        testSubject.Start();
    }

    void setupItemStorage()
    {
        itemStorage = new MemoryItemStorage();
        itemStorage.addRewards(STORED_REWARD_COUNT);
        itemStorage.addTokens(STORED_TOKEN_COUNT);
    }

    void setupIAPService()
    {
        mockIAPService = new MockIAPService();
        mockIAPService.itemStorage = itemStorage;
        mockIAPService.productList = new List<IAPProduct>();

        for (int i=0; i<5; i++)
        {
            mockIAPService.productList.Add(
                new IAPProduct("product" + i,
                           "desc" + i,
                           "item_id" + i,
                           i * 10,
                           i,
                           IAPProduct.ProductType.MONEY,
                           IAPProduct.ProductType.TOKEN));
        }

        mockIAPService.productList.Add(
            new IAPProduct("reward",
                       "desc",
                       "TOKEN_TO_REVARD",
                       20,
                       2,
                       IAPProduct.ProductType.TOKEN,
                       IAPProduct.ProductType.REWARD));

        mockIAPService.productList.Add(
            new IAPProduct("much_reward",
                       "desc",
                       "MUCH_TOKEN_TO_REVARD",
                       2350,
                       200,
                       IAPProduct.ProductType.TOKEN,
                       IAPProduct.ProductType.REWARD));

        mockIAPService.Start();
    }
}
