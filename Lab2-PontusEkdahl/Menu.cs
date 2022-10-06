using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
            Currency,
            Checkout,
            Logout,
            NotSelected
        }

        private static List<Product> ProductStock = new List<Product>()
        {
            new Product("Lamp Oil", 34.99,
                "To light the way in the dark of night."),
            
            new Product("Oil Lamp", 129.99,
                "Genie not included."),

            new Product("Potentially Flying Carpet", 899.99,
                "It might fly. It might not. Buy at your own risk."),

            new Product("Magic(?) Flute", 399.99,
                "Some say it's magical. Most people don't but SOME do."),

            new Product("Jar of Red Sand", 5.99,
                "Looks pretty cool, but otherwise completely useless."),

            new Product("Assorted Spices", 19.99,
                "For when you're tired of bland tasting food."),

            new Product("Snake Oil", 14.99,
                "Literally snake oil.")
        };
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
            new Option("Change currency", MenuItems.Currency),
            new Option("Checkout", MenuItems.Checkout),
            new Option("Logout", MenuItems.Logout)
        };
        /*
         * ======================================== START MENU =========================================
         */
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
                        LoginMenu();
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
        /*
         * ========================================== LOGIN & REGISTER ================================================
         */
        public static void LoginMenu()
        {
            var inputName = string.Empty;
            var inputPassword = string.Empty;
            var customerList = GetCustomersFromFile();

            Console.Clear();

            Console.Write("Please enter your username: ");
            inputName = Console.ReadLine();
            if (customerList.All(customer => customer.Name.ToLower() != inputName.ToLower()))
            {
                Console.Clear();
                Console.WriteLine("Username does not exist.");
                Thread.Sleep(1500);
                return;
            }

            var currentUser = customerList.Single(customer => customer.Name.ToLower() == inputName.ToLower());

            Console.Write("Please input your password: ");
            inputPassword = Console.ReadLine();
            if (currentUser.Password == inputPassword)
            {
                MainMenu(currentUser);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid password!");
                Thread.Sleep(1500);
            }

        }

        public static void RegisterMenu()
        {
            var inputName = string.Empty;
            var inputPassword = string.Empty;

            Console.Clear();

            Console.Write("Please enter your account name (ONLY A-Z & NUMBERS ALLOWED): ");
            inputName = Console.ReadLine();
            if (CheckIfNameExists(inputName)
                || String.IsNullOrEmpty(inputName)
                || !Regex.IsMatch(inputName, @"^[a-zA-Z0-9]+$"))
            {
                Console.Clear();
                Console.WriteLine("Name is not available or empty.");
                Thread.Sleep(1500);
                return;
            }

            Console.Write("Please enter account password (ONLY A-Z & NUMBERS ALLOWED): ");
            inputPassword = Console.ReadLine();
            if (String.IsNullOrEmpty(inputPassword) ||
                !Regex.IsMatch(inputPassword, @"^[a-zA-Z0-9]+$"))
            {
                Console.Clear();
                Console.WriteLine("Password is empty or contains forbidden characters.");
                Thread.Sleep(1500);
                return;
            }

            Console.WriteLine("Please select your membership tier.");
            Console.WriteLine("'N' = NONE | 'B' = BRONZE | 'S' = SILVER | 'G' = GOLD");
            var tier = RegisterTier();
            SaveCustomerToFile(inputName, inputPassword, tier);
        }
        /*
         * ======================================= MAIN MENU ===========================================
         */
        public static void MainMenu(Customer currentUser)
        {
            var index = 0;
            var shutDown = false;
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                var nextMenu = MenuItems.NotSelected;
                PrintMenu("\\menutexts\\mainmenu.txt", MainOptions, MainOptions[index]);
                Console.WriteLine($"\nLogged in as {currentUser.Name}");
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < MainOptions.Count - 1)
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
                        nextMenu = MainOptions[index].MenuItem;
                        break;
                }

                switch (nextMenu)
                {
                    case MenuItems.Shop:
                        currentUser.ShoppingCart.AddRange(ShopMenu());
                        break;
                    case MenuItems.Cart:
                        CartMenu(currentUser.ShoppingCart);
                        break;
                    case MenuItems.Currency:
                        var newCurrency = CurrencyMenu();
                        ChangeCurrencyInList(newCurrency, ProductStock);
                        ChangeCurrencyInList(newCurrency, currentUser.ShoppingCart);
                        break;
                    case MenuItems.Checkout:
                        CheckoutMenu(currentUser);
                        break;
                    case MenuItems.Logout:
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
        /*
         * ======================================= SHOP ===============================================
         */

        public static List<Product> ShopMenu()
        {
            var index = 0;
            var cart = new List<Product>();
            var keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.X)
            {
                PrintMenu("\\menutexts\\shopmenu.txt", ProductStock, ProductStock[index]);
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < ProductStock.Count - 1)
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
                        var amount = BuyProduct(ProductStock[index]);
                        AddProductsToCart(ref cart, ProductStock[index], amount);
                        break;
                }
            }
            return cart;
        }

        public static int BuyProduct(Product product)
        {
            var amount = 0;
            Console.Clear();
            Console.WriteLine($"Ah! So you're interested in {product.Name}?\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(product.Description + "\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"That will be {(product.Price).ToString("0.00")}{Product.GetCurrencySymbol(product.Currency)}\n");

        Console.Write("Enter amount you want to buy: ");
            if (!int.TryParse(Console.ReadLine(), out amount))
            {
                Console.Clear();
                Console.WriteLine("Invalid input! Enter amount in integers ONLY!");
                Thread.Sleep(1500);
            }
            return amount;
        }

        public static void AddProductsToCart(ref List<Product> cart, Product product, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                cart.Add(product);
            }
        }
        /*
         * ============================================== CURRENCY MENU =====================================================
         */
        public static Product.Currencies CurrencyMenu()
        {
            var index = 0;
            var currencyArr = Enum.GetValues<Product.Currencies>();
            var keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                PrintMenu(currencyArr, currencyArr[index]);
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < currencyArr.Length - 1)
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
                }
            }
            return currencyArr[index];
        }

        public static void ChangeCurrencyInList(Product.Currencies newCurrency, List<Product> productList)
        {
            foreach (var product in productList)
            {
                product.Currency = newCurrency;
            }
        }
        /*
         * ============================================= CART MENU ===========================================================
         */
        public static void CartMenu(List<Product> cart)
        {
            Console.Clear();
            Console.WriteLine("Your cart currently contains: \n");
            foreach (var stockProd in ProductStock)
            {
                var amount = cart.Count(cartProd => cartProd == stockProd);
                if (amount > 0)
                {
                    Console.WriteLine($"\nYou have {amount} {stockProd}");
                    Console.WriteLine(
                        $"Total price of your {stockProd.Name}: {(stockProd.Price * amount).ToString("0.00")}" +
                        Product.GetCurrencySymbol(stockProd.Currency));
                }
            }
            Console.WriteLine("\nPress ANY key to return to main menu.");
            Console.ReadKey();
        }
        /*
         * ============================================= CHECKOUT MENU ========================================================
         */
        public static void CheckoutMenu(Customer currentUser)
        {
            var sumOfPrice = currentUser.ShoppingCart
                .Sum(p => p.Price);
            var currency = ProductStock.First().Currency;

            Console.Clear();
            Console.WriteLine($"The sum of your items is {(sumOfPrice).ToString("0.00")}" + Product.GetCurrencySymbol(currency));
            Console.ReadKey();
        }
        public static bool CheckIfNameExists(string input)
        {
            var customerList = GetCustomersFromFile();
            var inputExists = customerList.Any(customer => customer.Name.ToLower() == input.ToLower());
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
            var stringSeparators = new string[] { "Name: ", ", Password: ", ", Tier: ", };
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

        public static void PrintMenu(string pathToFile, List<Product> productStock, Product selected)
        {
            Console.Clear();

            var path = GetPathToResourcesFolder();
            path += pathToFile;
            var text = (File.ReadAllText(path));

            Console.WriteLine(text);

            foreach (var product in productStock)
            {
                if (product == selected)
                {
                    Console.Write(">");
                }
                Console.WriteLine(product);
            }
        }

        public static void PrintMenu(Product.Currencies[] currencies, Product.Currencies selected)
        {
            Console.Clear();

            foreach (var currency in currencies)
            {
                if (currency == selected)
                {
                    Console.Write(">");
                }
                Console.WriteLine(currency);
            }
        }
    }
}