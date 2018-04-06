using NUnit.Framework;

using CustomLinkedList;

public class LinkedListTester
{
    [Test]
    public void AccessingElementAtIndexMoreOrEqualtoElementsCountThrowsException()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.That(() => list[3],
            Throws.Exception.With.Message.EndsWith("Invalid index: " + 3));
    }

    [Test]
    public void AccessingElementAtNegativeIndexThrowsException()
    {
        var list = new DynamicList<int>();

        Assert.That(() => list[-1],
            Throws.Exception.With.Message.EndsWith("Invalid index: " + -1));
    }

    [Test]
    public void AddingElementPutsItAtTheEndOfTheList()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);

        Assert.That(() => list[1],
            Is.EqualTo(2));
    }

    [Test]
    public void AttemptingToRemoveItemAtIndexGreaterOrEqualToElementsCountThrowsException()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);
        
        Assert.That(() => list.RemoveAt(3),
            Throws.Exception.With.Message.EndsWith("Invalid index: " + 3));
    }

    [Test]
    public void AttemptingToRemoveElementAtNegativeIndexThrowsException()
    {
        var list = new DynamicList<int>();

        Assert.That(() => list.RemoveAt(-1),
           Throws.Exception.With.Message.EndsWith("Invalid index: " + -1));
    }

    [Test]
    public void RemoveAt_RemovesItemAtGivenIndex()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        list.RemoveAt(1);

        Assert.That(list[1], Is.EqualTo(3));
    }

    [Test]
    public void Remove_RemovesGivenElement()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        var removed = list.Remove(3);

        Assert.That(() => list[2], Throws.Exception);
    }

    [Test]
    public void Remove_ReturnsMinusOneIfElementNotFound()
    {
        var list = new DynamicList<int>();

        list.Add(1);

        var result = list.Remove(2);

        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void IndexOf_ReturnsFirstIndexOfGivenElement()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(3);
        
        var result = list.IndexOf(3);

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void IndexOf_ReturnsMinusOneIfElementNotFound()
    {
        var list = new DynamicList<int>();

        list.Add(1);
        list.Add(2);
        list.Add(3);

        var result = list.IndexOf(4);

        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public void Contains_ReturnsTrueIfElementExistsWithinList()
    {
        var list = new DynamicList<int>();

        list.Add(1);

        Assert.That(() => list.Contains(1).Equals(true));
    }

    [Test]
    public void Contains_ReturnsFalseIfElementDoesNotExistWithinList()
    {
        var list = new DynamicList<int>();

        list.Add(1);

        Assert.That(() => list.Contains(2).Equals(false));
    }
}
