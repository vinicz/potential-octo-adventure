using UnityEngine;
using System.Collections;

public class ProductItem : MonoBehaviour
{

		public UISprite tokenProductSprite;
		public UISprite premiumProductSprite;
		public UISprite rewardProductSprite;
		public UISprite moneyCurrencySprite;
		public UISprite tokenCurrencySprite;
		public UILabel productNameLabel;
		public UILabel amountLabel;
		public UILabel priceLabel;
		public UILabel priceTextLabel;
		public UILabel descriptionLabel;
		public ProductButtonTool productButton;
		private IAPProduct product;

		public void setupProductItem (IAPProduct product)
		{
				this.product = product;

				setupProductIcon ();
				setupCurrencyIcon ();
				setupLabels ();
				setupButton ();

				GameServiceLayer.serviceLayer.itemService.ItemCountChanged += setupLabels;

		}

		void setupProductIcon ()
		{
				switch (product.productItemTye) {
				case IAPProduct.ProductType.REWARD:
						rewardProductSprite.gameObject.SetActive (true);
						break;
				case IAPProduct.ProductType.TOKEN:
						tokenProductSprite.gameObject.SetActive (true);
						break;
				case IAPProduct.ProductType.PREMIUM_MEMEBERSHIP:
						premiumProductSprite.gameObject.SetActive (true);
						break;
				}
		}

		void setupCurrencyIcon ()
		{
				switch (product.payingCurrency) {
				case IAPProduct.ProductType.MONEY:
						moneyCurrencySprite.gameObject.SetActive (true);
						break;
				case IAPProduct.ProductType.TOKEN:
						tokenCurrencySprite.gameObject.SetActive (true);
						break;
				}
		}

		void setupLabels ()
		{
				productNameLabel.text = product.name;
				amountLabel.text = product.amount.ToString ();
				priceLabel.text = product.price.ToString ();
				descriptionLabel.text = product.description;

				if (GameServiceLayer.serviceLayer.itemService.getTokenCount () < product.price 
						&& product.payingCurrency == IAPProduct.ProductType.TOKEN) {
						productNameLabel.color = Color.red;
						amountLabel.color = Color.red;
						priceLabel.color = Color.red;
						descriptionLabel.color = Color.red;
						priceTextLabel.color = Color.red;
						productButton.gameObject.GetComponent<UIButton> ().isEnabled = false;
				} else {
						productNameLabel.color = Color.white;
						amountLabel.color = Color.white;
						priceLabel.color = Color.white;
						descriptionLabel.color = Color.white;
						priceTextLabel.color = Color.white;
						productButton.gameObject.GetComponent<UIButton> ().isEnabled = true;

				}
		}

		void setupButton ()
		{
				productButton.product = product;
		}

		void OnDestroy ()
		{
				GameServiceLayer.serviceLayer.itemService.ItemCountChanged -= setupLabels;
		}
}
