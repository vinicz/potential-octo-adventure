using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IAPService : MonoBehaviour
{

    public delegate void PurchaseCompletedSuccesfullyHandler();
    public event PurchaseCompletedSuccesfullyHandler PurchaseCompletedSuccesfully;

    public delegate void PurchaseFailedHandler();
    public event PurchaseFailedHandler PurchaseFailed;

    public abstract void buyProduct(string productId);

    public abstract List<IAPProduct> getProducts();

    protected virtual void OnPurchaseCompletedSuccesfully()
    {
        if (PurchaseCompletedSuccesfully != null)
        {
            PurchaseCompletedSuccesfully();
        }
    }

    protected virtual void OnPurchaseFailed()
    {
        if (PurchaseFailed != null)
        {
            PurchaseFailed();
        }
    }
    
}
