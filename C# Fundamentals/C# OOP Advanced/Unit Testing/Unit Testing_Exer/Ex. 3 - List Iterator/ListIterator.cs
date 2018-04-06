using System;
using System.Collections.Generic;

public class ListIterator
{
    private int index = 0;
    private List<string> items;

    public ListIterator(List<string> items)
    {
        this.Items = items;
    }

    public List<string> Items
    {
        get { return this.items; }
        private set
        {
            foreach (var item in value)
            {
                if (item == null)
                {
                    throw new ArgumentNullException();
                }
            }

            this.items = value;
        }
    }

    public bool HasNext()
    {
        return this.index < this.items.Count - 1;
    }

    public bool Move()
    {
        if (HasNext())
        {
            index++;
            return true;
        }

        return false;
    }

    public void Print()
    {
        if (this.Items.Count == 0)
        {
            throw new InvalidOperationException("Invalid Operation!");
        }

        Console.WriteLine(this.Items[this.index]);
    }
}
