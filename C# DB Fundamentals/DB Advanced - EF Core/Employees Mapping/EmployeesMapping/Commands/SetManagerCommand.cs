using Services.Contracts;
using EmployeesMapping.Commands.Contracts;

namespace EmployeesMapping.Commands
{
    public class SetManagerCommand : ICommand
    {
        private readonly IEmployeeService employeeService;

        public SetManagerCommand(IEmployeeService empliyeService)
        {
            this.employeeService = empliyeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var managerId = int.Parse(args[1]);

            employeeService.SetManager(employeeId, managerId);

            return $"Successfully set manager with ID: {managerId} to employee with ID: {employeeId}.";
        }
    }
}
