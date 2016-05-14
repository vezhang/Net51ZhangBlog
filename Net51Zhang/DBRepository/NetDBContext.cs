using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Web;
using Net51Zhang.Common;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;
using Net51Zhang.Models.TypeConfigurations;

namespace Net51Zhang.DBRepository
{
    public class NetDBContext : DbContext, IDbContext
    {
        public NetDBContext(string connectstring) : base(connectstring)
        {
            Database.SetInitializer<NetDBContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var targetTypes =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(
                        t =>
                            t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == typeof (NetEntityTypeConfiguration<>));
            foreach (var targetType in targetTypes)
            {
                dynamic data = Activator.CreateInstance(targetType);
                modelBuilder.Configurations.Add(data);
            }

            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters)
            where T : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null,
            params object[] parameters)
        {
            int? oldTimeout = null;
            if (timeout.HasValue)
            {
                oldTimeout = ((IObjectContextAdapter) this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = timeout;
            }

            var result = this.Database.ExecuteSqlCommand(sql, parameters);

            if (timeout.HasValue)
            {
                ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = oldTimeout;
            }

            return result;
        }

        public void Detach(object entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter) this).ObjectContext.Detach(entity);
        }

        public bool ProxyCreationEnabled 
        {
            get { return this.Configuration.ProxyCreationEnabled; }
            set { this.Configuration.ProxyCreationEnabled = value; } 
        }

        public bool AutoDetectChangesEnabled 
        {
            get { return this.Configuration.AutoDetectChangesEnabled; }
            set { this.Configuration.AutoDetectChangesEnabled = value; }
        }
    }
}