namespace Lab2_PontusEkdahl.Customers;

public class SilverMember : Customer
{
    public SilverMember(string name, string password) : base(name, password)
    {
        Discount = 10;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Tier: Silver";
    }
}