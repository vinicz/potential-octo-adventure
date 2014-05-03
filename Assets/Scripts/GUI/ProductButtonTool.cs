using UnityEngine;
using System.Collections;

public class ProductButtonTool : MonoBehaviour {

	
    public IAPProduct product;
    
    public void OnClick()
    {
        GameServiceLayer.serviceLayer.itemService.buyIAPProduct(product.item_id);
    }



}
