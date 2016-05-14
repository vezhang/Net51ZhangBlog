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
    public abstract class BaseMovieModel : PageModel
    {
        public List<USBoxEntity> UsBoxEntities { get; set; }

        public BaseMovieModel(IServiceManager serviceManager) : base(serviceManager)
        {
            this.UsBoxEntities = new List<USBoxEntity>();
        }

        public virtual void Init()
        {
            this.UsBoxEntities = this.ServiceManager.Cache.GetOrAdd(Constant.MovieUSBoxApi, () =>
            {
                string error;
                Stream response = Utils.ExecuteWebRequest(Constant.MovieUSBoxApi, 4, out error);

                if (string.IsNullOrEmpty(error))
                {
                    var result = JObject.Load(new JsonTextReader(new StreamReader(response, Encoding.UTF8)));
                    return
                        ((JArray) result["subjects"] ?? new JArray()).Select(
                            token => USBoxEntity.Parse(token as JObject)).ToList();
                }
                else
                {
                    throw new DoubanApiException(error);
                }

            }, 2 * 3600);
        }

        protected static List<MovieEntity> GetMovieEntities(string doubanApi, Func<JObject, MovieEntity> parseFunc)
        {
            string error;
            Stream response = Utils.ExecuteWebRequest(doubanApi, 4, out error);

            if (string.IsNullOrEmpty(error))
            {
                var result = JObject.Load(new JsonTextReader(new StreamReader(response, Encoding.UTF8)));
                return
                    (((JArray)result["subjects"]) ?? new JArray()).Select(
                        token => parseFunc(token as JObject)).ToList();
            }
            else
            {
                throw new DoubanApiException(error);
            }
        }
    }
}