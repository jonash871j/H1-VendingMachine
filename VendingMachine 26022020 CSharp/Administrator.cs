
class Administrator
{
    private Money money;

    #region Properties
    public Money Money
    {
        get { return money; }
        set { money = value; }
    }
    #endregion

    public Administrator(double balance)
    {
        this.money = new Money(balance);
    }
}