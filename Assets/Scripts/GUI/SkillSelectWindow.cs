using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSelectWindow : MonoBehaviour {

    public GameObject skillTableParent;
    public GameObject characterItem;
    public int skillTableRowSize =3;

    private SkillItem selectedSkillItem;
    private UICenterOnChildImproved centeringHandler;
    
    void Start ()
    {

        IEnumerable<IAPProduct> purchasableCharacters = 
            GameServiceLayer.serviceLayer.itemService.getIAPProductsOfType (IAPProduct.ProductType.SKILL);
        List<string> skills = 
            GameServiceLayer.serviceLayer.characterService.getPossibleSkills();

        
        
        foreach (string skill in skills) {

            foreach (IAPProduct skillProduct in purchasableCharacters) {
                if (skill.Equals (skillProduct.item_id)) {
                    
                    createSkillItem (skillProduct);
                    break;
                }
                
            }
            
        }

        
        
    }
    
    void onNewItemCentered ()
    {
        GameObject skillItemObject = centeringHandler.centeredObject;
        selectedSkillItem = skillItemObject.GetComponent<SkillItem>();
    }
    
    GameObject createSkillItem (IAPProduct skillProduct)
    {
        GameObject newSkillItemObject = NGUITools.AddChild (skillTableParent, characterItem);
        newSkillItemObject.transform.localPosition = characterItem.transform.position;
        newSkillItemObject.transform.localRotation = characterItem.transform.rotation;
        newSkillItemObject.transform.localScale = characterItem.transform.localScale;

        SkillItem newSkillItem = newSkillItemObject.GetComponent<SkillItem> ();
        newSkillItem.setupSkillItem(skillProduct);

        return newSkillItemObject;
    }


    public SkillItem getSelectedSkillItem()
    {
        return selectedSkillItem;
    }

    public void getSelectedSkillItem(SkillItem skill)
    {
        selectedSkillItem = skill;
    }
    
}
