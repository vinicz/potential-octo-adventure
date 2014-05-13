using UnityEngine;
using System.Collections;

public class LevelItem : MonoBehaviour
{

    public UILabel levelName;
    public UILabel levelInfo;
    public LevelButtonTool levelButton;
    public UIButton levelUIButton;
    public UIDragPanelContents dragPanelContents;
    public GameObject lockObject;
    public UILabel lockLabel;
    public GameObject rewardObject;
    public UISprite reward1Icon;
    public UISprite reward2Icon;
    public UISprite reward3Icon;
    private LevelRecord levelRecord;

    public void setupLevelItem(GameObject parent, LevelRecord level, GameObject currentWindow)
    {
        levelRecord = level;
        levelName.text = level.levelName;
        levelInfo.text = "Best time: " + level.bestTime.ToString("0.00");
        levelInfo.text += "\nExplosives: " + level.collectedRewards + "/3";
        levelInfo.text += "\nRequirement: " + level.getRequiredRewards();

        dragPanelContents.draggablePanel = parent.transform.parent.GetComponent<UIDraggablePanel>();

        setupLevelRewardIcons(level);
        createLevelButton(level, currentWindow);
  
        GameServiceLayer.serviceLayer.itemService.ItemCountChanged += refreshLevelIcons;

    }

    public void destroyLevel()
    {
        GameServiceLayer.serviceLayer.itemService.ItemCountChanged -= refreshLevelIcons;
    }

    public void refreshLevelIcons()
    {
        setupLevelRewardIcons(levelRecord);
    }

    void setupLevelRewardIcons(LevelRecord level)
    {
        if (level.getRequiredRewards() <= GameServiceLayer.serviceLayer.itemService.getRewardCount())
        {
            levelUIButton.isEnabled = true;
            lockObject.SetActive(false);
            rewardObject.SetActive(true);

            if (level.collectedRewards >= 1)
            {
                reward1Icon.color = Color.white;
            } else
            {
                reward1Icon.color = Color.black;
            }
            if (level.collectedRewards >= 2)
            {
                reward2Icon.color = Color.white;
            } else
            {
                reward2Icon.color = Color.black;
            }
            if (level.collectedRewards >= 3)
            {
                reward3Icon.color = Color.white;
            } else
            {
                reward3Icon.color = Color.black;
            }
        } else
        {
            lockObject.SetActive(true);
            rewardObject.SetActive(false);
            lockLabel.text = level.getRequiredRewards().ToString();
            levelUIButton.isEnabled = false;
        }
    }

    void createLevelButton(LevelRecord level, GameObject currentWindow)
    {
        levelButton.levelIndex = level.getLevelIndex();
        levelButton.currentWindow = currentWindow;
    }


}
