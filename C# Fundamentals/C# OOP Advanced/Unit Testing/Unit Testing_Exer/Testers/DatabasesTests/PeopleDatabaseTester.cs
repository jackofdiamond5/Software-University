using NUnit.Framework;

public class PeopleDatabaseTester
{
    [Test]
    public void PeopleShouldHaveUniqueUsernames()
    {
        var person = new Person(1, "Pesho");
        var secondPerson = new Person(2, "Maria");

        var pDb = new PeopleDatabase(person, secondPerson);

        Assert
            .That(() => pDb.Add(new Person(3, "Pesho")),
            Throws.InvalidOperationException);
    }

    [Test]
    public void PeopleShouldHaveUniqueIds()
    {
        var person = new Person(1, "Pesho");
        var secondPerson = new Person(2, "Maria");

        var pDb = new PeopleDatabase(person, secondPerson);

        Assert
            .That(() => pDb.Add(new Person(2, "Ivan")),
            Throws.InvalidOperationException);
    }

    [Test]
    public void FindByUsernameThrowsExceptionIfNoUserIsFound()
    {
        var pDb = new PeopleDatabase();

        Assert.That(() => pDb.FindByUsername("Pesho"),
            Throws.InvalidOperationException);
    }

    [Test]
    public void FindByUsernameThrowsExceptionIfArgumentIsNull()
    {
        var pDb = new PeopleDatabase();

        Assert.That(() => pDb.FindByUsername(null),
            Throws.ArgumentNullException);
    }

    [Test]
    public void FindByIdThrowsExceptionIfNoUserIsFound()
    {
        var pDb = new PeopleDatabase();

        Assert.That(() => pDb.FindById(5),
            Throws.InvalidOperationException);
    }
}
