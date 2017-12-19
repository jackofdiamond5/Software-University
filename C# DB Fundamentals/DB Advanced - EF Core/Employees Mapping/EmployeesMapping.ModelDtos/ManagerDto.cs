using System.Collections.Generic;

namespace EmployeesMapping.ModelDtos
{
    public class ManagerDto
    {
        public ManagerDto()
        {
            this.ManagedEmployees = new List<EmployeeDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public ICollection<EmployeeDto> ManagedEmployees { get; set; }

        public int ManagedEmployeesCount => ManagedEmployees.Count;
    }
}
