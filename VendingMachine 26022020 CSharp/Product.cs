
public class Product
{
    private string name;
    private int itemID;
    private double price;
    private double factoryPrice;

    #region Properties
    public string Name
    {
        get { return name; }
        private set { name = value; }
    }
    public int ItemID
    {
        get { return itemID; }
        set { itemID = value; }
    }
    public double Price
    {
        get { return price; }
        set { price = value; }
    }
    public double FactoryPrice
    {
        get { return factoryPrice; }
        private set { factoryPrice = value; }
    }
    #endregion

    public Product(string name, int id, double price)
    {
        this.name = name;
        this.itemID = id;
        this.price = price;
        factoryPrice = price / 2;
    }
}