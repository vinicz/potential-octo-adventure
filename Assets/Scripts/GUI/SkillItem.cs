using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillItem : MonoBehaviour {

    public UILabel skillDescription;
    public IAPProduct skillProduct;
    public SkillIconsViewFactory skillIconsViewFactory;

    private UISprite currentSkillIcon;


    public void setupSkillItem(IAPProduct skillProduct)
    {
        skillDescription.text = skillProduct.description;
        this.skillProduct = skillProduct;

        skillIconsViewFactory.getSkillIconsView().showSkillIcon(skillProduct.item_id);
        currentSkillIcon = skillIconsViewFactory.getSkillIconsView().getSelectedSkillIcon();

        setupLabels();
        GameServiceLayer.serviceLayer.itemService.ItemCountChanged += setupLabels;
    }


    void setupLabels()
    {
        if (!skillProduct.purchased && skillProduct.price > GameServiceLayer.serviceLayer.itemService.getTokenCount())
        {
            skillDescription.color = Color.red;
            currentSkillIcon.color = Color.red;

        } else
        { 
            skillDescription.color = Color.white;
            currentSkillIcon.color = Color.white;
        }

        if (skillProduct.purchased)
        {

        } else
        {
       
        }
    }
}
