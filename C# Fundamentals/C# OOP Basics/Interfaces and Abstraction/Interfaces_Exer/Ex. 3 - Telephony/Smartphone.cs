using System;
using System.Linq;

class Smartphone : ICallable, IBrowsable
{
    private string url;
    private string number;

    public string Number
    {
        get => this.number;
        set
        {
            if (!value.Any(char.IsDigit))
            {
                throw new ArgumentException("Invalid number!");
            }

            this.number = value;
        }
    }

    public string Url
    {
        get => this.url;
        set
        {
            if (value.Any(char.IsDigit))
            {
                throw new ArgumentException("Invalid URL!");
            }

            this.url = value;
        }
    }
}
