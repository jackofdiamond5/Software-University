using System.Reflection;
using System.Collections.Generic;

using NUnit.Framework;

public class ListIteratorTests
{
    [Test]
    public void MoveIncreasesInternalIndex()
    {
        var iterator = new ListIterator(new List<string>() { "1", "2", "3" });

        iterator.Move();

        var iteratorIndex = iterator
            .GetType()
            .GetField("index", 
            BindingFlags.NonPublic |
            BindingFlags.Instance);

        Assert.That(iteratorIndex.GetValue(iterator), Is.EqualTo(1));
    }

    [Test]
    public void PrintThrowsExceptionIfCalledOnEmptyCollection()
    {
        var iterator = new ListIterator(new List<string>());

        Assert.That(() => iterator.Print(),
            Throws.InvalidOperationException);
    }

    [Test]
    public void HasNextReturnsFalseIfIndexAtLastElement()
    {
        var iterator = new ListIterator(new List<string>() { "1" });

        iterator.Move();

        Assert.That(() => iterator.HasNext(), Is.EqualTo(false));
    }
}
