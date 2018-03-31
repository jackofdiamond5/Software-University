using System;

public class Person : IComparable<Person>
{
    public Person(string name, int age, string town)
    {
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }

    public string Name { get; }

    public int Age { get; }

    public string Town { get; }

    public int CompareTo(Person other)
    {
        var sameNames = this.Name.CompareTo(other.Name);
        var sameAge = this.Age.CompareTo(other.Age);
        var sameTown = this.Town.CompareTo(other.Town);

        if(sameNames != 0 || sameAge != 0 || sameTown != 0)
        {
            return -1;
        }

        return 0;
    }
}
