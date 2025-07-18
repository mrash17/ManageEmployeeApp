using ManageEmployeeApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageEmployeeApp.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeSalary> EmployeeSalaries => Set<EmployeeSalary>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=employee.db");
    }
}