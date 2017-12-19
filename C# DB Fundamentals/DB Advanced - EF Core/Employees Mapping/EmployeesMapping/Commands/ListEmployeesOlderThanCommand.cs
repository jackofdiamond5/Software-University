using System.Text;
using Services.Contracts;
using EmployeesMapping.Commands.Contracts;

namespace EmployeesMapping.Commands
{
    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ListEmployeesOlderThanCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);

            var employeesOlderThan = employeeService.GetEmployeesOlderThan(age);

            var builder = new StringBuilder();

            foreach (var emp in employeesOlderThan)
            {
                var manager = emp.ManagerName is null ? "[no manager]" : emp.ManagerName;
                builder.AppendLine($"{emp.FirstName} {emp.LastName} - ${emp.Salary} - Manager: {manager}");
            }

            return builder.ToString();
        }
    }
}
