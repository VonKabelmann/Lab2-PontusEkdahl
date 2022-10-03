using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl.Customers
{
    public abstract class Customer
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password { get; private set; }

        private List<Product> _shoppingCart;

        public List<Product> ShoppingCart
        {
            get { return _shoppingCart; }
            set { _shoppingCart = value; }
        }
        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            ShoppingCart = new List<Product>();
        }

        public enum MembershipTier
        {
            Unassigned,
            Basic,
            Bronze,
            Silver,
            Gold
        }
        public int Discount { get; protected set; }

        public override string ToString()
        {
            var returnString = $"Name: {Name}, Password: {Password}";
            return returnString;
        }

        public static MembershipTier GetTierFromString(string tier)
        {
            switch (tier)
            {
                case "Basic":
                    return Customer.MembershipTier.Basic;
                case "Bronze":
                    return Customer.MembershipTier.Bronze;
                case "Silver":
                    return Customer.MembershipTier.Silver;
                case "Gold":
                    return Customer.MembershipTier.Gold;
                default:
                    return Customer.MembershipTier.Unassigned;
            }
        }
        public static Customer CreateCustomer(string name, string pw, Customer.MembershipTier tier)
        {
            switch (tier)
            {
                case Customer.MembershipTier.Basic:
                    return new BasicMember(name, pw);
                case Customer.MembershipTier.Bronze:
                    return new BronzeMember(name, pw);
                case Customer.MembershipTier.Silver:
                    return new SilverMember(name, pw);
                case Customer.MembershipTier.Gold:
                    return new GoldMember(name, pw);
                default:
                    return new BasicMember("invalid", "invalid");
            }
        }
    }
}
