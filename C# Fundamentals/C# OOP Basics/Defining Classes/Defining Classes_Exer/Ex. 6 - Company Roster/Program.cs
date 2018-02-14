using System;
using System.Linq;
using System.Collections.Generic;

public class Program
{
    static void Main()
    {
        var n = int.Parse(Console.ReadLine());

        var employeeList = new List<Employee>();

        for (int i = 0; i < n; i++)
        {
            var inputLine = Console.ReadLine();

            employeeList.Add(Employee.GetEmployeeData(inputLine));
        }

        var depGroups = employeeList
            .GroupBy(e => e.Department,
                e => e.Salary,
                (dep, sal) => new { Department = dep, Salary = sal.ToList() }
            )
            .ToDictionary(e => e.Department);

        var highestAverageSalary = decimal.MinValue;
        var highestSalaryInDep = "";
        foreach (var depGroup in depGroups)
        {
            var currentAverageSalary = depGroup.Value.Salary.Average();
            var currentDepartment = depGroup.Key;

            if (highestAverageSalary > currentAverageSalary)
                continue;

            highestAverageSalary = currentAverageSalary;
            highestSalaryInDep = currentDepartment;
        }

        Console.WriteLine($"Highest Average Salary: {highestSalaryInDep}");
        foreach (var employee in employeeList.OrderByDescending(s => s.Salary))
        {
            if (employee.Department != highestSalaryInDep)
                continue;

            Console.WriteLine($"{employee.Name} {employee.Salary:F2} {employee.Email} {employee.Age}");
        }
    }
}