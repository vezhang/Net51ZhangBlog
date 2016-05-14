using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Net51Zhang.Models;

namespace Net51Zhang.DBRepository
{
    public class Net51ZhangDBContext : DbContext
    {
        public Net51ZhangDBContext()
            : base("ApplicationServices")
        {
            Database.SetInitializer<Net51ZhangDBContext>(null);
        }

        public DbSet<LifeMovie> LifeMovies { get; set; }
    }
}