using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;
using Services.Contracts;
using EmployeesMapping.Data;
using EmployeesMapping.ModelDtos;
using EmployeesMapping.Data.Models;
using AutoMapper.QueryableExtensions;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public void Add(EmployeeDto empDto)
        {
            var employee = Mapper.Map<Employee>(empDto);

            context.Employees.Add(employee);

            context.SaveChanges();
        }

        public void SetBirthday(int id, DateTime date)
        {
            var employee = context.Employees.Find(id);

            employee.Birthday = date;

            context.SaveChanges();
        }

        public void SetAddress(int id, string address)
        {
            var employee = context.Employees.Find(id);

            employee.Address = address;

            context.SaveChanges();
        }

        public EmployeeDto ById(int id)
        {
            var dbEmployee = context.Employees.Find(id);

            var employee = Mapper.Map<EmployeeDto>(dbEmployee);

            return employee;
        }

        public EmployeeDto ByFirstAndLastName(string firstName, string lastName)
        {
            var dbEmployee = context.Employees
                .SingleOrDefault(e => e.FirstName.Equals(firstName) && e.LastName.Equals(lastName));

            var employee = Mapper.Map<EmployeeDto>(dbEmployee);

            return employee;
        }

        public EmployeePersonalInfoDto PersonalById(int id)
        {
            var dbEmployee = context.Employees.Find(id);

            var employee = Mapper.Map<EmployeePersonalInfoDto>(dbEmployee);

            return employee;
        }

        public void SetManager(int employeeId, int managerId)
        {
            var dbEmployee = context.Employees.Find(employeeId);
            var dbManager = context.Employees.Find(managerId);

            if (dbEmployee is null || dbManager is null)
            {
                throw new ArgumentException("Employee or manager not found!");
            }

            dbEmployee.ManagerId = dbManager.Id;

            context.SaveChanges();
        }

        public ManagerDto GetManagerInfo(int employeeId)
        {
            var manager = context.Employees.SingleOrDefault(m => m.Id.Equals(employeeId));

            if (manager is null)
            {
                throw new ArgumentException("Employee not found!");
            }

            var managerDto = Mapper.Map<ManagerDto>(manager);

            var managedEmployees = context.Employees
                .Where(e => e.ManagerId.Equals(employeeId))
                .ProjectTo<EmployeeDto>()
                .ToList();

            foreach (var managedEmployee in managedEmployees)
            {
                managerDto.ManagedEmployees.Add(managedEmployee);
            }

            return managerDto;
        }

        public EmployeeAndManagerDto[] GetEmployeesOlderThan(int age)
        {
            var employeesOlderThan = context.Employees
                .Where(e => e.Birthday.Value.Year > age)
                .ProjectTo<EmployeeAndManagerDto>()
                .ToArray();

            var employeesAndManagers = new List<EmployeeAndManagerDto>();

            foreach (var empDto in employeesOlderThan)
            {
                if (empDto.ManagerId is null)
                {
                    employeesAndManagers.Add(empDto);
                    continue;
                }

                var empManager = context.Employees.SingleOrDefault(e => e.Id.Equals(empDto.ManagerId));

                empDto.ManagerName = empManager.LastName;

                employeesAndManagers.Add(empDto);
            }

            return employeesAndManagers.ToArray();
        }

        public void AddEmployeeInfo(EmployeeInfoDto empInfoDto)
        {
            var employee = Mapper.Map<Employee>(empInfoDto);

            context.Employees.Add(employee);

            context.SaveChanges();
        }
    }
}
