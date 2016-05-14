using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.DBRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);

        void Insert(T item);

        void Insert(IEnumerable<T> items);

        void Update(T item);

        void Update(IEnumerable<T> items);

        void Delete(T item);

        void Delete(IEnumerable<T> items);

        IQueryable<T> Table { get; }
 
        IQueryable<T> TableNoTracking { get; } 
    }
}