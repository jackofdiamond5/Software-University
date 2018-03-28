public class ThreeUple<T, T1, T2>
{
    public ThreeUple(T item1, T1 item2, T2 item3)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
    }

    public T Item1 { get; set; }

    public T1 Item2 { get; set; }

    public T2 Item3 { get; set; }

    public override string ToString()
    {
        return $"{this.Item1} -> {this.Item2} -> {this.Item3}";
    }
}
