using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Models.TypeConfigurations
{
    public class ErrorLogEntityTypeConfiguration : NetEntityTypeConfiguration<LogEntity>
    {
        public ErrorLogEntityTypeConfiguration()
        {
            this.ToTable("ErrorTable");
            HasKey(m => m.Id);
            Property(m => m.CreateTime).IsRequired();
            Property(m => m.Message).IsRequired();
        }
    }
}