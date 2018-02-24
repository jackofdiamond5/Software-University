using System;

public class Child : Person
{
    private string name;

    public Child(string name, int age)
        : base(name, age) { }


    protected override int Age
    {
        get => base.Age;
        set
        {
            if (value > 15)
            {
                throw new ArgumentException("Child's age must be less than 15!");
            }

            base.Age = value;
        }
    }
}

