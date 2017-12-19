using System;
using System.Collections.Generic;

namespace EmployeesMapping.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            this.ManagedEmployees = new List<Employee>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }
        
        public ICollection<Employee> ManagedEmployees { get; set; }
    }
}
