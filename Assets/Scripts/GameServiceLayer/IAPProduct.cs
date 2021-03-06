using System;

[Serializable]
public class IAPProduct
{
    public enum ProductType
    {
        MONEY,
        REWARD,
        TOKEN,
        SERVICE,
		CHARACTER,
        SKILL
    };

    public string name;
    public string description;
    public string item_id;
    public int amount;
    public float price;
    public ProductType payingCurrency;
    public ProductType productItemTye;
	public bool consumable = true;
	public bool purchased = false;

    public IAPProduct(string name, string description, string item_id, int amount, int price, ProductType payingCurrency, ProductType productItemTye)
    {
        this.name = name;
        this.description = description;
        this.item_id = item_id;
        this.amount = amount;
        this.price = price;
        this.payingCurrency = payingCurrency;
        this.productItemTye = productItemTye;
    }

}


