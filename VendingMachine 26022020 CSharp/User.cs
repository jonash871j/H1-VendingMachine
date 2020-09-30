using System.Collections.Generic;

public class User
{
    private List<Product> ownedProducts = new List<Product>();
    private Money money;

    #region Properties
    public Money Money
    {
        get { return money; }
        set { money = value; }
    }
    public List<Product> OwnedProducts
    {
        get { return ownedProducts; }
        set { ownedProducts = value; }
    }
    #endregion

    public User(double balance)
    {
        this.money = new Money(balance);
    }
}