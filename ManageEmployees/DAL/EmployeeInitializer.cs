using ManageEmployees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageEmployees.DAL
{
    public class EmployeeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EmployeeContext>
    {
        /// <summary>
        ///Database is dropped and re-created whenever the model changes.
        ///The Seed method runs when the database is re-created and re-creates the test data.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(EmployeeContext context)
        {
            //Populate the databse with Team data
            var teams = new List<Team> 
            {
                new Team {Name = "Alpha", Project = "AutoDrive", ProjectManager = "Peggy"},
                new Team {Name = "Beta", Project = "RobotFuture", ProjectManager = "Nino"},
                new Team {Name = "Gamma", Project = "SmartHome", ProjectManager = "Elizabeth"},
            };
            //Save changes to database
            teams.ForEach(t => context.Teams.Add(t));
            context.SaveChanges();

            //Populate the databse with Employee data
            var employees = new List<Employee>
            {
                new Employee{FirstName="Alexander",LastName="Carson",Email = "alex_carson@gmail.com", Position = EmployeePosition.CEO, Salary = 12000, City = "London", Phone = "0899776907"},
                new Employee{FirstName="Meredith",LastName="Alonso",Email = "merry_alonso@gmail.com", Position = EmployeePosition.Delivery_Director, Salary = 10000, City = "Madrid", Phone = "0898766947", EmployeeId = 1},
                new Employee{FirstName="Yan",LastName="Li",Email = "yan_li@gmail.com", Position = EmployeePosition.Delivery_Director, Salary = 11000, City = "Paris", Phone = "0897086923", EmployeeId = 1},
                new Employee{FirstName="Peggy",LastName="Justice",Email = "just_peggy@gmail.com", Position = EmployeePosition.Project_Manager, Salary = 9000, City = "London", Phone = "0897307923", EmployeeId = 2},
                new Employee{FirstName="Nino",LastName="Olivetto",Email = "nino_oli@gmail.com", Position = EmployeePosition.Project_Manager, Salary = 8500, City = "Rome", Phone = "08970839562", EmployeeId = 3},
                new Employee{FirstName="James",LastName="Smith",Email = "james_smith@gmail.com", Position = EmployeePosition.Team_Leader, Salary = 7000, City = "Chicago", Phone = "0897024323", EmployeeId = 4, TeamID=1},
                new Employee{FirstName="Robert",LastName="Johnson",Email = "robert_johnson@gmail.com", Position = EmployeePosition.Team_Leader, Salary = 7500, City = "New York", Phone = "0897086729", EmployeeId = 5, TeamID=2},
                new Employee{FirstName="Maria",LastName="Garcia",Email = "maria_garcia@gmail.com", Position = EmployeePosition.Senior, Salary = 6000, City = "Berlin", Phone = "0897005623", EmployeeId = 4, TeamID=1},
                new Employee{FirstName="William",LastName="Smith",Email = "will_smith@gmail.com", Position = EmployeePosition.Senior, Salary = 5500, City = "Amsterdam", Phone = "0897216923", EmployeeId = 5, TeamID=2},
                new Employee{FirstName="Anne",LastName="Allison",Email = "anny@gmail.com", Position = EmployeePosition.Intermediate, Salary = 5000, City = "Warsaw", Phone = "0897970923", EmployeeId = 4, TeamID=1},
                new Employee{FirstName="Jane",LastName="Walter",Email = "jane_walt@gmail.com", Position = EmployeePosition.Intermediate, Salary = 5000, City = "Paris", Phone = "0897086510", EmployeeId = 5, TeamID=2},
                new Employee{FirstName="Thomas",LastName="Forest",Email = "thon_forest@gmail.com", Position = EmployeePosition.Junior, Salary = 3000, City = "Sofia", Phone = "0897020823", EmployeeId = 4, TeamID=1},
                new Employee{FirstName="Samuel",LastName="Lee",Email = "sam_lee@gmail.com", Position = EmployeePosition.Junior, Salary = 3000, City = "Burgas", Phone = "0897432923", EmployeeId = 5, TeamID=2},
                new Employee{FirstName="Phill",LastName="Hunt",Email = "phill_hunt@gmail.com", Position = EmployeePosition.Delivery_Director, Salary = 10500, City = "Bangkok", Phone = "0897736923", EmployeeId = 1},
                new Employee{FirstName="Elizabeth",LastName="Taylor",Email = "betty_taylor@gmail.com", Position = EmployeePosition.Project_Manager, Salary = 8700, City = "Dubai", Phone = "08977311534", EmployeeId = 14},
                new Employee{FirstName="John",LastName="Johnson",Email = "john_johnson@gmail.com", Position = EmployeePosition.Team_Leader, Salary = 7500, City = "Istanbul", Phone = "08979011534", EmployeeId = 15,TeamID = 3},
            };
            //Save changes to database
            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();
        }
    }
}