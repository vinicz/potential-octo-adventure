using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MockIAPService : IAPService
{

		public ItemStorage itemStorage;
		public List<IAPProduct> productList;
		private Dictionary<string,IAPProduct> productMap;
		public string file = "/ballthazar_iap_storage.dat";
		private string fullFilePath;

		public void Start ()
		{
				

				productMap = new Dictionary<string, IAPProduct> ();
				foreach (IAPProduct product in productList) {
						productMap.Add (product.item_id, product);
				}

				fullFilePath = Application.persistentDataPath + file;
				load ();
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
						save ();
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

		private void save ()
		{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.OpenWrite (fullFilePath);
		
				bf.Serialize (file,productList);
				file.Close ();
		}
	
		private void load ()
		{
				if (File.Exists (fullFilePath)) {
			
						BinaryFormatter bf = new BinaryFormatter ();
						FileStream file = File.Open (fullFilePath, FileMode.Open);
			
						List<IAPProduct> persitedData = (List<IAPProduct>)bf.Deserialize (file);
						
						mergePersistedProducts (persitedData);
			
						file.Close ();
			
				} else {
						save ();
				} 
		}
		
		void mergePersistedProducts (List<IAPProduct> persitedData)
		{
				foreach (IAPProduct persistedProduct in persitedData) {
						IAPProduct product = productMap [persistedProduct.item_id];
						if (product != null) {
								product.purchased = persistedProduct.purchased;
						}
				}
		}
}
