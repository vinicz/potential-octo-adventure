using UnityEngine;
using System.Collections;

public class DiamondWindow : MonoBehaviour
{

    public UILabel diamondLabel;
    private bool inited = false;

    void Update()
    {
        if (!inited)
        {
            updateDiamondLabel();
            
            GameServiceLayer.serviceLayer.gameMaster.CollectedDiamondCountChanged += updateDiamondLabel;
        }
    }

    void updateDiamondLabel()
    {
        string newDiamondLabel = "";

        newDiamondLabel += GameServiceLayer.serviceLayer.gameMaster.getCollectedDiamonds().ToString();
        newDiamondLabel += "/";
        newDiamondLabel += GameServiceLayer.serviceLayer.gameMaster.getRequiredDiamondCount().ToString();
        diamondLabel.text = newDiamondLabel;
    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.gameMaster.CollectedDiamondCountChanged -= updateDiamondLabel;
    }



}
