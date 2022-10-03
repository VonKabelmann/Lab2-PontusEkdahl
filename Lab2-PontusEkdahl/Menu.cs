using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            var shutDown = false;
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                var nextMenu = MenuItems.NotSelected;
                PrintMenu("\\menutexts\\startmenu.txt", StartOptions, StartOptions[index]);
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
                        RegisterMenu();
                        break;
                    case MenuItems.Exit:
                        shutDown = true;
                        break;
                    default:
                        break;
                }

                if (shutDown)
                {
                    break;
                }
            }
        }

        public static void LoginMenu(List<Customer> registeredCustomers)
        {

        }

        //public static List<Customer> GetCustomersFromFile()
        //{

        //}

        public static void RegisterMenu()
        {
            var inputName = String.Empty;
            var inputPassword = string.Empty;

            Console.Clear();

            Console.Write("Please enter your account name: ");
            inputName = Console.ReadLine();
            if (CheckIfNameExists(inputName) || String.IsNullOrEmpty(inputName))
            {
                Console.Clear();
                Console.WriteLine("Name is not available or empty.");
                Thread.Sleep(1500);
                return;
            }

            Console.Write("Please enter account password: ");
            inputPassword = Console.ReadLine();
            if (String.IsNullOrEmpty(inputPassword))
            {
                Console.Clear();
                Console.WriteLine("Password must not be empty.");
                Thread.Sleep(1500);
                return;
            }

            Console.WriteLine("Please select your membership tier.");
            Console.WriteLine("'N' = NONE | 'B' = BRONZE | 'S' = SILVER | 'G' = GOLD");
            var tier = RegisterTier();
            SaveCustomerToFile(inputName, inputPassword, tier);
        }
        
        public static bool CheckIfNameExists(string input)
        {
            var customerList = GetCustomersFromFile();
            var inputExists = customerList.Any(a => a.Name == input);
            return inputExists;
        }
        

        public static Customer.MembershipTier RegisterTier()
        {
            ConsoleKeyInfo keyInfo;
            var tier = Customer.MembershipTier.Unassigned;
            while (tier == Customer.MembershipTier.Unassigned)
            {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.N:
                        tier = Customer.MembershipTier.Basic;
                        break;
                    case ConsoleKey.B:
                        tier = Customer.MembershipTier.Bronze;
                        break;
                    case ConsoleKey.S:
                        tier = Customer.MembershipTier.Silver;
                        break;
                    case ConsoleKey.G:
                        tier = Customer.MembershipTier.Gold;
                        break;
                }
            }

            return tier;
        }
        public static List<Customer> GetCustomersFromFile()
        {
            var path = GetPathToCustomersFile();
            var customersArr = File.ReadAllLines(path);
            var stringSeparators = new string[] { "Name: ", ", Password: ", ", Tier: " };
            var customerList = new List<Customer>();
            foreach (var str in customersArr)
            {
                var customerSplit = str.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                var tier = Customer.GetTierFromString(customerSplit[2]);
                var customer = Customer.CreateCustomer(customerSplit[0], customerSplit[1], tier);
                customerList.Add(customer);
            }
            return customerList;
        }



        public static void SaveCustomerToFile(string accName, string accPass, Customer.MembershipTier tier)
        {
            var path = GetPathToCustomersFile();
            var newCustomer = Customer.CreateCustomer(accName, accPass, tier);
            var customerList = GetCustomersFromFile();
            customerList.Add(newCustomer);
            var customerArr = new string[customerList.Count];
            var index = 0;
            foreach (var customer in customerList)
            {
                customerArr[index] = customer.ToString();
                index++;
            }
            File.WriteAllLines(path, customerArr);
        }

        public static string GetPathToCustomersFile()
        {
            var path = GetPathToResourcesFolder();
            return path += "\\lists\\customers.txt";
        }
        public static void PrintMenu()
        {

        }

        public static string GetPathToResourcesFolder()
        {
            var resourcesPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\resources"));
            return resourcesPath;
        }

        public static void PrintMenu(string pathToFile, List<Option> options, Option selected)
        {
            Console.Clear();

            var path = GetPathToResourcesFolder();
            path += pathToFile;
            var text = (File.ReadAllText(path));

            Console.WriteLine(text);

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