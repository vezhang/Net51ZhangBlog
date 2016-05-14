using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common.Cache;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Service
{
    public class SqlLogService : ILogService
    {
        private IRepository<LogEntity> _repository;

        public SqlLogService(IRepository<LogEntity> repository, ICache cache)
        {
            this._repository = repository;
        }

        public void Delete(LogEntity item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            this._repository.Delete(item);
        }

        public void Update(LogEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Update(item);
        }

        public IList<LogEntity> GetAll()
        {
            return this._repository.Table.ToList();
        }

        public void Insert(LogEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Insert(item);
        }

        public LogEntity GetEntityById(int id)
        {
            return this._repository.GetById(id);
        }
    }
}