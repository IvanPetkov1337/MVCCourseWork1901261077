using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CourseWork1.Models
{
    public class Orders
    {
        public Int32 Id { get; set; }
 
       [Required]
       [StringLength(25, MinimumLength = 1)]
        public string FirstNameOfClient { get; set; }

        [StringLength(25, MinimumLength = 1)]
        public string LastNameOfClient { get; set; }
        
        [Required]
        [Range(1, 12)]
        public int Table { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        [Display(Name = "Time of Order")]
        public DateTime TimeOfOrder { get; set; }

        [StringLength(128)]
        public string Notes { get; set; }
        
        [Range(0, 100)]
        public double Tip { get; set; }

        [Required]
        [Range(1.00, 5.00)]
        public double Rating { get; set; }

        public Int32? MenuId { get; set; }
        public Int32? WaiterId { get; set; }



        public virtual Menu Menu { get; set; }
        public virtual Waiter Waiter { get; set; }
    }
}