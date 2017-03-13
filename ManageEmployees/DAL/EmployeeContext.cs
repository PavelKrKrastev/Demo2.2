using ManageEmployees.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ManageEmployees.DAL
{
    public class EmployeeContext : DbContext
    {
        //The name of the connection string (stored in the Web.config) is passed in to the constructor.
        public EmployeeContext()
            : base("name=EmployeeDBConnectionString")
        {
            
        }

        //Creates a DbSet property for each entity set. This corresponds to a database table, and an entity corresponds to a row in the table.
        public DbSet<Team> Teams { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //Prevents table names from being pluralized.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}