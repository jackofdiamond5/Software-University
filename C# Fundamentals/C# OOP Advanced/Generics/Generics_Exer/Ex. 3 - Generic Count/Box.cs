using System;

public class Box<T> : IComparable, IComparable<T>
    where T : IComparable
{
    public Box(T value)
    {
        this.ValueHolder = value;
    }

    public T ValueHolder { get; }
    
    public int CompareTo(T other)
    {
        if (this.ValueHolder.GetType() != other.GetType())
        {
            return 0;
        }

        if (this.ValueHolder.CompareTo(other) == 0)
        {
            return 0;
        }

        if(this.ValueHolder.CompareTo(other) == -1)
        {
            return -1;
        }

        if(this.ValueHolder.CompareTo(other) == 1)
        {
            return 1;
        }

        throw new ArgumentException();
    }

    public int CompareTo(object obj)
    {
        if (this.ValueHolder.GetType() != obj.GetType())
        {
            return 0;
        }

        if (this.ValueHolder.CompareTo(obj) == 0)
        {
            return 0;
        }

        if (this.ValueHolder.CompareTo(obj) == -1)
        {
            return -1;
        }

        if (this.ValueHolder.CompareTo(obj) == 1)
        {
            return 1;
        }

        throw new ArgumentException();
    }

    public override string ToString()
    {
        return $"{this.ValueHolder.GetType().FullName}: {this.ValueHolder}";
    }
}
