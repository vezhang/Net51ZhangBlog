using System;
using System.Collections.Generic;
using Net51Zhang.Models.Manager;

namespace Net51Zhang.Service
{
    public interface IAccountService
    {
        void Delete(AccountEntity item);
        void Update(AccountEntity item);
        IList<AccountEntity> GetAll();
        void Insert(AccountEntity item);
        AccountEntity GetEntityById(int id);
    }
}