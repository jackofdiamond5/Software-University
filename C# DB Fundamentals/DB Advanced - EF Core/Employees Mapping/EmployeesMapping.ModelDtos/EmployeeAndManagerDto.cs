namespace EmployeesMapping.ModelDtos
{
    public class EmployeeAndManagerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal? Salary { get; set; }
        
        public int? ManagerId { get; set; }

        public string ManagerName { get; set; }
    }
}
