using UnityEngine;
using System.Collections;

public class DiamondWindow : MonoBehaviour
{

    public UILabel diamondLabel;
    private bool inited = false;
	private DiamondMasterScript diamondGameHandler;

    void Update()
    {
        if (!inited)
        {
			diamondGameHandler = (DiamondMasterScript) GameServiceLayer.serviceLayer.gameMaster;

            updateDiamondLabel();
			diamondGameHandler.CollectedDiamondCountChanged += updateDiamondLabel;
        }
    }

    void updateDiamondLabel()
    {
        string newDiamondLabel = "";

		newDiamondLabel += diamondGameHandler.getCollectedDiamonds().ToString();
        newDiamondLabel += "/";
		newDiamondLabel += diamondGameHandler.getRequiredDiamondCount().ToString();
        diamondLabel.text = newDiamondLabel;
    }

    void OnDestroy()
    {
		diamondGameHandler.CollectedDiamondCountChanged -= updateDiamondLabel;
    }



}
