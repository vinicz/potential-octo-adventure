using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopWindow : MonoBehaviour
{

    public GameObject productItemViewPrefab;
    public int rows = 2;
    public float columnWidth = 600f;
    public float rowWidth = 240f;
    public float depthAdjustment = 50f;

    void Start()
    {

        List<IAPProduct> prodcuts = GameServiceLayer.serviceLayer.itemService.getIAPProducts();
    
        int rowCounter = 0;
        int columnCounter = 0;

        foreach (IAPProduct product in prodcuts)
        {
            GameObject productItemObject = NGUITools.AddChild(this.gameObject, productItemViewPrefab);
            ProductItem productItemView = productItemObject.GetComponent<ProductItem>();
            productItemView.setupProductItem(product);

            productItemObject.transform.localPosition = productItemViewPrefab.transform.position +  new Vector3(columnCounter * columnWidth, 
                                                                                                                rowCounter * -rowWidth, 
                                                                                                                columnCounter * depthAdjustment);
            productItemObject.transform.localRotation = productItemViewPrefab.transform.rotation;
            productItemObject.transform.localScale = productItemViewPrefab.transform.localScale;

            rowCounter++;
            if (rowCounter >= rows)
            {
                rowCounter = 0;
                columnCounter++;
            }
        }
    }

}
