using System;
using System.Collections.Generic;

public class ListyIterator<T>
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
}
