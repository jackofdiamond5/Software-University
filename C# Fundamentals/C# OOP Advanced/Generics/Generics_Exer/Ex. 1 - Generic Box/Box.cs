public class Box<T>
{
    public Box(T value)
    {
        this.ValueHolder = value;
    }

    public T ValueHolder { get; }

    public override string ToString()
    {
        return $"{this.ValueHolder.GetType().FullName}: {this.ValueHolder}";
    }
}
