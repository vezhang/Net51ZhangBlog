using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Net51Zhang.Common;
using Net51Zhang.Models.Movie;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Controllers
{
    public class MovieController : BaseController
    {
        public MovieController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public ActionResult Index()
        {
            var model = new IndexModel(this.ServiceManager);
            model.Init();

            return View(model);
        }

        public ActionResult Detail(string id)
        {
            var model = new DetailModel(this.ServiceManager, id);
            model.Init();

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail_AddComment(MovieCommentEntity comment)
        {
            var model = new DetailModel(this.ServiceManager, comment.MovieId);
            model.AddComment(this.Request, comment);

            return this.Redirect(this.Url.Action("Detail", "Movie") + string.Format("?id={0}", comment.MovieId));
        }

        [AdminAuthorize]
        public ActionResult Comments()
        {
            List<MovieCommentEntity> comments= this.ServiceManager.MovieCommentService.GetAll().ToList();
            comments.Sort((left, right) =>
            {
                return right.CreatedTime.CompareTo(left.CreatedTime);
            });

            return this.View(comments);
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult DeleteComment(int id)
        {
            var target = this.ServiceManager.MovieCommentService.GetEntityById(id);
            this.ServiceManager.MovieCommentService.Delete(target);

            return this.Json(new { Id = id });
        }

        public ActionResult Category(string tag, string start)
        {
            if (Request.IsAjaxRequest())
            {
                var data = CategoryModel.GetCategoryMovieEntities(tag, string.IsNullOrEmpty(start) ? "0" : start);

                return PartialView("_PartialCategory", data);
            }
            else
            {
                var model = new CategoryModel(this.ServiceManager, tag);
                model.Init();

                return View(model);
            }
        }

        public ActionResult Search(string query)
        {
            var model = new SearchModel(this.ServiceManager, query);
            model.Init();

            return View(model);
        }
    }
}