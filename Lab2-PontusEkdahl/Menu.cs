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
        public enum MenuItems // enum för menyval
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

        private static List<Product> ProductStock = new List<Product>() // alla produkter som erbjuds
        {
            new Product("Canister of Lamp Oil", "Canisters of Lamp Oil", 34.99,
                "To light the way in the dark of night."),

            new Product("Big Round Bomb", "Big Round Bombs", 49.99,
                "Best way to rid your path of any pesky boulders."),

            new Product("Length of Rope", "Length's of Rope", 19.99,
                "You can never have too much rope!"),

            new Product("Oil Lamp", "Oil Lamps", 129.99,
                "Genie not included."),

            new Product("Potentially Flying Carpet", "Potentially Flying Carpets", 899.99,
                "It might fly. It might not. Buy at your own risk."),

            new Product("Magic(?) Flute", "Magic(?) Flutes", 399.99,
                "Some say it's magical. Most people don't but SOME do."),

            new Product("Jar of Red Sand", "Jars of Red Sand", 5.99,
                "Looks pretty cool, but otherwise completely useless."),

            new Product("Bag of Assorted Spices", "Bags of Assorted Spices", 19.99,
                "For when you're tired of bland tasting food."),

            new Product("Vial of Snake Oil", "Vials of Snake Oil", 14.99,
                "Literally snake oil.")
        };
        private static readonly List<Option> StartMenuOptions = new List<Option>() // menyval
        {
            new Option("Login", MenuItems.Login),
            new Option("Register", MenuItems.Register),
            new Option("Exit program", MenuItems.Exit)
        };

        private static readonly List<Option> MainMenuOptions = new List<Option>() // menyval
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
            var nextMenu = MenuItems.NotSelected;
            ConsoleKeyInfo keyInfo;
            while (nextMenu != MenuItems.Exit)
            {
                nextMenu = MenuItems.NotSelected;
                PrintMenu("\\menutexts\\startmenu.txt", StartMenuOptions, StartMenuOptions[index]);
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < StartMenuOptions.Count - 1)
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
                        nextMenu = StartMenuOptions[index].MenuItem;
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
                DisplayMessage("Username does not exist.");
                return;
            }

            var currentUser = customerList.Find(customer => customer.Name.ToLower() == inputName.ToLower());

            Console.Write("Please input your password: ");
            inputPassword = Console.ReadLine();

            if (currentUser.VerifyPassword(inputPassword))
            {
                MainMenu(currentUser);
            }
            else
            {
                DisplayMessage("Invalid password!");
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
                DisplayMessage("Name is not available or empty.");
                return;
            }

            Console.Write("Please enter account password (ONLY A-Z & NUMBERS ALLOWED): ");
            inputPassword = Console.ReadLine();
            if (String.IsNullOrEmpty(inputPassword) ||
                !Regex.IsMatch(inputPassword, @"^[a-zA-Z0-9]+$"))
            {
                DisplayMessage("Password is empty or contains forbidden characters.");
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
            var inputExists = customerList.Any(customer => customer.Name.ToLower() == input.ToLower());
            return inputExists;
        }
        public static Customer.MembershipTier RegisterTier()
        {
            var tier = Customer.MembershipTier.Unassigned;
            while (tier == Customer.MembershipTier.Unassigned)
            {
                var keyInfo = Console.ReadKey(true);
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
        /*
         * ======================================= MAIN MENU ===========================================
         */
        public static void MainMenu(Customer currentUser)
        {
            var index = 0;
            var nextMenu = MenuItems.NotSelected;
            ConsoleKeyInfo keyInfo;
            while (nextMenu != MenuItems.Logout)
            {
                nextMenu = MenuItems.NotSelected;
                PrintMenu("\\menutexts\\mainmenu.txt", MainMenuOptions, MainMenuOptions[index]);

                Console.WriteLine($"\nLogged in as {currentUser.Name}");
                Console.WriteLine($"Tier: {currentUser.GetTypeAsString()}");

                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (index < MainMenuOptions.Count - 1)
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
                        nextMenu = MainMenuOptions[index].MenuItem;
                        break;
                }

                switch (nextMenu)
                {
                    case MenuItems.Shop:
                        currentUser.ShoppingCart = ShopMenu(currentUser.ShoppingCart);
                        break;
                    case MenuItems.Cart:
                        CartMenu(currentUser);
                        break;
                    case MenuItems.Currency:
                        var newCurrency = CurrencyMenu();
                        ChangeCurrencyInList(newCurrency, ProductStock);
                        ChangeCurrencyInList(newCurrency, currentUser.ShoppingCart);
                        break;
                    case MenuItems.Checkout:
                        if (CheckoutMenu(currentUser))
                        {
                            currentUser.ShoppingCart.Clear();
                        }
                        break;
                }
            }
        }
        /*
         * ======================================= SHOP ===============================================
         */

        public static List<Product> ShopMenu(List<Product> cart)
        {
            var index = 0;
            var keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.X)
            {
                PrintMenu("\\menutexts\\shopmenu.txt", ProductStock, ProductStock[index]);
                keyInfo = Console.ReadKey(true);
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
                        if (amount + cart.Count(prod => prod == ProductStock[index]) > 10)
                        {
                            DisplayMessage("We don't have that much in stock!");
                        }
                        else
                        {
                            AddProductsToCart(ref cart, ProductStock[index], amount);
                        }
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

            Console.WriteLine($"That will be {(product.Price).ToString("0.00")}{Product.GetCurrencySymbol(product.Currency)} per {product.Name}\n");

            Console.Write("Enter amount you want to buy: ");
            if (!int.TryParse(Console.ReadLine(), out amount))
            {
                DisplayMessage("Invalid input! Enter amount in integers ONLY!");
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
                keyInfo = Console.ReadKey(true);
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
         * ================================================ CART MENU ===========================================================
         */
        public static void CartMenu(Customer customer)
        {
            if (customer.ShoppingCart.Count == 0)
            {
                Menu.DisplayMessage("Your cart is empty!");
                return;
            }
            customer.ViewCart();
            Console.WriteLine("\nPress ANY key to return to main menu.");
            Console.ReadKey(true);
        }
        /*
         * ============================================= CHECKOUT MENU ========================================================
         */
        public static bool CheckoutMenu(Customer currentUser)
        {
            var distinctCart = currentUser.ShoppingCart.Distinct();
            var keyInfo = new ConsoleKeyInfo();
            var totalPrice = currentUser.ShoppingCart
                .Sum(p => p.Price);
            var discountedPrice = totalPrice * currentUser.CalculateDiscount();
            var amountDiscounted = totalPrice - discountedPrice;

            if (currentUser.ShoppingCart.Count == 0)
            {
                DisplayMessage("Your cart is empty!");
                return false;
            }

            Console.Clear();

            if (currentUser is not BasicMember)
            {
                Console.WriteLine($"\nWow! Because you're a {currentUser.GetTypeAsString()} Member you get a {currentUser.Discount}% discount!\n" +
                                  $"You just saved {amountDiscounted:0.00}{Product.GetCurrencySymbol(ProductStock.First().Currency)}!\n" +
                                  $"The price before your discount was {totalPrice:0.00}{Product.GetCurrencySymbol(ProductStock.First().Currency)}");
            }

            Console.WriteLine($"\nThe total price is: {discountedPrice:0.00}{Product.GetCurrencySymbol(ProductStock.First().Currency)}\n\n" +
                              $"PRESS 'Y' TO CONFIRM PURCHASE\n" +
                              $"PRESS 'N' TO EXIT WITHOUT PURCHASING");

            while (keyInfo.Key != ConsoleKey.Y && keyInfo.Key != ConsoleKey.N)
            {
                keyInfo = Console.ReadKey(true);
            }

            if (keyInfo.Key == ConsoleKey.Y)
            {
                DisplayMessage($"Thank you, {currentUser.Name} for shopping with us!");
                DisplayMessage("The shop has been restocked!");
                if (currentUser.Name.ToLower() == "niklas")
                {
                    DisplayMessage("And thank you for being an amazing teacher!");
                    DisplayMessage("You're the best, Niklas!");
                    DisplayMessage("(snälla ge mig VG)");
                }
                return true;
            }

            return false;
        }
        /*
         * ============================================= GENERAL METHODS ==========================================================
         */
        public static void DisplayMessage(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
            Thread.Sleep(1500);
        }
        
        /*
         * ========================================== RESOURCE/FILE METHODS ================================================
         */
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
        /*
         * ========================================= PRINTMENU METHODS ============================================
         */
        public static void PrintMenu(string pathToFile, List<Option> options, Option selected) // FÖR INTRO OCH MAIN MENU
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

        public static void PrintMenu(string pathToFile, List<Product> productStock, Product selected) // FÖR SHOP MENU
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

        public static void PrintMenu(Product.Currencies[] currencies, Product.Currencies selected) // FÖR CURRENCIES MENU
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