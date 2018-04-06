using System;
using System.Linq;

public class Database
{
    private const int MaxArrSize = 16;

    public Database(params int?[] initialValues)
    {
        this.values = new int?[MaxArrSize];
        this.Values = initialValues;
    }

    private int?[] values;

    public int?[] Values
    {
        get { return this.values; }
        private set
        {
            if (value.Length > MaxArrSize)
            {
                throw new InvalidOperationException();
            }

            for (var i = 0; i < value.Length; i++)
            {
                this.values[i] = value[i];
            }
        }
    }

    public void Add(int value)
    {
        if (this.values.All(v => v != null))
        {
            throw new InvalidOperationException();
        }

        var lastElementIndex = 0;
        foreach (var num in this.values)
        {
            if (num != null)
                continue;

            lastElementIndex = Array.IndexOf(this.values, num);
            break;
        }

        this.values[lastElementIndex] = value;
    }

    public void Remove()
    {
        if (this.values.All(v => v == null))
        {
            throw new InvalidOperationException();
        }

        for (var i = this.values.Length - 1; i > 0; i--)
        {
            if (this.values[i] == null)
                continue;

            this.values[i] = null;
            break;
        }
    }

    public int?[] Fetch()
    {
        return this.values.Where(n => n != null).ToArray();
    }
}
