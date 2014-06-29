using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillItem : MonoBehaviour {

    public UILabel skillDescription;
    public GameObject buySkillObject;
    public UILabel skillPrice;
    public IAPProduct skillProduct;
    public SkillIconsViewFactory skillIconsViewFactory;

    private UISprite currentSkillIcon;


    public void setupSkillItem(IAPProduct skillProduct)
    {
        skillDescription.text = skillProduct.description;
        skillPrice.text = skillProduct.price.ToString();
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
            skillPrice.color = Color.red;
            currentSkillIcon.color = Color.red;

        } else
        { 
            skillDescription.color = Color.white;
            skillPrice.color = Color.white;
            currentSkillIcon.color = Color.white;
        }

        if (skillProduct.purchased)
        {
            buySkillObject.SetActive(false);
        } else
        {
            buySkillObject.SetActive(true);
        }
    }
}
