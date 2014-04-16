using UnityEngine;
using System.Collections;

public class ItemService : MonoBehaviour {

    public ItemStorage itemStorage;

    private int diamondCount;

	public void Start () {
	    
        diamondCount = itemStorage.getDiamondCount();
	}

    public void addDiamonds(int diamonds)
    {
        diamondCount+= diamonds;
    }

    public int getDiamondCount()
    {
        return diamondCount;
    }
}
