using System.Linq;
using NUnit.Framework;

public class BubbleTester
{
    [Test]
    public void BubbleTestSortsElements()
    {
        var items = new int[] { 5, -3, 11, 43, 0, 1, 3, 3, -56, 83 };
        var sorter = new BubbleSort(items);

        sorter.Sort();

        var collection = sorter.Collection;

        for (var i = 0; i < collection.Length - 1; i++)
        {
            Assert.That(collection[i], 
                Is.LessThanOrEqualTo(collection[i + 1]));
        }
    }
}