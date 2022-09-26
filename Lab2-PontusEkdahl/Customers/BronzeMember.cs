namespace Lab2_PontusEkdahl.Customers;

public class BronzeMember : Customer
{
    public BronzeMember(string name, string password) : base(name, password)
    {
        Discount = 5;
    }
}