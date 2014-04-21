using UnityEngine;
using System.Collections;

public class RewardWindow : MonoBehaviour {

    public UILabel explosiveLabel;
    public UILabel explosionLabel;

	
	void Start () {

        explosiveLabel.text = GameServiceLayer.serviceLayer.itemService.getRewardCount().ToString();
        explosionLabel.text = GameServiceLayer.serviceLayer.itemService.getTokenCount().ToString();
	}
	
	
}
