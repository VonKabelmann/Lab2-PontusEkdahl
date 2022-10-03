namespace Lab2_PontusEkdahl.Customers;

public class SilverMember : Customer
{
    public SilverMember(string name, string password) : base(name, password)
    {
        Discount = 10;
    }
    public override string ToString()
    {
        var returnString = $"{base.ToString()}, Tier: Silver, Cart: ";
        foreach (var product in ShoppingCart)
        {
            returnString += product.ToString();
        }
        return returnString;
    }
}