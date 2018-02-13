using System.Linq;
using System.Collections.Generic;

public class Person
{
    private string name;
    private int age;
    private List<BankAccount> accounts;

    public Person()
    {
        this.accounts = new List<BankAccount>();
    }

    public Person(string name, int age) : this()
    {
        this.name = name;
        this.age = age;
    }

    public Person(string name, int age, List<BankAccount> bankAccounts) : this(name, age)
    {
        this.accounts = bankAccounts;
    }

    public decimal GetBalance()
    {
        return this.accounts.Sum(a => a.Balance);
    }
}