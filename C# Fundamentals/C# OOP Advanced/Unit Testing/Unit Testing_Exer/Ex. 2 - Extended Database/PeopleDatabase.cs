using System;
using System.Linq;

public class PeopleDatabase
{
    private const int MaxArrSize = 16;

    public PeopleDatabase(params Person[] initialValues)
    {
        this.people = new Person[MaxArrSize];
        this.People = initialValues;
    }

    private Person[] people;

    public Person[] People
    {
        get { return this.people; }
        private set
        {
            if (value.Length > MaxArrSize)
            {
                throw new InvalidOperationException();
            }

            for (var i = 0; i < value.Length; i++)
            {
                this.people[i] = value[i];
            }
        }
    }

    public void Add(Person personToAdd)
    {
        if (this.people.All(p => p != null))
        {
            throw new InvalidOperationException();
        }

        if (this.people.Any(p => p != null &&
            p.Username == personToAdd.Username))
        {
            throw new InvalidOperationException();
        }

        if (this.people.Any(p => p != null &&
            p.Id == personToAdd.Id))
        {
            throw new InvalidOperationException();
        }

        var lastElementIndex = 0;
        foreach (var person in this.people)
        {
            if (person != null)
                continue;

            lastElementIndex = Array.IndexOf(this.people, person);
            break;
        }

        this.people[lastElementIndex] = personToAdd;
    }

    public void Remove()
    {
        if (this.people.All(p => p == null))
        {
            throw new InvalidOperationException();
        }

        for (var i = this.people.Length - 1; i > 0; i--)
        {
            if (this.people[i] == null)
                continue;

            this.people[i] = null;
            break;
        }
    }

    public Person[] Fetch()
    {
        return this.people.Where(n => n != null).ToArray();
    }

    public Person FindByUsername(string username)
    {
        if(username == null)
        {
            throw new ArgumentNullException();
        }

        var person = this.people.FirstOrDefault(f => f != null && 
            f.Username == username);
        if (person == null)
        {
            throw new InvalidOperationException();
        }

        return person;
    }

    public Person FindById(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        var person = this.people.FirstOrDefault(f => f != null &&
            f.Id == id);
        if(person == null)
        {
            throw new InvalidOperationException();
        }
        
        return person;
    }
}
