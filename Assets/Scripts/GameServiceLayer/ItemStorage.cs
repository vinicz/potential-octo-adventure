using UnityEngine;
using System.Collections;

public abstract class ItemStorage : MonoBehaviour {

    public abstract void addDiamonds(int diamondCount);
    public abstract int getDiamondCount();
}
