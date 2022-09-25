using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl
{
    public class Product
    {
        public string Name { get; set; }

        private double _price;

        public double Price
        {
            get
            {
                switch (Currency)
                {
                    case Currencies.SEK:
                        return _price;
                    case Currencies.USD:
                        return _price * 0.09;
                    case Currencies.GBP:
                        return _price * 0.08;
                }
                return _price;

            }
            set { _price = value; }
        }

        public enum Currencies
        {
            SEK,
            USD,
            GBP
        }

        public Currencies Currency { get; private set; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
            Currency = Currencies.SEK;
        }

        public override string ToString()
        {
            return $"Product: {Name}, Price: {Price.ToString("0.00")}" +
                   $"{(Currency == Currencies.SEK ? "kr" : string.Empty)}" +
                   $"{(Currency == Currencies.GBP ? "£" : string.Empty)}" +
                   $"{(Currency == Currencies.USD ? "$" : string.Empty)}";
        }

        public double GetPriceInCurrency(string currency)
        {
            double result;
            switch (currency)
            {
                case "dollar":
                    result = Price * 0.09;
                    break;
                case "pounds":
                    result = Price * 0.08;
                    break;
                default:
                    result = Price;
                    break;
            }
            return Math.Round(result, 2);
        }
    }
}
