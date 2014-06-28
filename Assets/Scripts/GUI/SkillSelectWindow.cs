using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSelectWindow : MonoBehaviour {

    public UIGrid skillGrid;
    public GameObject characterItem;

    private SkillItem centeredSkillItem;
    private UICenterOnChildImproved centeringHandler;
    
    void Start ()
    {

        IEnumerable<IAPProduct> purchasableCharacters = 
            GameServiceLayer.serviceLayer.itemService.getIAPProductsOfType (IAPProduct.ProductType.SKILL);
        List<string> skills = 
            GameServiceLayer.serviceLayer.characterService.getPossibleSkills();
        centeringHandler = skillGrid.GetComponent<UICenterOnChildImproved> ();
        centeringHandler.NewItemCentered += onNewItemCentered;
        
        
        foreach (string skill in skills) {

            foreach (IAPProduct skillProduct in purchasableCharacters) {
                if (skill.Equals (skillProduct.item_id)) {
                    
                    createSkillItem (skillProduct);
                    break;
                }
                
            }
            
        }
        
        skillGrid.Reposition ();
        
        
    }
    
    void onNewItemCentered ()
    {
        GameObject skillItemObject = centeringHandler.centeredObject;
        centeredSkillItem = skillItemObject.GetComponent<SkillItem>();
    }
    
    GameObject createSkillItem (IAPProduct skillProduct)
    {
        GameObject newSkillItemObject = NGUITools.AddChild (skillGrid.gameObject, characterItem);
        newSkillItemObject.transform.localPosition = characterItem.transform.position;
        newSkillItemObject.transform.localRotation = characterItem.transform.rotation;
        newSkillItemObject.transform.localScale = characterItem.transform.localScale;

        SkillItem newSkillItem = newSkillItemObject.GetComponent<SkillItem> ();
        newSkillItem.setupSkillItem(skillProduct);

        return newSkillItemObject;
    }


    public SkillItem getCenteredSkillItem()
    {
        return centeredSkillItem;
    }
    
}
