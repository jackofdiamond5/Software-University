using System;

public class Person : IComparable<Person>
{
    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get; }

    public int Age { get; }

    public override bool Equals(object obj)
    {
        var person = (Person)obj;
        return this.Name.Equals(person.Name) && this.Age.Equals(person.Age);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 13;
            hash = (hash * 7) + (!object.ReferenceEquals(null, this.Name) ? this.Name.GetHashCode() : 0);
            hash = (hash * 7) + (!object.ReferenceEquals(null, this.Age) ? this.Age.GetHashCode() : 0);
            return hash;
        }
    }

    public int CompareTo(Person other)
    {
        var result = this.Name.CompareTo(other.Name);

        if (result == 0)
        {
            result = this.Age.CompareTo(other.Age);
        }

        return result;
    }
}
