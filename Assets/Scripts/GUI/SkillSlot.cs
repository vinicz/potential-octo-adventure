using UnityEngine;
using System.Collections;

public class SkillSlot : MonoBehaviour
{

    public SkillSelectWindow skillSelectWindow;
    public int skillIndex;
    public SkillIconsViewFactory skillIconsViewFactory;
    public UILabel initialLabel;

    void Start()
    {
        string selectedSkill = GameServiceLayer.serviceLayer.characterService.getSelectedSkill(skillIndex);

        if (!selectedSkill.Equals(""))
        {
            showSkill(selectedSkill);
        } else
        {
            initialLabel.gameObject.SetActive(true);
        }
    }

    void OnClick()
    {
        SkillItem centeredSkillItem = skillSelectWindow.getSelectedSkillItem();

        if (!centeredSkillItem.skillProduct.purchased)
        {
            GameServiceLayer.serviceLayer.itemService.PurchaseCompleted += skillPurchaseCompleted;
            GameServiceLayer.serviceLayer.itemService.PurchaseFailed += skillPurchaseFailed;
            GameServiceLayer.serviceLayer.itemService.buyIAPProduct(centeredSkillItem.skillProduct.item_id);
        } else
        {
            selectCenteredSkill();
        }

    }

    void skillPurchaseCompleted()
    {
        unsubscribePurchaseEvents();

        selectCenteredSkill();

    }

    void skillPurchaseFailed()
    {
        unsubscribePurchaseEvents();
    }

    void selectCenteredSkill()
    {
        SkillItem centeredSkillItem = skillSelectWindow.getSelectedSkillItem();
        string skillId = centeredSkillItem.skillProduct.item_id;
        GameServiceLayer.serviceLayer.characterService.setSelectedSkill(skillId, skillIndex);
        showSkill(skillId);
    }

    void showSkill(string skillId)
    {
      
        skillIconsViewFactory.getSkillIconsView().showSkillIcon(skillId);
        initialLabel.gameObject.SetActive(false);
    }

    void unsubscribePurchaseEvents()
    {
        GameServiceLayer.serviceLayer.itemService.PurchaseCompleted -= skillPurchaseCompleted;
        GameServiceLayer.serviceLayer.itemService.PurchaseFailed -= skillPurchaseFailed;
    }
}
