using Services.Contracts;
using EmployeesMapping.ModelDtos;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class AddEmployeeCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);

            var empDto = new EmployeeDto
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            employeeService.Add(empDto);

            return $"Emloyee {firstName} added to the database!";
        }
    }
}
