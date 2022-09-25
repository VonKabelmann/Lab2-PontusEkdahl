using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_PontusEkdahl
{
    public abstract class Menu
    {
        private enum MenuPages
        {
            StartMenu,
            LoginName,
            LoginPassword,
            RegisterName,
            RegisterPassword,
            MainMenu,
            Shop,
            Cart,
            Cashier
        }
        public static void GetMenu()
        {
            Console.WriteLine("Welcome! Would you like to login or register?");
        }
    }

    public class LoginMenu : Menu
    {
        
    }

    public class MainMenu : Menu
    {
        
    }
}
