using System;
using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        static void Main()
        {
            using (var salesContext = new SalesDbContext())
            {
                salesContext.Database.EnsureCreated();
            }
        }
    }
}
