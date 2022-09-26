namespace Lab2_PontusEkdahl.Customers;

public class GoldMember : Customer
{
    public GoldMember(string name, string password) : base(name, password)
    {
        Discount = 15;
    }
}