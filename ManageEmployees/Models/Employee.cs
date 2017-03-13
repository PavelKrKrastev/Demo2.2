using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ManageEmployees.Models
{
    public class Employee
    {
        /// <summary>
        /// Defines the structure of the Employee entity
        /// used to create the database useing the code first
        /// method
        /// </summary>

        public Employee()
        {

        }
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }


        public string Email { get; set; }

        [Required]
        [Display(Name = "Position")]
        public EmployeePosition Position { get; set; }

        public double Salary { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }

        //Foreign key used for knowing the hierarchy in the company
        [Display(Name = "Managed by")]
        public int? EmployeeId { get; set; }
        public virtual Employee Manager { get; set; }

        //Foreign key for the employee data model 
        //An employee can be only on one team
        public int? TeamID { get; set; }
        public virtual Team Teams { get; set; }
    }

    //Enum type for representing the positions available in the company
    public enum EmployeePosition
    {
        CEO = 0,
        Delivery_Director = 1,
        Project_Manager = 2,
        Team_Leader = 3,
        Senior = 4,
        Intermediate = 5,
        Junior = 6,
        Trainee = 7,
    }
}