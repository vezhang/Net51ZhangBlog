using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Models.Diary
{
    public class DetailModel:PageModel
    {
        private int _id;
        public DiaryModel Entity { get; set; }
        public List<DiaryCommentEntity> Comments { get; set; }

        public DetailModel(IServiceManager serviceManager, int id) : base(serviceManager)
        {
            this._id = id;
        }

        public void Init()
        {
            this.Entity = this.ServiceManager.DiaryService.GetEntityById(this._id).ToModel();
            this.Comments =
                this.ServiceManager.DiaryCommentService.GetAll().Where(com => com.DiaryId == this._id).ToList();
        }

        public void AddComment(HttpRequestBase request, DiaryCommentEntity comment)
        {
            comment.CreatedTime = DateTime.Now;
            comment.IPAddress = Utils.GetRequestIPAddress(request);
            this.ServiceManager.DiaryCommentService.Insert(comment);
        }
    }
}