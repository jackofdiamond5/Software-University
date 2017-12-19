using System;
using System.Globalization;
using Services.Contracts;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class SetBirthdayCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetBirthdayCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var date = args[1];

            employeeService.SetBirthday(employeeId, DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture));

            return $"Birthdat updated for employee with ID: {employeeId}";
        }
    }
}
