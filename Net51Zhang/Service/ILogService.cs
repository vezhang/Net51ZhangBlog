using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Service
{
    public interface ILogService
    {
        void Delete(LogEntity item);
        void Update(LogEntity item);
        IList<LogEntity> GetAll();
        void Insert(LogEntity item);
        LogEntity GetEntityById(int id);
    }
}