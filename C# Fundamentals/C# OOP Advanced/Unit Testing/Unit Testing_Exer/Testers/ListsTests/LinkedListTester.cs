using NUnit.Framework;

using CustomLinkedList;

public class LinkedListTester
{
    DynamicList<int> list;

    [SetUp]
    public void CreateList()
    {
        this.list = new DynamicList<int>();
    }

    [TearDown]
    public void DestroyList()
    {
        this.list = null;
    }

    [Test]
    public void AccessingElementAtIndexMoreOrEqualtoElementsCountThrowsException()
    {
        var outOfRangeIndex = 3;

        list.Add(1);
        list.Add(2);
        list.Add(3);

        Assert.That(() => list[outOfRangeIndex],
            Throws.Exception
            .With
            .Message
            .EndsWith("Invalid index: " + outOfRangeIndex));
    }

    [Test]
    public void AccessingElementAtNegativeIndexThrowsException()
    {
        var outOfRangeIndex = -1;

        Assert.That(() => list[outOfRangeIndex],
            Throws.Exception
            .With
            .Message
            .EndsWith("Invalid index: " + outOfRangeIndex));
    }

    [Test]
    public void AddingElementPutsItAtTheEndOfTheList()
    {
        var expectedValue = 2;

        list.Add(1);
        list.Add(2);

        Assert.That(() => list[1],
            Is.EqualTo(expectedValue));
    }

    [Test]
    public void AttemptingToRemoveItemAtIndexGreaterOrEqualToElementsCountThrowsException()
    {
        var outOfRangeIndex = 3;

        list.Add(1);
        list.Add(2);
        list.Add(3);
        
        Assert.That(() => list.RemoveAt(outOfRangeIndex),
            Throws.Exception
            .With
            .Message
            .EndsWith("Invalid index: " + outOfRangeIndex));
    }

    [Test]
    public void AttemptingToRemoveElementAtNegativeIndexThrowsException()
    {
        var outOfRangeIndex = -1;

        Assert.That(() => list.RemoveAt(outOfRangeIndex),
           Throws.Exception
           .With
           .Message
           .EndsWith("Invalid index: " + outOfRangeIndex));
    }

    [Test]
    public void RemoveAt_RemovesItemAtGivenIndex()
    {
        var indexToRemoveAt = 1;
        var expectedValue = 3;

        list.Add(1);
        list.Add(2);
        list.Add(3);

        list.RemoveAt(indexToRemoveAt);

        Assert.That(list[indexToRemoveAt], 
            Is.EqualTo(expectedValue));
    }

    [Test]
    public void Remove_RemovesGivenElement()
    {
        list.Add(1);
        list.Add(2);
        list.Add(3);

        var indexToAttemptAccessing = 2;
        var elementToRemove = 3;

        var removed = list.Remove(elementToRemove);

        Assert.That(() => list[indexToAttemptAccessing],
            Throws.Exception);
    }

    [Test]
    public void Remove_ReturnsMinusOneIfElementNotFound()
    {
        var expectedReturnValue = -1;

        list.Add(1);

        var result = list.Remove(2);

        Assert.That(result, Is.EqualTo(expectedReturnValue));
    }

    [Test]
    public void IndexOf_ReturnsFirstIndexOfGivenElement()
    {
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(3);

        var expectedReturnValue = 2;

        var result = list.IndexOf(3);

        Assert.That(result, Is.EqualTo(expectedReturnValue));
    }

    [Test]
    public void IndexOf_ReturnsMinusOneIfElementNotFound()
    {
        var expectedReturnValue = -1;

        list.Add(1);
        list.Add(2);
        list.Add(3);

        var result = list.IndexOf(4);

        Assert.That(result, Is.EqualTo(expectedReturnValue));
    }

    [Test]
    public void Contains_ReturnsTrueIfElementExistsWithinList()
    {
        list.Add(1);

        Assert.That(() => list.Contains(1).Equals(true));
    }

    [Test]
    public void Contains_ReturnsFalseIfElementDoesNotExistWithinList()
    {
        list.Add(1);

        Assert.That(() => list.Contains(2).Equals(false));
    }
}
