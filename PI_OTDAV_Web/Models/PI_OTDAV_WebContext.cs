using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PI_OTDAV_Web.Models
{
    public class PI_OTDAV_WebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PI_OTDAV_WebContext() : base("name=PI_OTDAV_WebContext")
        {
        }

        public System.Data.Entity.DbSet<PI_OTDAV_Web.Models.paimentModel> paimentModels { get; set; }

        public System.Data.Entity.DbSet<PI_OTDAV_Web.Models.User> Users { get; set; }
    }
}
