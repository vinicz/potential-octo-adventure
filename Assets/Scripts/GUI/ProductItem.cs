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
	public UILabel descriptionLabel;
    public ProductButtonTool productButton;

    public void setupProductItem(IAPProduct product)
    {
        setupProductIcon(product);
        setupCurrencyIcon(product);
        setupLabels(product);
        setupButton(product);

    }

    void setupProductIcon(IAPProduct product)
    {
        switch (product.productItemTye)
        {
            case IAPProduct.ProductType.REWARD:
                rewardProductSprite.gameObject.SetActive(true);
                break;
            case IAPProduct.ProductType.TOKEN:
                tokenProductSprite.gameObject.SetActive(true);
                break;
            case IAPProduct.ProductType.PREMIUM_MEMEBERSHIP:
                premiumProductSprite.gameObject.SetActive(true);
                break;
        }
    }

    void setupCurrencyIcon(IAPProduct product)
    {
        switch (product.payingCurrency)
        {
            case IAPProduct.ProductType.MONEY:
                moneyCurrencySprite.gameObject.SetActive(true);
                break;
            case IAPProduct.ProductType.TOKEN:
                tokenCurrencySprite.gameObject.SetActive(true);
                break;
        }
    }

    void setupLabels(IAPProduct product)
    {
        productNameLabel.text = product.name;
        amountLabel.text = product.amount.ToString();
        priceLabel.text = product.price.ToString();
		descriptionLabel.text = product.description;
    }

    void setupButton(IAPProduct product)
    {
        productButton.product = product;
    }
}
