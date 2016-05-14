using System.Collections.Generic;
using Net51Zhang.Common;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Models.Movie
{
    public class CategoryModel : BaseMovieModel
    {
        public const int DefaultCountPerpage = 10;
        public List<MovieEntity> Movies { get; set; }
        public string Tag { get;set; }
        public CategoryModel(IServiceManager serviceManager, string tag) : base(serviceManager)
        {
            this.Tag = tag;
            this.Movies = new List<MovieEntity>();
        }

        public override void Init()
        {
            base.Init();
            
            string key = string.Format(Constant.MovieTagApi, this.Tag, DefaultCountPerpage);
            this.Movies = this.ServiceManager.Cache.GetOrAdd(key, () =>
            {
                return GetMovieEntities(key, MovieEntity.Parse);

            }, 3600);
        }

        public static List<MovieEntity> GetCategoryMovieEntities(string tag, string start)
        {
            string key = string.Format(Constant.MovieTagApi, tag, DefaultCountPerpage) + string.Format("&start={0}", start);

            return GetMovieEntities(key, MovieEntity.Parse);
        }
    }
}