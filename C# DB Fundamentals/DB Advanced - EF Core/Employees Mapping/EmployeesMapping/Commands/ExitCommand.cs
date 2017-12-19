using Services.Contracts;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class ExitCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ExitCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            return "Exit";
        }
    }
}
