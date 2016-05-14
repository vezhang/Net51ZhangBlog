using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Models.TypeConfigurations
{
    public class MovieCommentTypeConfiguration : NetEntityTypeConfiguration<MovieCommentEntity>
    {
        public MovieCommentTypeConfiguration()
        {
            this.ToTable("MovieCommentTable");
            HasKey(m => m.Id);
            Property(m => m.CreatedTime).IsRequired();
            Property(m => m.Content).IsRequired();
            Property(m => m.MovieId).IsRequired();
            Property(m => m.Email).IsRequired();
            Property(m => m.NickName).IsRequired();
        }
    }
}