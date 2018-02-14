public class Rectangle
{
    private string id;
    private double width;
    private double height;
    private double topLeftX;
    private double topLeftY;

    public Rectangle(string id, double width, double height, double topLeftX, double topLeftY)
    {
        this.Id = id;
        this.Width = width;
        this.Height = height;
        this.TopLeftX = topLeftX;
        this.TopLeftY = topLeftY;
    }

    public string Id { get => id; set => id = value; }

    public double Width { get => width; set => width = value; }

    public double Height { get => height; set => height = value; }

    public double TopLeftX { get => topLeftX; set => topLeftX = value; }

    public double TopLeftY { get => topLeftY; set => topLeftY = value; }


    public static bool RectanglesIntersect(Rectangle first, Rectangle second)
    {
        return first.ContainsRectangle(second) || second.ContainsRectangle(first);
    }

    private bool ContainsRectangle(Rectangle rectangle)
    {
        return this.ContainsPoint(rectangle.TopLeftX, rectangle.TopLeftY) ||
               this.ContainsPoint(rectangle.TopLeftX, rectangle.TopLeftY + height) ||
               this.ContainsPoint(rectangle.TopLeftX + width, rectangle.TopLeftY) ||
               this.ContainsPoint(rectangle.TopLeftX + width, rectangle.TopLeftY + height);
    }

    private bool ContainsPoint(double x, double y)
    {
        return x >= this.TopLeftX && x <= this.TopLeftX + width &&
               y >= this.TopLeftY && y <= this.TopLeftY + height;
    }
}