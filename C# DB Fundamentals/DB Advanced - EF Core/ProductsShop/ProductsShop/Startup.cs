using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;
using ProductsShop.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ProductsShop
{
    public class Startup
    {
        public static void Main()
        {
            var context = new ShopContext();

            using (context)
            {
                context.Database.EnsureCreated();

                //ImportUsersJson(context);
                //ImportProductsJson(context);
                //ImportCategoriesJson(context);

                //SetCategories(context);

                //ExportProductsInRangeJson(context);
                //ExportSoldProductsJson(context);
                //ExportCategoriesByCountJson(context);
                //ExportUsersAndProductsJson(context);

                

                //ImportUsersXml(context);
                //ImportProductsXml(context);
                //ImportCategoriesXml(context);
                //ExportProductsInRangeXml(context);
                //ExportSoldProductsXml(context);
                //ExportCategoriesByCountXml(context);
                //ExportUsersAndProductsXml(context);
            }
        }

        #region XML

        public static void ExportUsersAndProductsXml(ShopContext context)
        {
            var users = context.Users.Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                age = u.Age,
                soldProducts = new
                {
                    count = u.SoldProducts.Count,
                    products = u.SoldProducts.Select(sp => new
                    {
                        sp.Name,
                        sp.Price
                    })
                            .ToArray()
                }
            })
                .Where(u => u.soldProducts.count != 0)
                .OrderByDescending(u => u.soldProducts.count)
                .ThenBy(u => u.lastName)
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users",
                new XAttribute("count", users.Length)
                )
            );


            foreach (var dbUser in users)
            {
                xDoc.Element("users").Add(new XElement("user",
                    new XAttribute("first-name", dbUser.firstName ?? ""),
                    new XAttribute("last-name", dbUser.lastName ?? ""),
                    new XAttribute("age", dbUser.age ?? 0)
                    )
                );

                var curUser = xDoc
                    .Element("users")
                    .Elements("user")
                    .SingleOrDefault(u => u.IsEmpty);

                curUser.Add(new XElement("sold-products",
                    new XAttribute("count", dbUser.soldProducts.count)
                    )
                );

                foreach (var product in dbUser.soldProducts.products)
                {
                    curUser.Add(new XElement("product",
                        new XAttribute("name", product.Name ?? ""),
                        new XAttribute("price", product.Price)
                        )
                    );
                }

                xDoc.Element("users").Elements("user").Where(c => c.IsEmpty).Remove();
            }

            xDoc.Save("Exported/UsersAndProductsXml.xml");
        }

        public static void ExportCategoriesByCountXml(ShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    name = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = c.Products.Average(p => p.Product.Price),
                    totalRevenue = c.Products.Sum(p => p.Product.Price)
                })
                .OrderBy(c => c.name)
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("categories"));

            foreach (var category in categories)
            {
                xDoc.Element("category")
                   .Add(new XAttribute("name", category.name));

                var curElement = xDoc.Elements("categories").SingleOrDefault(c => c.IsEmpty);

                curElement.Add(new XElement("products-count", category.productsCount),
                        new XElement("average-price", category.averagePrice),
                        new XElement("total-revenue", category.totalRevenue)
                    );
            }

            xDoc.Save("Exported/CategoriesByCountXml.xml");
        }

        public static void ExportSoldProductsXml(ShopContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    SoldProducts = u.SoldProducts.Select(p => new
                    {
                        p.BuyerId,
                        p.Name,
                        p.Price,
                        p.Buyer.FirstName,
                        p.Buyer.LastName
                    })
                    .Where(p => p.BuyerId != null)
                    .ToArray()
                })
                .Where(u => u.SoldProducts.Length != 0)
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var aUser in users)
            {
                xDoc.Element("users").Add(new XElement("user",
                           new XAttribute("first-name", aUser.FirstName ?? ""),
                           new XAttribute("last-name", aUser.LastName ?? "")
                    )
                );

                var usersElements = xDoc.Elements("users").Elements("user")
                    .SingleOrDefault(u => u.IsEmpty);

                foreach (var aProduct in aUser.SoldProducts)
                {
                    if (aProduct.FirstName is null || aProduct.LastName is null ||
                        aProduct.BuyerId is null ||
                        aProduct.Name is null)
                        continue;

                    usersElements.Add(new XElement("product",
                            new XElement("name", aProduct.Name ?? ""),
                            new XElement("price", aProduct.Price)
                        )
                    );
                }

                xDoc.Element("users").Elements("user").Where(e => e.IsEmpty).Remove();
            }

            xDoc.Save("Exported/SoldProductsXml.xml");
        }

        public static void ExportProductsInRangeXml(ShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .ToArray();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("products"));

            foreach (var aProduct in products)
            {
                xDoc.Element("products").Add(
                                    new XElement("product",
                                        new XElement("name", aProduct.Name),
                                        new XElement("price", aProduct.Price),
                                        new XElement("buyer", aProduct.Buyer)
                                    )
                                );
            }

            xDoc.Save("Exported/ProductsInRange.xml");
        }

        public static void ImportCategoriesXml(ShopContext context)
        {
            var categoriesString = File.ReadAllText("categories.xml");
            var categoriesXml = XDocument.Parse(categoriesString);

            foreach (var category in categoriesXml.Root.Elements())
            {
                var dbCategory = new Category
                {
                    Name = category.Element("name").Value
                };

                context.Add(dbCategory);
            }

            context.SaveChanges();
        }

        public static void ImportProductsXml(ShopContext context)
        {
            var productsString = File.ReadAllText("products.xml");
            var productsXml = XDocument.Parse(productsString);

            var users = context.Users.Select(u => u.Id).ToArray();
            var counter = 0;

            foreach (var product in productsXml.Root.Elements())
            {
                if (counter >= users.Length - 1)
                {
                    counter = 1;
                }
                counter++;

                var dbProduct = new Product
                {
                    Name = product.Element("name")?.Value ?? null,
                    Price = decimal.Parse(product.Element("price").Value),
                    BuyerId = users[users.Length - counter],
                    SellerId = users[counter]
                };


                context.Products.Add(dbProduct);
            }

            context.SaveChanges();
        }

        public static void ImportUsersXml(ShopContext context)
        {
            var usersString = File.ReadAllText("users.xml");
            var usersXml = XDocument.Parse(usersString);

            foreach (var user in usersXml.Root.Elements())
            {
                var firstName = user.Attribute("firstName")?.Value ?? "";
                var lastName = user.Attribute("lastName")?.Value ?? "";
                var ageNotNull = int.TryParse(user.Element("age")?.Value, out int ageParsed);
                int? xmlAge = null;

                if (ageNotNull)
                {
                    xmlAge = ageParsed;
                }

                context.Add(new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = xmlAge
                });
            }

            context.SaveChanges();
        }

        #endregion

        public static void SetCategories(ShopContext context)
        {
            var products = context.Products
                .Select(p => p.ProductId)
                .ToArray();
            var categories = context.Categories
                .Select(c => c.CategoryId)
                .ToArray();

            var categoryProducts = new List<CategoryProduct>();

            var rnd = new Random();
            foreach (var product in products)
            {
                var categoryIndex = rnd.Next(0, categories.Length - 1);

                var categoryproduct = new CategoryProduct
                {
                    ProductId = product,
                    CategoryId = categories[categoryIndex]
                };

                categoryProducts.Add(categoryproduct);
            }

            context.SaveChanges();
        }

        #region JSON

        public static void ExportUsersAndProductsJson(ShopContext context)
        {
            var users = context.Users.Select(u => new
            {
                firstName = u.FirstName,
                lastName = u.LastName,
                age = u.Age,
                soldProducts = new
                {
                    count = u.SoldProducts.Count,
                    products = u.SoldProducts.Select(sp => new
                    {
                        sp.Name,
                        sp.Price
                    })
                    .ToArray()
                }
            })
            .Where(u => u.soldProducts.count != 0)
            .OrderByDescending(u => u.soldProducts.count)
            .ThenBy(u => u.lastName)
            .ToArray();

            var convertedUsers = JsonConvert.SerializeObject(users);

            var parsed = $"{{{"usersCount:" + users.Length},users:{convertedUsers}}}";

            var jsonSerialized = JsonConvert.DeserializeObject(parsed);

            var json = JsonConvert.SerializeObject(jsonSerialized, Formatting.Indented);

            File.WriteAllText("Exported/UsersAndProductsJson.json", json);
        }

        public static void ExportCategoriesByCountJson(ShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = c.Products.Average(p => p.Product.Price),
                    totalRevenue = c.Products.Sum(p => p.Product.Price)
                })
                .OrderBy(c => c.category)
                .ToArray();

            var json = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText("Exported/CategoriesCountJson.json", json);
        }

        public static void ExportSoldProductsJson(ShopContext context)
        {
            var products = context.Products
                .Select(p => new
                {
                    p.BuyerId,
                    p.Seller.FirstName,
                    p.Seller.LastName,
                    SoldProducts = context.Products.Select(sp => new
                    {
                        sp.Name,
                        sp.Price,
                        sp.Buyer.FirstName,
                        sp.Buyer.LastName
                    })
                    .ToArray()
                })
                .Where(p => p.BuyerId != null && p.SoldProducts.Length != 0)
                .ToArray();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("Exported/SoldProductsJson.json", json);
        }

        public static void ExportProductsInRangeJson(ShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    Seller = p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToArray();

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("Exported/ProductsInRangeJson.json", json);
        }

        public static void ImportCategoriesJson(ShopContext context)
        {
            var jCategories = JArray.Parse(File.ReadAllText("categories.json"));

            foreach (var jCategory in jCategories)
            {
                var category = JsonConvert.DeserializeObject<Category>(jCategory.ToString());

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        public static void ImportProductsJson(ShopContext context)
        {
            var jProducts = JArray.Parse(File.ReadAllText("products.json"));

            var users = context.Users.Select(u => u.Id).ToArray();
            var counter = 0;

            foreach (var jProduct in jProducts)
            {
                if (counter >= users.Length - 1)
                {
                    counter = 1;
                }
                counter++;

                var product = JsonConvert.DeserializeObject<Product>(jProduct.ToString(), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                product.BuyerId = users[users.Length - counter];
                product.SellerId = users[counter];

                context.Products.Add(product);
            }

            context.SaveChanges();
        }

        public static void ImportUsersJson(ShopContext context)
        {
            var jUsers = JArray.Parse(File.ReadAllText("users.json"));

            foreach (var jUser in jUsers)
            {
                var user = JsonConvert.DeserializeObject<User>(jUser.ToString());

                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        #endregion
    }
}
