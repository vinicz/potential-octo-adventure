using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillItem : MonoBehaviour {

    public UILabel skillDescription;
    public GameObject buySkillObject;
    public UILabel skillPrice;
    public IAPProduct skillProduct;
    public UISprite defaultSkillIcon;

    public List<SkillIcon> skillIcons;


    public void setupSkillItem(IAPProduct skillProduct)
    {
        skillDescription.text = skillProduct.description;
        skillPrice.text = skillProduct.price.ToString();
        this.skillProduct = skillProduct;

        UISprite currentSkillIcon = SkillIcon.findIconForSkill(skillIcons, skillProduct.item_id, defaultSkillIcon);
        currentSkillIcon.gameObject.SetActive(true);
    }


}
