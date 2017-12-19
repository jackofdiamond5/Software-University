using System;
using System.Linq;
using System.Globalization;
using P02_DatabaseFirst.Data;
using System.Collections.Generic;
using P02_DatabaseFirst.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace P02_DatabaseFirst
{
    class Program
    {
        private static void Main()
        {
            var context = new SoftUniContext();

            using (context)
            {
                const string input = "Seattle";

                var towns = context.Towns
                    .Include(a => a.Addresses)
                    .Select(t => new
                    {
                        t.TownId,
                        t.Name,
                        t.Addresses
                    })
                    .Where(t => t.Name.Equals(input));

                var employeesIds = context.Employees.Select(e => e.EmployeeId);
                var employeeList = context.Employees.ToList();

                var removedAddrCount = 0;
                foreach (var town in towns)
                {
                    foreach (var addr in town.Addresses)
                    {
                        addr.Employees = employeeList;

                        foreach (var emp in employeesIds)
                        {
                            var currentEmp = addr.Employees.SingleOrDefault(e => e.EmployeeId == emp);

                            addr.Employees.SingleOrDefault(e => e.EmployeeId == currentEmp.EmployeeId).AddressId = null;
                        }

                        var currentAddress = context.Addresses.Find(addr.AddressId);
                        context.Addresses.Remove(currentAddress);
                        removedAddrCount++;
                    }

                    var currentTown = context.Towns.Find(town.TownId);
                    context.Towns.Remove(currentTown);
                }

                //context.SaveChanges();

                if (removedAddrCount > 1 || removedAddrCount == 0)
                {
                    Console.WriteLine($"{removedAddrCount} addresses in {input} were deleted");
                }
                else
                {
                    Console.WriteLine($"{removedAddrCount} address in {input} was deleted");
                }
            }
        }
    }
}