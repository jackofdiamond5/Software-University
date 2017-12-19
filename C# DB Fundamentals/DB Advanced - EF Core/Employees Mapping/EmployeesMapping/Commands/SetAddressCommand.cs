using Services.Contracts;
using ICommand = EmployeesMapping.Commands.Contracts.ICommand;

namespace EmployeesMapping.Commands
{
    public class SetAddressCommand : ICommand
    {
        private readonly IEmployeeService employeeService;
        
        public SetAddressCommand(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var address = args[1];

            employeeService.SetAddress(employeeId, address);

            return $"Successfully set address {address} for employee with ID: {employeeId}";
        }
    }
}
