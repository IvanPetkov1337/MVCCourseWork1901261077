using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CourseWork1.Models
{
    public class SystemDBController : DbContext
    {
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Menu> Menus { get; set; }
    }
}