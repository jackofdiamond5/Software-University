using Services.Contracts;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class EmployeeInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeeInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var employeeInfo = employeeService.ById(employeeId);

            return $"ID: {employeeInfo.Id} - {employeeInfo.FirstName} {employeeInfo.LastName} - ${employeeInfo.Salary:F2}";
        }
    }
}
