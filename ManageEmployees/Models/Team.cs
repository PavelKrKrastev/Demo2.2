using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManageEmployees.Models
{
    public class Team
    {
        /// <summary>
        /// Defines the structure of the Employee entity
        /// used to create the database useing the code first
        /// method
        /// </summary
        public Team()
        {

        }

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Team Project")]
        [StringLength(50)]
        public string Project { get; set; }

        [Required]
        [Display(Name = "ProjectManager")]
        public string ProjectManager { get; set; }

        //One team can have multyple people working on it
        public virtual ICollection<Employee> Employees { get; set; }

    }
}