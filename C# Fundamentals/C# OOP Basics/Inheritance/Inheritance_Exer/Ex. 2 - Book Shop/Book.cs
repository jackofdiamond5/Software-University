using System;
using System.Linq;
using System.Text;

public class Book
{
    private string title;
    private string author;
    private decimal price;

    public Book(string author, string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }

    protected string Title
    {
        get => title;
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("Title not valid!");
            }

            this.title = value;
        }
    }

    protected string Author
    {
        get => author;
        set
        {
            if (AuthorNameNotValid(value))
            {
                throw new ArgumentException("Author not valid!");
            }

            this.author = value;
        }
    }

    protected virtual decimal Price
    {
        get => price;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Price not valid!");
            }

            this.price = value;
        }
    }

    private static bool AuthorNameNotValid(string name)
    {
        bool result = false;

        var nameArgs = name.Split();

        if (nameArgs.Length > 1)
        {
            result = int.TryParse(nameArgs.Skip(1).First().ToCharArray()[0].ToString(), out int temp);
        }

        return result;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder
            .AppendLine($"Type: {this.GetType().Name}")
            .AppendLine($"Title: {this.Title}")
            .AppendLine($"Author: {this.Author}")
            .AppendLine($"Price: {this.Price:F2}");

        return builder.ToString().TrimEnd();
    }
}