using System;
using System.Globalization;
using EmployeesMapping.Commands.Contracts;
using EmployeesMapping.ModelDtos;
using Services.Contracts;

namespace EmployeesMapping.Commands
{
    public class AddEmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);
            var birthday = DateTime.ParseExact(args[3], "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var empInfoDto = new EmployeeInfoDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary,
                Birthday = birthday
            };

            employeeService.AddEmployeeInfo(empInfoDto);

            return $"New employee with personal info added.";
        }
    }
}
