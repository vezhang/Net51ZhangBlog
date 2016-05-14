using System;
using System.Collections.Generic;
using System.Linq;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.Manager;

namespace Net51Zhang.Service
{
    public class SqlAccountService : IAccountService
    {
        private IRepository<AccountEntity> _repository;

        public SqlAccountService(IRepository<AccountEntity> repository)
        {
            this._repository = repository;
        }

        public void Delete(AccountEntity item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            this._repository.Delete(item);
        }

        public void Update(AccountEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Update(item);
        }

        public IList<AccountEntity> GetAll()
        {
            return this._repository.Table.ToList();
        }

        public void Insert(AccountEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Insert(item);
        }

        public AccountEntity GetEntityById(int id)
        {
            return this._repository.GetById(id);
        }
    }
}