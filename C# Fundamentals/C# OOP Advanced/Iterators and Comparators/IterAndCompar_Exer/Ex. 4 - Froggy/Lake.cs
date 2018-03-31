using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lake<T> : IEnumerable<T>
{
    private List<T> collection;

    public Lake(List<T> elements)
    {
        this.collection = elements;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for(var i = 0; i < this.collection.Count; i++)
        {
            yield return this.collection[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

