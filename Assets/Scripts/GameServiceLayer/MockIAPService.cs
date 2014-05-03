using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MockIAPService : IAPService
{

    public ItemStorage itemStorage;
    public List<IAPProduct> productList;
    private Dictionary<string,IAPProduct> productMap;

    public void Start()
    {
        productMap = new Dictionary<string, IAPProduct>();
        foreach (IAPProduct product in productList)
        {
            productMap.Add(product.item_id, product);
        }
    }

    public override void buyProduct(string productId)
    {
        IAPProduct product = productMap [productId];
        bool purchaseSuccesfull = false;

        if (product.payingCurrency == IAPProduct.ProductType.MONEY && 
            product.productItemTye == IAPProduct.ProductType.TOKEN)
        {
            itemStorage.addTokens(product.amount);
            purchaseSuccesfull = true;
        } else if (product.payingCurrency == IAPProduct.ProductType.TOKEN && 
            product.productItemTye == IAPProduct.ProductType.REWARD)
        {
            if (product.price <= itemStorage.getTokenCount())
            {
                itemStorage.removeTokens((int)product.price);
                itemStorage.addRewards(product.amount);
                purchaseSuccesfull = true;
            }
        }

        if (purchaseSuccesfull)
        {
            OnPurchaseCompletedSuccesfully();
        } else
        {
            OnPurchaseFailed();
        }
    }

    public override List<IAPProduct> getProducts()
    {
        return productList;
    }

    
}
