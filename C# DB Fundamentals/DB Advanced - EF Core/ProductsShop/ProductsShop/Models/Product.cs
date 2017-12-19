using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class Product
    {
        public Product()
        {
            this.Categories = new List<CategoryProduct>();
        }

        public int ProductId { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ICollection<CategoryProduct> Categories { get; set; }
    }
}
