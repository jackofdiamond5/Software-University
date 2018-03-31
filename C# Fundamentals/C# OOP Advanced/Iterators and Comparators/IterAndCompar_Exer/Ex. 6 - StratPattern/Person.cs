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

    public int CompareTo(Person other)
    {
        if(this.Name.Length > other.Name.Length)
        {
            return 1;
        }
        else if(this.Name.Length == other.Name.Length)
        {
            var thisFirstLetter = this.Name[0].ToString().ToLower();
            var otherFirstLetter = other.Name[0].ToString().ToLower();
            
            if(thisFirstLetter[0] > otherFirstLetter[0])
            {
                return 1;
            }
            else if(thisFirstLetter[0] < otherFirstLetter[0])
            {
                return -1;
            }
        }
        else if(this.Name.Length < other.Name.Length)
        {
            return -1;
        }
        
        return 0;
    }
}
