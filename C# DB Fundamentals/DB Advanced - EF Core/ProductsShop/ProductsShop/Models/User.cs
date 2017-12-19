using System.Collections.Generic;

namespace ProductsShop.Models
{
    public class User
    {
        public int Id { get; set; }

        public int? Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Product> BoughtProducts { get; set; }

        public ICollection<Product> SoldProducts { get; set; }
    }
}
