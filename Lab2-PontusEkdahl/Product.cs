using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl
{
    public class Product
    {
        public string Name { get; private set; }

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
                        return Math.Round(_price * 0.09, 2);
                    case Currencies.GBP:
                        return Math.Round(_price * 0.08, 2);
                }
                return _price;

            }
            set { _price = value; }
        }

        public string Description { get; private set; }

        public enum Currencies
        {
            SEK,
            USD,
            GBP
        }

        public Currencies Currency { get; set; }

        public Product(string name, double price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
            Currency = Currencies.SEK;
        }

        public override string ToString()
        {
            return $"{Name}, Price: {Price.ToString("0.00")}" + GetCurrencySymbol(Currency);
        }

        public static string GetCurrencySymbol(Currencies currency)
        {
            return $"{(currency == Currencies.SEK ? "kr" : string.Empty)}" +
                   $"{(currency == Currencies.GBP ? "£" : string.Empty)}" +
                   $"{(currency == Currencies.USD ? "$" : string.Empty)}";
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
