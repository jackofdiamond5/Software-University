using System;

using AutoMapper;

using Services;
using Services.Contracts;
using EmployeesMapping.Data;
using EmployeesMapping.ModelDtos;
using EmployeesMapping.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeesMapping
{
    public class Startup
    {
        public static void Main()
        {
            InitializeMapepr();

            var serviceProvider = ConfigureServices();

            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static void InitializeMapepr()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>();
                cfg.CreateMap<Employee, ManagerDto>();
                cfg.CreateMap<Employee, EmployeeAndManagerDto>();
                cfg.CreateMap<Employee, EmployeeInfoDto>();
                cfg.CreateMap<EmployeeInfoDto, Employee>();
            });
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(options =>
                options.UseSqlServer(Configure.ConfigurationString));

            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}