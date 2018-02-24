using System;
using System.Text;

public class Worker : Human
{
    private const int workDaysPerWeek = 5;

    private decimal weekSalary;
    private decimal dailyWorkHours;

    public Worker(string firstName, string lastName, decimal weekSalary, decimal dailyWorkHours)
        : base(firstName, lastName)
    {
        this.WeekSalary = weekSalary;
        this.DailyWorkHours = dailyWorkHours;
    }

    public decimal WeekSalary
    {
        get => weekSalary;
        set
        {
            if (value <= 10)
            {
                throw new ArgumentException("Expected value mismatch! Argument: weekSalary");
            }

            this.weekSalary = value;
        }
    }

    public decimal DailyWorkHours
    {
        get => dailyWorkHours;
        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("Expected value mismatch! Argument: workHoursPerDay");
            }

            this.dailyWorkHours = value;
        }
    }

    public decimal CalculateSalaryPerHour()
    {
        return this.WeekSalary / workDaysPerWeek / this.DailyWorkHours;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder
            .AppendLine($"First Name: {this.FirstName}")
            .AppendLine($"Last Name: {this.LastName}")
            .AppendLine($"Week Salary: {this.WeekSalary:F2}")
            .AppendLine($"Hours per day: {this.DailyWorkHours:F2}")
            .Append($"Salary per hour: {CalculateSalaryPerHour():F2}");

        return builder.ToString();
    }
}