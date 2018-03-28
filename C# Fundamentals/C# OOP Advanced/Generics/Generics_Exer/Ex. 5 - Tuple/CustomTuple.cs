public class CustomTuple<T, T1>
{
    public CustomTuple(T item1, T1 item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }

    public T Item1 { get; }

    public T1 Item2 { get; }

    public override string ToString()
    {
        return $"{this.Item1} -> {this.Item2}";
    }
}
