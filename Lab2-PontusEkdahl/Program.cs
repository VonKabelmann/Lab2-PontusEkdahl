using Lab2_PontusEkdahl;
using Lab2_PontusEkdahl.Customers;
using static System.Net.Mime.MediaTypeNames;

Product product1 = new Product("Banana", 113);
product1.Currency = Product.Currencies.USD;
Console.WriteLine(product1);

Menu.StartMenu();
