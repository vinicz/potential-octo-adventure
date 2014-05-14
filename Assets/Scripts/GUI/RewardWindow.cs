using UnityEngine;
using System.Collections;

public class RewardWindow : MonoBehaviour {

    public UILabel explosiveLabel;
    public UILabel explosionLabel;

    private bool inited = false;

	
	void Start () {

        refreshAllItemCount();
        GameServiceLayer.serviceLayer.itemService.ItemCountChanged += refreshAllItemCount;

	}


    void Update()
    {
        if (!inited)
        {
            refreshAllItemCount();
            inited  =true;
        }
    }
	
	
    void refreshAllItemCount()
    {
        explosiveLabel.text = GameServiceLayer.serviceLayer.itemService.getRewardCount().ToString();
        explosionLabel.text = GameServiceLayer.serviceLayer.itemService.getTokenCount().ToString();
    }

    void OnDestroy()
    {
        GameServiceLayer.serviceLayer.itemService.ItemCountChanged -= refreshAllItemCount;
    }
}
