using System;
using System.Collections.Generic;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Service
{
    public interface IDiaryService
    {
        void Delete(DiaryEntity item);
        void Update(DiaryEntity item);
        IList<DiaryEntity> GetAll();
        void Insert(DiaryEntity item);
        DiaryEntity GetEntityById(int id);
    }
}