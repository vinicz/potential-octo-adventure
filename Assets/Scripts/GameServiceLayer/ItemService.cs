using UnityEngine;
using System.Collections;

public class ItemService : MonoBehaviour
{

    public ItemStorage itemStorage;
    public int diamondsToTokenCount = 50;
    private int diamondCount;
    private int tokenCount;

    public void Start()
    {  
        diamondCount = itemStorage.getDiamondCount();
        tokenCount = itemStorage.getTokenCount();
    }

    public void addDiamonds(int diamonds)
    {
        int newDiamondCount = diamondCount + diamonds;
        int addedTokenCount = newDiamondCount / diamondsToTokenCount - diamondCount / diamondsToTokenCount;

        tokenCount += addedTokenCount;
        diamondCount = newDiamondCount;

        itemStorage.addDiamonds(diamonds);
        itemStorage.addTokens(addedTokenCount);
    }

    public int getDiamondCount()
    {
        return diamondCount;
    }

    public int getTokenCount()
    {
        return tokenCount;
    }
}
