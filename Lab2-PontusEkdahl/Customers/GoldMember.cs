namespace Lab2_PontusEkdahl.Customers;

public class GoldMember : Customer
{
    public GoldMember(string name, string password) : base(name, password)
    {
        Discount = 15;
    }
    public override string ToString()
    {
        return $"{base.ToString()}, Tier: Gold";
    }
}