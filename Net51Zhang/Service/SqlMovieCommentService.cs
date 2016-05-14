using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common.Cache;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Service
{
    public class SqlMovieCommentService : IMovieCommentService
    {
        private IRepository<MovieCommentEntity> _repository;

        public SqlMovieCommentService(IRepository<MovieCommentEntity> repository, ICache cache)
        {
            this._repository = repository;
        }

        public void Delete(MovieCommentEntity item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            this._repository.Delete(item);
        }

        public void Update(MovieCommentEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Update(item);
        }

        public IList<MovieCommentEntity> GetAll()
        {
            return this._repository.Table.ToList();
        }

        public void Insert(MovieCommentEntity item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this._repository.Insert(item);
        }

        public MovieCommentEntity GetEntityById(int id)
        {
            return this._repository.GetById(id);
        }
    }
}