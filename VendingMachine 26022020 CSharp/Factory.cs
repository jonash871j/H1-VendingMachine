
public class Factory
{
    private Money money;

    #region Properties
    public Money Money
    {
        get { return money; }
        private set { money = value; }
    }
    #endregion

    public Factory(double balance)
    {
        this.money = new Money(balance);
    }

    /// <summary>
    /// Used to buy factory pack consisting of products from factory
    /// </summary>
    /// <returns>FactoryPack</returns>
    public FactoryPack BuyFromFactory(Product product, Money money, int amount)
    {
        FactoryPack factoryPack = new FactoryPack(product, amount);

        if (product.Price <= 0)
            return null;

        if (money != null)
        {
            double price = (product.FactoryPrice * amount);
            if (price > money.Balance)
                return null;
            else
            {
                money.Balance -= price;
                this.money.Balance += price;
            }
        }
        return factoryPack;
    }
}