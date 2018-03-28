using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CustomList<T> : IEnumerable, IEnumerable<T>
    where T : IComparable
{
    private IList<T> list;

    public CustomList()
    {
        this.list = new List<T>();
    }

    public void Add(T element)
    {
        this.list.Add(element);
    }

    public T Remove(int index)
    {
        var removedElement = this.list[index];
        list.RemoveAt(index);

        return removedElement;
    }

    public bool Contains(T element)
    {
        return list.Contains<T>(element);
    }

    public void Swap(int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    public int CountGreaterThan(T element)
    {
        return list.Count(e => e.CompareTo(element) > 0);
    }

    public T Max()
    {
        return list.Max();
    }

    public T Min()
    {
        return list.Min();
    }

    public CustomList<T> Sort()
    {
        var sorted = list.OrderBy(t => t).ToList();
        var customList = new CustomList<T>();

        foreach(var item in sorted)
        {
            customList.Add(item);
        }

        return customList;
    }

    public IEnumerator GetEnumerator()
    {
        return ((IEnumerable)list).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return list.GetEnumerator();
    }
}
