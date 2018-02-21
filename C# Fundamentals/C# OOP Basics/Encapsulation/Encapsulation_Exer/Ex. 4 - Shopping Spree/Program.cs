using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        try
        {
            var persons = Console.ReadLine().Split(';');
            var products = Console.ReadLine().Split(';');

            var allCustomers = new List<Person>();
            foreach (var person in persons)
            {
                if (string.IsNullOrEmpty(person))
                    continue;

                var currentPersonData = person.Split('=');
                var currentCustomerName = currentPersonData[0];
                var currentCustomerMoney = decimal.Parse(currentPersonData[1]);

                allCustomers.Add(new Person(currentCustomerName, currentCustomerMoney));
            }

            var allProducts = new List<Product>();
            foreach (var product in products)
            {
                if (string.IsNullOrEmpty(product))
                    continue;

                var currentProductData = product.Split('=');
                var currentProductName = currentProductData[0];
                var currentProductPrice = decimal.Parse(currentProductData[1]);

                allProducts.Add(new Product(currentProductName, currentProductPrice));
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                var data = command.Split();

                var currentCustomerName = data[0];
                var currentCustomerProduct = data[1];

                var customer = allCustomers.Single(p => p.Name == currentCustomerName);
                var orderedProduct = allProducts.Single(p => p.Name == currentCustomerProduct);

                if (customer.Money < orderedProduct.Price)
                {
                    Console.WriteLine($"{customer.Name} can't afford {currentCustomerProduct}");
                }
                else
                {
                    customer.AddProduct(orderedProduct);
                    customer.SubstractMoney(orderedProduct.Price);

                    Console.WriteLine($"{customer.Name} bought {orderedProduct.Name}");
                }
            }

            foreach (var customer in allCustomers)
            {
                if (customer.Products.Count == 0)
                {
                    Console.WriteLine($"{customer.Name} - Nothing bought");
                }
                else
                {
                    var boughtProductsNames = customer.Products.Select(p => p.Name).ToArray();

                    Console.WriteLine($"{customer.Name} - {string.Join(", ", boughtProductsNames)}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}