using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl
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
		}

        public int Discount { get; protected set; }
    }

    public class BasicMember : Customer
    {
        public BasicMember(string name, string password) : base(name, password)
        {
            Discount = 0;
        }
    }
    public class BronzeMember : Customer
    {
        public BronzeMember(string name, string password) : base(name, password)
        {
            Discount = 5;
        }
    }
    public class SilverMember : Customer
    {
        public SilverMember(string name, string password) : base(name, password)
        {
            Discount = 10;
        }
    }
    public class GoldMember : Customer
    {
        public GoldMember(string name, string password) : base(name, password)
        {
            Discount = 15;
        }
    }
}
