using System.Linq;

public class BubbleSort
{
    public BubbleSort(int[] collection)
    {
        this.Collection = collection;
    }

    public int[] Collection { get; private set; }

    public void Sort()
    {
        while (this.Collection.First() != this.Collection.Min() ||
                this.Collection.Last() != this.Collection.Max())
        {
            for (var i = 0; i < this.Collection.Length - 1; i++)
            {
                if (this.Collection[i] > this.Collection[i + 1])
                {
                    var temp = this.Collection[i];
                    this.Collection[i] = this.Collection[i + 1];
                    this.Collection[i + 1] = temp;
                }
            }
        }
    }
}
