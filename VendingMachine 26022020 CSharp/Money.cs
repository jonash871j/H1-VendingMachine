
public class Money
{
    private double balance;

    #region Properties
    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }
    #endregion

    public Money(double balance)
    {
        this.balance = balance;
    }
}
