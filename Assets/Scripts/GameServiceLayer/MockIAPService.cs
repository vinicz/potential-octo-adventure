using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MockIAPService : IAPService
{

		public ItemStorage itemStorage;
		public List<IAPProduct> productList;
		private Dictionary<string,IAPProduct> productMap;

		public void Start ()
		{
				productMap = new Dictionary<string, IAPProduct> ();
				foreach (IAPProduct product in productList) {
						productMap.Add (product.item_id, product);
				}
		}

		public override void buyProduct (string productId)
		{
				IAPProduct product = productMap [productId];
				bool purchaseSuccesfull = false;

				if (product.payingCurrency == IAPProduct.ProductType.MONEY && 
						product.productItemTye == IAPProduct.ProductType.TOKEN) {
						itemStorage.addTokens (product.amount);

						purchaseSuccesfull = true;
				} else if (product.payingCurrency == IAPProduct.ProductType.TOKEN && 
						product.productItemTye == IAPProduct.ProductType.CHARACTER) {
						purchaseSuccesfull = purchaseWithTokens (product);
						if (purchaseSuccesfull) {
								product.purchased = true;
						}
						

				} else if (product.payingCurrency == IAPProduct.ProductType.TOKEN && 
						product.productItemTye == IAPProduct.ProductType.REWARD) {
						purchaseSuccesfull = purchaseWithTokens (product);

						if (purchaseSuccesfull) {
								itemStorage.addRewards (product.amount);
						}
				}

				if (purchaseSuccesfull) {
						OnPurchaseCompletedSuccesfully ();
				} else {
						OnPurchaseFailed ();
				}
		}

		public override List<IAPProduct> getProducts ()
		{
				return productList;
		}

		public override IEnumerable<IAPProduct> getProductOfType (IAPProduct.ProductType productype)
		{
				return (from product in productList where product.productItemTye == productype select product);

		}
    
		bool purchaseWithTokens (IAPProduct product)
		{
				bool purchaseSuccesfull = false;

				if (product.price <= itemStorage.getTokenCount ()) {
						itemStorage.removeTokens ((int)product.price);
						purchaseSuccesfull = true;
				}
				return purchaseSuccesfull;
		}
}
