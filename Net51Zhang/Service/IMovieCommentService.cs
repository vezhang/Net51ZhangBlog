using System;
using System.Collections.Generic;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Service
{
    public interface IMovieCommentService
    {
        void Delete(MovieCommentEntity item);
        void Update(MovieCommentEntity item);
        IList<MovieCommentEntity> GetAll();
        void Insert(MovieCommentEntity item);
        MovieCommentEntity GetEntityById(int id);
    }
}