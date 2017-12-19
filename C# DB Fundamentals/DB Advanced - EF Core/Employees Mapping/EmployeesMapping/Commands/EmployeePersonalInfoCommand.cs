using System.Text;
using Services.Contracts;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public EmployeePersonalInfoCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);

            var employeeInfo = employeeService.PersonalById(employeeId);

            var builder = new StringBuilder();
            builder.AppendLine($"ID: {employeeInfo.Id} - {employeeInfo.FirstName} " +
                $"{employeeInfo.LastName} - ${employeeInfo.Salary:F2}");
            builder.AppendLine($"Birthday: {employeeInfo.Birthday}");
            builder.AppendLine($"Address: {employeeInfo.Address}");

            return builder.ToString();
        }
    }
}
