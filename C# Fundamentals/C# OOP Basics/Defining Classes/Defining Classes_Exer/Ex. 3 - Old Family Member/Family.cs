using System.Collections.Generic;
using System.Linq;

public class Family
{
    public List<Person> People { get; set; } = new List<Person>();

    public void AddMembers(Person person)
    {
        this.People.Add(person);
    }

    public Person GetOldestMember()
    {
        return People.OrderByDescending(p => p.Age).First();
    }
}