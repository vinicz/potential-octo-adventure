using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class ItemServiceTest {

    private ItemService testSubject;

    private MemoryItemStorage itemStorage;

    [SetUp]
    public void setUp()
    {
        setupItemStorage();

        testSubject = new ItemService();
        testSubject.itemStorage = itemStorage;
        testSubject.Start();


    }

    [Test]
    public void shouldBeAbleToAddDiamons()
    {
        int diamondCount = 15;

        testSubject.addDiamonds(diamondCount);
        int newDiamondsCount = testSubject.getDiamondCount();

        Assert.AreEqual(47, newDiamondsCount);
    }

    [Test]
    public void shouldGetStoredDiamonds()
    {
        int diamondsCount = testSubject.getDiamondCount();
        
        Assert.AreEqual(32, diamondsCount);
    }



    void setupItemStorage()
    {
        itemStorage = new MemoryItemStorage();
        itemStorage.addDiamonds(32);
    }
}
