using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl.Customers
{
    public abstract class Customer
    {
        /*
         * =============================== PROPERTIES & CONSTRUCTOR =================================
         */
        public string Name { get; private set; }

        public string Password { get; private set; }

        public List<Product> ShoppingCart { get; set; }

        public int Discount { get; protected set; }

        public enum MembershipTier
        {
            Unassigned,
            Basic,
            Bronze,
            Silver,
            Gold
        }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            ShoppingCart = new List<Product>();
        }

        
        
        /*
         * =================================== CLASS METHODS ====================================
         */
        public override string ToString()
        {
            return $"Name: {Name}, Password: {Password}";
        }

        public double CalculateDiscount()
        {
            return 1 - (Discount * 0.01);
        }

        public bool VerifyPassword(string pwInput)
        {
            if (pwInput == Password)
            {
                return true;
            }
            return false;
        }

        public string GetTypeAsString()
        {
            switch (this)
            {
                case BasicMember:
                    return "Basic";
                case BronzeMember:
                    return "Bronze";
                case SilverMember:
                    return "Silver";
                case GoldMember:
                    return "Gold";
                default:
                    return "ERROR";
            }
        }

        public void ViewCart()
        {
            var distinctCart = ShoppingCart.Distinct();
            var totalSum = ShoppingCart.Sum(prod => prod.Price);

            Console.Clear();
            Console.WriteLine("Your cart currently contains: \n");
            foreach (var distinctProd in distinctCart)
            {
                var amount = ShoppingCart.Count(cartProd => cartProd == distinctProd);
                Console.WriteLine($"\nYou have {amount} {(amount == 1 ? distinctProd.Name : distinctProd.PluralName)}");
                Console.WriteLine(
                    $"Total price of your " +
                    $"{(amount == 1 ? distinctProd.Name : distinctProd.PluralName)}: " +
                    $"{(distinctProd.Price * amount):0.00}" +
                    $"{Product.GetCurrencySymbol(distinctProd.Currency)}");
            }

            Console.WriteLine($"\nThe total price for all your products is: {totalSum:0.00}" +
                              $"{Product.GetCurrencySymbol(distinctCart.First().Currency)}");


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
