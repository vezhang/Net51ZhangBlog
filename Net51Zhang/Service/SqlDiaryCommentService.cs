using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common.Cache;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Service
{
    public class SqlDiaryCommentService : IDiaryCommentService
    {
        private IRepository<DiaryCommentEntity> _repository;

        public SqlDiaryCommentService(IRepository<DiaryCommentEntity> repository, ICache cache)
        {
            this._repository = repository;
        }

        public void Delete(DiaryCommentEntity item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            this._repository.Delete(item);
        }

        public void Update(DiaryCommentEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Update(item);
        }

        public IList<DiaryCommentEntity> GetAll()
        {
            return this._repository.Table.ToList();
        }

        public void Insert(DiaryCommentEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Insert(item);
        }

        public DiaryCommentEntity GetEntityById(int id)
        {
            return this._repository.GetById(id);
        }
    }
}