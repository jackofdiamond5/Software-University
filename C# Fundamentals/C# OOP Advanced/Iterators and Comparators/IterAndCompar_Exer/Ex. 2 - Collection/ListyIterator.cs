using System;
using System.Collections;
using System.Collections.Generic;

public class ListyIterator<T> : IEnumerable<T>
{
    private List<T> items;
    private int index;

    public ListyIterator(List<T> items)
    {
        this.items = items;
    }

    public bool Move()
    {
        if (HasNext())
        {
            this.index++;
            return true;
        }

        return false;
    }
    
    public void Print()
    {
        if(this.items.Count == 0)
        {
            throw new InvalidOperationException("Invalid Operation!");
        }

        Console.WriteLine(this.items[this.index]);
    }

    public bool HasNext()
    {
        return this.index < this.items.Count - 1;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for(var i = 0; i < this.items.Count; i++)
        {
            yield return this.items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public void PrintAll()
    {
        foreach(var item in this)
        {
            Console.Write($"{item} ");
        }
    }
}
