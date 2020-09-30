
public class FactoryPack
{
    private Product product;
    private int amount;

    #region Properties
    public Product Product
    {
        get { return product; }
        private set { product = value; }
    }
    public int Amount
    {
        get { return amount; }
        private set { amount = value; }
    }
    #endregion

    public FactoryPack(Product product, int amount)
    {
        this.product = product;
        this.amount = amount;
    }
}
