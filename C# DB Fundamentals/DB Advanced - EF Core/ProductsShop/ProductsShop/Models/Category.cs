using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductsShop.Models
{
    public class Category
    {
        public Category()
        {
            this.Products = new List<CategoryProduct>();
        }

        public int CategoryId { get; set; }

        [MaxLength(15), MinLength(3)]
        public string Name { get; set; }

        public ICollection<CategoryProduct> Products { get; set; }
    }
}
