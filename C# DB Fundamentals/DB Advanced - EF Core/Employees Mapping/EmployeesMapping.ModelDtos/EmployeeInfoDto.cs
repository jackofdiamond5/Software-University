using System;

namespace EmployeesMapping.ModelDtos
{
    public class EmployeeInfoDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
