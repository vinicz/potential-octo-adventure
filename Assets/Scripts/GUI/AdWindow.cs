using UnityEngine;
using System.Collections;

public class AdWindow : UIWindow {

    public string AD_FREE_SERVICE_ID = "add_free";
    public float adTime = 5f;
    public UILabel adTimeLabel;

    private IAPProduct addFreeServicePorduct;
	
    void Start()
    {
        if (addFreeServicePorduct.purchased)
        {
            loadNextLevel();
        }
    }


	void Update () {
	
        if (adTime >= 0)
        {
            adTime -= Time.deltaTime;
           
        } else
        {
            adTime =0;
            loadNextLevel();
        }

        adTimeLabel.text = "Continue in... " + adTime.ToString("0");
	}


    public override void initWindow()
    {
        addFreeServicePorduct = GameServiceLayer.serviceLayer.itemService.getIAPProduct(AD_FREE_SERVICE_ID);
    }

    void loadNextLevel()
    {
        this.gameObject.SetActive(false);
        GameServiceLayer.serviceLayer.gameMaster.loadNextLevel();
    }
}
