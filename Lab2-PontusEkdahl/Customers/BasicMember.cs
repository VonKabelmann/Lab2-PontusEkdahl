namespace Lab2_PontusEkdahl.Customers;

public class BasicMember : Customer
{
    public BasicMember(string name, string password) : base(name, password)
    {
        Discount = 0;
    }
    public override string ToString()
    {
        var returnString = $"{base.ToString()}, Tier: Basic";
        if (ShoppingCart.Count != 0)
        {
            returnString += ", Cart: ";
            foreach (var product in ShoppingCart)
            {
                returnString += $"*{product.ToString()}";
            }
        }
        return returnString;
    }
}