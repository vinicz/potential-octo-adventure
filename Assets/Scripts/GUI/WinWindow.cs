using UnityEngine;
using System.Collections;

public class WinWindow : UIWindow {

    public UIButton nextUIButton;
    public UILabel nextButtonLabel;
    public GameObject rewardObject;
    public UISprite reward1Icon;
    public UISprite reward2Icon;
    public UISprite reward3Icon;
    public GameObject lockObject;
    public UILabel lockLabel;

    bool inited = false;

	
	void Update () 
    {
        if (!inited)
        {
            initWindow();
        }
	}

    public override void initWindow()
    {
        GameServiceLayer.serviceLayer.gameMaster.LevelPassed  += onLevelPassed;
        this.gameObject.SetActive(false);
        inited = true;
    }

    void onLevelPassed()
    {
       
        this.gameObject.SetActive(true);
        LevelRecord currentLevel = GameServiceLayer.serviceLayer.gameMaster.getCurrentLevelRecord();
        setupLevelRewardIcons(currentLevel);

    }


    void setupLevelRewardIcons(LevelRecord level)
    {
        
        if (level.collectedRewards >= 1)
        {
            reward1Icon.color = Color.white;
        }
        else
        {
            reward1Icon.color = Color.black;
        }
        if (level.collectedRewards >= 2)
        {
            reward2Icon.color = Color.white;
        }
        else
        {
            reward2Icon.color = Color.black;
        }
        if (level.collectedRewards >= 3)
        {
            reward3Icon.color = Color.white;
        }
        else
        {
            reward3Icon.color = Color.black;
        }


        if (level.getRequiredRewards() > GameServiceLayer.serviceLayer.itemService.getRewardCount())
        {
            lockObject.SetActive(true);
            lockLabel.text = level.getRequiredRewards().ToString();
            nextUIButton.isEnabled = false;
            nextButtonLabel.gameObject.SetActive(false);
        }
    }
}
