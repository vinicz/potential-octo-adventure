using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSelectWindow : MonoBehaviour
{

    public delegate void SelectedSkillChangedHandler();
    public event SelectedSkillChangedHandler SelectedSkillChanged;

    public UIGrid skillTableParent;
    public GameObject characterItem;
    public GameObject buySkillObject;
    public UILabel selectedSkillDescritptionLabel;
    public UILabel selectedSkillPriceLabel;
    private SkillItem selectedSkillItem;
    private UICenterOnChildImproved centeringHandler;
    
    void Start()
    {

        IEnumerable<IAPProduct> purchasableCharacters = 
            GameServiceLayer.serviceLayer.itemService.getIAPProductsOfType(IAPProduct.ProductType.SKILL);
        List<string> skills = 
            GameServiceLayer.serviceLayer.characterService.getPossibleSkills();
        
        foreach (string skill in skills)
        {

            foreach (IAPProduct skillProduct in purchasableCharacters)
            {
                if (skill.Equals(skillProduct.item_id))
                {
                    
                    createSkillItem(skillProduct);
                    break;
                }
                
            }
            
        }

        skillTableParent.Reposition();

        
        
    }

    void onSelectedSkillPurchase()
    {
        if (!selectedSkillItem.skillProduct.purchased)
        {
            GameServiceLayer.serviceLayer.itemService.PurchaseCompleted += skillPurchaseCompleted;
            GameServiceLayer.serviceLayer.itemService.PurchaseFailed += skillPurchaseFailed;
            GameServiceLayer.serviceLayer.itemService.buyIAPProduct(selectedSkillItem.skillProduct.item_id);
        } 
    }
    
//    void onNewItemCentered()
//    {
//        GameObject skillItemObject = centeringHandler.centeredObject;
//        selectedSkillItem = skillItemObject.GetComponent<SkillItem>();
//    }
//    
    GameObject createSkillItem(IAPProduct skillProduct)
    {
        GameObject newSkillItemObject = NGUITools.AddChild(skillTableParent.gameObject, characterItem);
        newSkillItemObject.transform.localPosition = characterItem.transform.position;
        newSkillItemObject.transform.localRotation = characterItem.transform.rotation;
        newSkillItemObject.transform.localScale = characterItem.transform.localScale;

        SkillItem newSkillItem = newSkillItemObject.GetComponent<SkillItem>();
        newSkillItem.setupSkillItem(skillProduct, this);


        return newSkillItemObject;
    }

    void skillPurchaseCompleted()
    {
        unsubscribePurchaseEvents();
        buySkillObject.SetActive(false);

        if (SelectedSkillChanged != null)
        {
            SelectedSkillChanged();
        }

    }
    
    void skillPurchaseFailed()
    {
        unsubscribePurchaseEvents();
    }

    void unsubscribePurchaseEvents()
    {
        GameServiceLayer.serviceLayer.itemService.PurchaseCompleted -= skillPurchaseCompleted;
        GameServiceLayer.serviceLayer.itemService.PurchaseFailed -= skillPurchaseFailed;
    }

    public SkillItem getSelectedSkillItem()
    {
        return selectedSkillItem;
    }

    public void setSelectedSkillItem(SkillItem skill)
    {
        selectedSkillItem = skill;

        selectedSkillPriceLabel.text = selectedSkillItem.skillProduct.price.ToString();
        selectedSkillDescritptionLabel.text = selectedSkillItem.skillProduct.description;

        if (selectedSkillItem.skillProduct.purchased)
        {
            buySkillObject.SetActive(false);
        } else
        {
            buySkillObject.SetActive(true);
        }


        if (SelectedSkillChanged != null)
        {
            SelectedSkillChanged();
        }

    }
    
}
