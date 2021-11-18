using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWork1.Models
{
    public class Waiter
    {
         public Int32 Id { set; get; }

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string FirstName { set; get; }

        [StringLength(25, MinimumLength = 1)]
        public string LastName { set; get; }

        [Required]
         public bool IsVaccinated { set; get; }

        [Required]
        [Range(0,80)]
         public int Experience { set; get; }
         public double Salary { set; get; } 
         public virtual ICollection<Orders> Orders { get; set; }




    }
}