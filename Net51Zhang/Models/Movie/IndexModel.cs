using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Models.Movie
{
    public class IndexModel : BaseMovieModel
    {
        public List<MovieEntity> MovieInTheaters { get; set; }

        public List<MovieEntity> MovieComingSoon { get; set; } 

        public IndexModel(IServiceManager serviceManager) : base(serviceManager)
        {
            this.MovieInTheaters = new List<MovieEntity>();
            this.MovieComingSoon = new List<MovieEntity>();
        }

        public override void Init()
        {
            base.Init();

            this.MovieInTheaters = this.ServiceManager.Cache.GetOrAdd(Constant.MovieInTheatersApi, () =>
            {
                return GetMovieEntities(Constant.MovieInTheatersApi, MovieEntity.SnapParse);

            }, 3600);

            this.MovieComingSoon = this.ServiceManager.Cache.GetOrAdd(Constant.MovieComingSoonApi, () =>
            {
                return GetMovieEntities(Constant.MovieComingSoonApi, MovieEntity.SnapParse);

            }, 3600);
        }
    }
}