using System.Text;
using EmployeesMapping.Commands.Contracts;
using Services.Contracts;

namespace EmployeesMapping.Commands
{
    public class ManagerInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public ManagerInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var managerInfo = employeeService.GetManagerInfo(employeeId);

            var builder = new StringBuilder();

            builder.AppendLine($"{managerInfo.FirstName} {managerInfo.LastName} | {managerInfo.ManagedEmployeesCount}");
            foreach (var managedEmp in managerInfo.ManagedEmployees)
            {
                builder.AppendLine($"\t- {managedEmp.FirstName} {managedEmp.LastName} - ${managedEmp.Salary:F2}");
            }

            return builder.ToString();
        }
    }
}
