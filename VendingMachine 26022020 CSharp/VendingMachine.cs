using System.Collections.Generic;

public class VendingMachine
{
    private List<Product> products = new List<Product>();
    private List<int> productAmount = new List<int>();

    private Money money = new Money(0);

    private int maxProductsInMachine;
    private int maxProductsPerCell;

    #region Properties
    public List<Product> Products
    {
        get { return products; }
        private set { products = value; }
    }
    public List<int> ProductAmount
    {
        get { return productAmount; }
        private set { productAmount = value; }
    }
    #endregion

    public VendingMachine(int maxProductsInMachine, int maxProductsPerCell)
    { 
        this.maxProductsInMachine = maxProductsInMachine;
        this.maxProductsPerCell = maxProductsPerCell;
    }

    /// <summary>
    /// Used to but product from vending machine
    /// </summary>
    /// <returns>product</returns>
    public Product BuyProduct(int id, Money money)
    {
        int productIndex = FindProductIndexFromID(id);
        if (productIndex == -1)
            return null;

        if ((money.Balance >= products[productIndex].Price) && (productAmount[productIndex] >= 1))
        {
            productAmount[productIndex]--;
            money.Balance += this.money.Balance;
            money.Balance -= products[productIndex].Price;
            return products[productIndex];
        }
        else
            return null;
    }

    /// <summary>
    /// Used to change price on product in vending machine
    /// </summary>
    /// <returns>message</returns>
    public string ChangeProductPrice(int id, double price)
    {
        int productIndex = FindProductIndexFromID(id);

        if (productIndex == -1)
            return "Product id was invalid!";

        products[productIndex].Price = price;
        return "Price changed!";
    }

    /// <summary>
    /// Used to refill product in the vending machine
    /// </summary>
    /// <returns>message</returns>
    public string RefillProduct(FactoryPack pack)
    {
        if (pack == null)
            return "Product was invalid!";

        int productIndex = FindProductIndexFromID(pack.Product.ItemID);

        ProductAmount[productIndex] += pack.Amount;
        return "Product refilled";
    }

    /// <summary>
    /// Used to add product in the vending machine
    /// </summary>
    /// <returns>message</returns>
    public string AddProduct(FactoryPack pack)
    {
        if (pack == null)
            return "Product was invalid!";

        products.Add(pack.Product);
        productAmount.Add(pack.Amount);

        return "Product added!";
    }

    /// <summary>
    /// Used to empty the money box in the vending machine
    /// </summary>
    /// <returns>Money</returns>
    public Money EmptyMoneyBox()
    {
        Money get = new Money(money.Balance);
        money.Balance = 0;
        return get;
    }

    /// <summary>
    /// Used to check if there is any kinds room in the vending machine
    /// </summary>
    /// <returns>null if successful or message if failed</returns>
    public string CheckMachineRoom(int id, int amount)
    {
        int index = FindProductIndexFromID(id);

        if (amount <= 0)
            return "Invalid amount!";

        if (index != -1)
        {
            int cellAmount = productAmount[index] + amount;
            if ((productAmount[index] + amount) > maxProductsPerCell)
                return "Too many products in cell! Only " + maxProductsPerCell + " allowed!";
        }

        if (amount > maxProductsPerCell)
            return "Too many products in cell! Only " + maxProductsPerCell + " allowed!";

        if (productAmount.Count > maxProductsInMachine)
            return "Too many products in machine! Only " + maxProductsInMachine + " allowed!";

        return null;
    }

    /// <summary>
    /// Used to convert a id into a array product index 
    /// </summary>
    /// <returns>array indroex of product if successfull or -1 if failed</returns>
    public int FindProductIndexFromID(int id)
    {
        for (int i = 0; i < products.Count; i++)
            if (products[i].ItemID == id)
                return i;
        return -1;
    }
}