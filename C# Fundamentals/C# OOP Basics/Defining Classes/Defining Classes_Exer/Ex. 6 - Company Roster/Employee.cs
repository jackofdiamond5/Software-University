using System.Linq;
using System.Text.RegularExpressions;

public class Employee
{
    public string Name { get; set; }

    public decimal Salary { get; set; }

    public string Position { get; set; }
    
    public string Department { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }

    public static Employee GetEmployeeData(string inputLine)
    {
        var inputLineSplit = inputLine.Split();

        var employeeName = inputLineSplit[0];
        var employeeSalary = decimal.Parse(inputLineSplit[1]);
        var employeePosition = inputLineSplit[2];
        var employeeDepartment = inputLineSplit[3];

        const string validateEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$";

        var employeeEmail =
            inputLineSplit.FirstOrDefault(item => Regex.IsMatch(item, validateEmail));

        var ageParsed = int.TryParse(inputLineSplit.Last(), out int employeeAge);

        return new Employee
        {
            Name = employeeName,
            Salary = employeeSalary,
            Position = employeePosition,
            Department = employeeDepartment,
            Email = string.IsNullOrEmpty(employeeEmail) ? "n/a" : employeeEmail,
            Age = ageParsed ? employeeAge : -1
        };
    }
}

