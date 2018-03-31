using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CustomStack<T> : IEnumerable<T>
{
    private List<T> collection;

    public CustomStack()
    {
        this.collection = new List<T>();
    }

    public void Push(params T[] elements)
    {
        foreach (var item in elements)
        {
            this.collection.Add(item); 

            if (this.collection.Count == 1)
            {
                continue;
            }

            if (this.collection.Count == 2)
            {
                this.collection.Reverse();
                continue;
            }

            for (var i = this.collection.Count - 1; i > 0; i--)
            {
                this.collection[i] = this.collection[i - 1];
            }

            this.collection[0] = item;
        }
    }

    public T Pop()
    {
        var element = this.collection.First();
        this.collection.Remove(element);
        return element;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < collection.Count; i++)
        {
            yield return this.collection[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
