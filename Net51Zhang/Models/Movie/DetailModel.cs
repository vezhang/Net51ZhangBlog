using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Net51Zhang.Common;
using Net51Zhang.Models.Movie.EntityModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Net51Zhang.Models.Movie
{
    public class DetailModel : BaseMovieModel
    {
        public MovieEntity Entity { get; set; }
        public List<MovieCommentEntity> Comments { get; set; } 

        private string _id;
        public DetailModel(IServiceManager serviceManager, string id) : base(serviceManager)
        {
            this._id = id;
        }

        public override void Init()
        {
            base.Init();

            string key = string.Format(Constant.MovieDetailApi, this._id);
            this.Entity = this.ServiceManager.Cache.GetOrAdd(key, () =>
            {
                string error;
                Stream response = Utils.ExecuteWebRequest(key, 4, out error);

                if (string.IsNullOrEmpty(error))
                {
                    var result = JObject.Load(new JsonTextReader(new StreamReader(response, Encoding.UTF8)));
                    return MovieEntity.Parse(result);
                }
                else
                {
                    throw new DoubanApiException(error);
                }

            }, TimeSpan.FromMinutes(30));

            this.Comments =
                this.ServiceManager.MovieCommentService.GetAll()
                    .Where(item => item.MovieId.Equals(this._id, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            this.Comments.Sort((left, right) =>
            {
                return left.CreatedTime.CompareTo(right.CreatedTime);
            });
        }

        public void AddComment(HttpRequestBase request, MovieCommentEntity comment)
        {
            comment.CreatedTime = DateTime.Now;
            comment.IPAddress = Utils.GetRequestIPAddress(request);
            this.ServiceManager.MovieCommentService.Insert(comment);
        }
    }
}