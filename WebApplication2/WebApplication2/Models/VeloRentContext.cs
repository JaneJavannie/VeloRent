using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class VeloRentContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public VeloRentContext() : base("name=VeloRentContext")
        {
        }

        public System.Data.Entity.DbSet<WebApplication2.Clients> Clients { get; set; }

        public System.Data.Entity.DbSet<WebApplication2.RentPoints> RentPoints { get; set; }

        public System.Data.Entity.DbSet<WebApplication2.Velos> Velos { get; set; }

        public System.Data.Entity.DbSet<WebApplication2.Req> Reqs { get; set; }
    }
}
