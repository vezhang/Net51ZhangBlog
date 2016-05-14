using System;
using System.Collections.Generic;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Service
{
    public interface IDiaryCommentService
    {
        void Delete(DiaryCommentEntity item);
        void Update(DiaryCommentEntity item);
        IList<DiaryCommentEntity> GetAll();
        void Insert(DiaryCommentEntity item);
        DiaryCommentEntity GetEntityById(int id);
    }
}