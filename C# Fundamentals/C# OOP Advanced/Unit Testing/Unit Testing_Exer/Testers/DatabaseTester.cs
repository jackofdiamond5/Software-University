using NUnit.Framework;
using System.Collections.Generic;

namespace DatabseTests
{
    public class DatabaseTester
    {
        [Test]
        public void DatabaseCapcityCannotGoAboveSixteenElements()
        {
            var items = new List<int?>();
            for(var i = 0; i < 16; i++)
            {
                items.Add(i);
            }

            var database = new Database(items.ToArray());

            Assert
                .That(() => database.Add(17), 
                Throws.InvalidOperationException);
        }

        [Test]
        public void InitialDatabaseCapacityMustBeLessThanOrEqualToSixteen()
        {
            var items = new List<int?>();
            for (var i = 0; i <= 16; i++)
            {
                items.Add(i);
            }

            Assert
                .That(() => new Database(items.ToArray()), 
                Throws.InvalidOperationException);
        }

        [Test]
        public void CannotRemoveElementFromEmptyDatabase()
        {
            var database = new Database();

            Assert
                .That(() => database.Remove(), 
                Throws.InvalidOperationException);
        }
    }
}