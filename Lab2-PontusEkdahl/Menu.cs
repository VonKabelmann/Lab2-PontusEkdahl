using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl
{
    public class Menu
    {
        List<Option> _startOptions = new List<Option>()
        {
            new Option("Login", new Action(Menu.GetMenu)),
            new Option("Register", new Action(Menu.GetMenu))
        };
        List<Option> _mainOptions = new List<Option>()
        {
            new Option("Shop", new Action(Menu.GetMenu)),
            new Option("Check cart", new Action(Menu.GetMenu)),
            new Option("Cashier", new Action(Menu.GetMenu))
        };
        public static void GetMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Would you like to login or register?");
        }
    }
}