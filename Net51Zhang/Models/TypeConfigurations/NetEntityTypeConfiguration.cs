using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Models.TypeConfigurations
{
    public abstract class NetEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public NetEntityTypeConfiguration()
        {
            this.PostInitialize();
        }

        protected virtual void PostInitialize()
        {
        }
    }
}