using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common.Cache;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Service
{
    public class SqlDiaryService : IDiaryService
    {
        private IRepository<DiaryEntity> _repository;

        public SqlDiaryService(IRepository<DiaryEntity> repository, ICache cache)
        {
            this._repository = repository;
        }

        public void Delete(DiaryEntity item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            this._repository.Delete(item);
        }

        public void Update(DiaryEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Update(item);
        }

        public IList<DiaryEntity> GetAll()
        {
            return this._repository.Table.ToList();
        }

        public void Insert(DiaryEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Insert(item);
        }

        public DiaryEntity GetEntityById(int id)
        {
            return this._repository.GetById(id);
        }
    }
}