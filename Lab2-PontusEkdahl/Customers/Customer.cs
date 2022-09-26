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
        }

        public int Discount { get; protected set; }
    }
}
