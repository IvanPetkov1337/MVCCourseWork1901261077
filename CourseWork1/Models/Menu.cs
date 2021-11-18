using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;

namespace CourseWork1.Models
{
    public class Menu
    {
        public Int32 Id { get; set; }
        [Required]
        [StringLength(32)]
        public string ItemName { get; set; }

        [Required]
        public bool IsVegan { get; set; }

        [Required]
        [Range(0, 200)]
        public double Price { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public int Weight { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }





    }
}