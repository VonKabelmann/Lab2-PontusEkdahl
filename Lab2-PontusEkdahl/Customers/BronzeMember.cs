namespace Lab2_PontusEkdahl.Customers;

public class BronzeMember : Customer
{
    public BronzeMember(string name, string password) : base(name, password)
    {
        Discount = 5;
    }

    public override string ToString()
    {
        var returnString = $"{base.ToString()}, Tier: Bronze, Cart: ";
        foreach (var product in ShoppingCart)
        {
            returnString += product.ToString();
        }
        return returnString;
    }
}