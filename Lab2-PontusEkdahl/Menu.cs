using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab2_PontusEkdahl.Customers;

namespace Lab2_PontusEkdahl
{
    public static class Menu
    {
        public enum MenuItems
        {
            Login,
            Register,
            Exit,
            Shop,
            Cart,
            Checkout,
            Logout,
            NotSelected
        }
        private static readonly List<Option> StartOptions = new List<Option>()
        {
            new Option("Login", MenuItems.Login),
            new Option("Register", MenuItems.Register),
            new Option("Exit program", MenuItems.Exit)
        };
        private static readonly List<Option> MainOptions = new List<Option>()
        {
            new Option("Shop", MenuItems.Shop),
            new Option("Check cart", MenuItems.Cart),
            new Option("Checkout", MenuItems.Checkout),
            new Option("Logout", MenuItems.Logout)
        };
        public static void StartMenu()
        {
            var index = 0;
            var nextMenu = MenuItems.NotSelected;
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                PrintMenu("\\menutexts\\startmenu.txt",StartOptions, StartOptions[index]);
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < StartOptions.Count - 1)
                        {
                            index++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (index > 0)
                        {
                            index--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        nextMenu = StartOptions[index].MenuItem;
                        break;
                }

                switch (nextMenu)
                {
                    case MenuItems.Login:
                        break;
                    case MenuItems.Register:
                        break;
                    case MenuItems.Exit:
                        break;
                    default:
                        break;
                }
            }
        }

        public static void LoginMenu(List<Customer>  registeredCustomers)
        {

        }

        public static void RegisterMenu()
        {

        }

        public static void SaveCustomerToFile()
        {

        }

        public static void PrintMenu()
        {

        }

        public static string GetPathToResources()
        {
            var resourcesPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\resources"));
            return resourcesPath;
        }

        public static void PrintMenu(string pathToFile,List<Option> options, Option selected)
        {
            Console.Clear();

            var path = GetPathToResources();
            path += pathToFile;
            var text = (File.ReadAllLines(path));

            foreach (var str in text)
            {
                Console.WriteLine(str);
            }

            foreach (var option in options)
            {
                if (option == selected)
                {
                    Console.Write(">");
                }
                Console.WriteLine(option.Name);
            }
        }
    }
}