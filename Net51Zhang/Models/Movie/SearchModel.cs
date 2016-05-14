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
    public class SearchModel : BaseMovieModel
    {
        public List<MovieEntity> Movies { get; set; }
        public string Query { get; set; }

        public SearchModel(IServiceManager serviceManager, string query) : base(serviceManager)
        {
            this.Query = query;
            this.Movies = new List<MovieEntity>();
        }

        public override void Init()
        {
            base.Init();

            string error;
            Stream response = Utils.ExecuteWebRequest(string.Format(Constant.MovieSearchApi, this.Query), 4, out error);

            if (string.IsNullOrEmpty(error))
            {
                var result = JObject.Load(new JsonTextReader(new StreamReader(response, Encoding.UTF8)));
                this.Movies =
                    (((JArray) result["subjects"]) ?? new JArray()).Select(token => MovieEntity.Parse(token as JObject))
                        .ToList();
            }
        }
    }
}