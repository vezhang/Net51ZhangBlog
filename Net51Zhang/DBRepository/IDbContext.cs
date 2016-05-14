using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.DBRepository
{
    public interface IDbContext
    {
        IDbSet<T> Set<T>() where T : BaseEntity;

        int SaveChanges();

        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : BaseEntity, new();

        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);

        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        void Detach(object entity);

        bool ProxyCreationEnabled { get; set; }

        bool AutoDetectChangesEnabled { get; set; }
    }
}