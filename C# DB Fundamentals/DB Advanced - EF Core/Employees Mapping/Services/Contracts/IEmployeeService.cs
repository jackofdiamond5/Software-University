using System;
using EmployeesMapping.ModelDtos;

namespace Services.Contracts
{
    public interface IEmployeeService
    {
        void Add(EmployeeDto empDto);

        void SetBirthday(int id, DateTime date);

        void SetAddress(int id, string address);

        EmployeeDto ById(int id);

        EmployeeDto ByFirstAndLastName(string firstName, string lastName);

        EmployeePersonalInfoDto PersonalById(int id);

        void SetManager(int employeeId, int managerId);

        ManagerDto GetManagerInfo(int employeeId);

        EmployeeAndManagerDto[] GetEmployeesOlderThan(int age);

        void AddEmployeeInfo(EmployeeInfoDto empInfoDto);
    }
}
