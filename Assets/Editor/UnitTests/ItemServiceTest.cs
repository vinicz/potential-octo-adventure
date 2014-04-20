using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ItemServiceTest {
    const int STORED_TOKEN_COUNT = 7;
    const int STORED_DIAMOND_COUNT = 32;

    private ItemService testSubject;

    private MemoryItemStorage itemStorage;

    [SetUp]
    public void setUp()
    {
        setupItemStorage();

        setupItemsService();
    }

    [Test]
    public void shouldBeAbleToAddDiamons()
    {
        int diamondCount = 15;

        testSubject.addDiamonds(diamondCount);
        int newDiamondsCount = testSubject.getDiamondCount();

        Assert.AreEqual(STORED_DIAMOND_COUNT+15, newDiamondsCount);
    }

    [Test]
    public void shouldGetStoredDiamonds()
    {
        int diamondsCount = testSubject.getDiamondCount();
        
        Assert.AreEqual(STORED_DIAMOND_COUNT, diamondsCount);
    }

    [Test]
    public void shouldGetTokenAfterDiamonds()
    {
        testSubject.addDiamonds(50);

        Assert.AreEqual(STORED_TOKEN_COUNT+1, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGetTokenOnlyOneAfterDiamonds()
    {
        testSubject.addDiamonds(50);
        testSubject.addDiamonds(1);
        
        Assert.AreEqual(STORED_TOKEN_COUNT+1, testSubject.getTokenCount());
    }

    [Test]
    public void shouldGet10TokensAfterDiamonds()
    {
        testSubject.addDiamonds(500);
        
        Assert.AreEqual(STORED_TOKEN_COUNT+10, testSubject.getTokenCount());
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
        testSubject.addDiamonds(500);

        setupItemsService();
        
        Assert.AreEqual(STORED_TOKEN_COUNT+10, testSubject.getTokenCount());
        Assert.AreEqual(STORED_DIAMOND_COUNT + 500, testSubject.getDiamondCount());
    }



    void setupItemsService()
    {
        testSubject = new ItemService();
        testSubject.diamondsToTokenCount = 50;
        testSubject.itemStorage = itemStorage;
        testSubject.Start();
    }


    void setupItemStorage()
    {
        itemStorage = new MemoryItemStorage();
        itemStorage.addDiamonds(STORED_DIAMOND_COUNT);
        itemStorage.addTokens(STORED_TOKEN_COUNT);
    }
}
