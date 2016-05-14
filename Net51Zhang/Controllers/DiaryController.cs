using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Common;
using Net51Zhang.Models.Diary;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Controllers
{
    public class DiaryController : BaseController
    {
        public DiaryController(IServiceManager manager) : base(manager)
        {
        }

        public ActionResult Index(string tag)
        {
            var model = new IndexModel(this.ServiceManager, tag);
            model.Init();

            return View(model);
        }

        public ActionResult Detail(int id)
        {
            var model = new DetailModel(this.ServiceManager, id);
            model.Init();

            return View(model);
        }

        [HttpPost]
        public ActionResult Detail_AddComment(DiaryCommentEntity comment)
        {
            var model = new DetailModel(this.ServiceManager, comment.DiaryId);
            model.AddComment(this.Request, comment);

            return this.Redirect(this.Url.Action("Detail", "Diary") + string.Format("?id={0}", comment.DiaryId));
        }

        [HttpPost]
        public ActionResult Detail_StarUp(int id)
        {
            var target = this.ServiceManager.DiaryService.GetEntityById(id);
            if (target.StarUp != null)
            {
                target.StarUp++;
            }
            else
            {
                target.StarUp = 1;
            }

            this.ServiceManager.DiaryService.Update(target);

            return this.Json("OK");
        }

        [AdminAuthorize]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Add(DiaryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CreateTime == null)
                {
                    model.CreateTime = DateTime.Today;
                }

                var normFiles = model.Files != null
                    ? model.Files.Where(f => f != null).ToArray()
                    : new HttpPostedFileBase[0];

                if (normFiles.Length > 3)
                {
                    ModelState.AddModelError("", "图片不能超过3张");
                }
                else
                {
                    if (normFiles.Any())
                    {
                        foreach (HttpPostedFileBase file in normFiles)
                        {
                            string path = System.IO.Path.Combine(Server.MapPath("~/Images/diary"), file.FileName);
                            file.SaveAs(path);
                        }

                        List<string> oldImgs =
                            (model.Images ?? "").Split(';')
                                .Where(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s)).ToList();

                        oldImgs.AddRange(normFiles.Select(file => file.FileName));
                        model.Images = string.Join(";", oldImgs.Distinct());
                    }

                    var entity = new DiaryEntity();
                    model.CopyTo(entity);
                    this.ServiceManager.DiaryService.Insert(entity);

                    return this.RedirectToAction("Index", "Diary");
                }
            }

            return this.View();
        }

        [AdminAuthorize]
        public ActionResult Edit(int id)
        {
            var entity = this.ServiceManager.DiaryService.GetEntityById(id);
            if (entity != null)
            {
                return this.View(entity.ToModel());
            }
            else
            {
                return this.RedirectToAction("Add");
            }
        }

        [HttpPost]
        [AdminAuthorize]
        public ActionResult Edit(DiaryModel model)
        {
            if (ModelState.IsValid)
            {
                var target = this.ServiceManager.DiaryService.GetEntityById(model.Id);

                if (model.Removed)
                {
                    this.ServiceManager.DiaryService.Delete(target);
                    return this.RedirectToAction("Index", "Diary");
                }

                if (model.CreateTime == null)
                {
                    model.CreateTime = DateTime.Today;
                }

                var normFiles = model.Files != null
                    ? model.Files.Where(f => f != null).ToArray()
                    : new HttpPostedFileBase[0];

                if (normFiles.Length > 3)
                {
                    ModelState.AddModelError("", "图片不能超过3张");
                }
                else
                {
                    if (normFiles.Any())
                    {
                        foreach (HttpPostedFileBase file in normFiles)
                        {
                            string path = System.IO.Path.Combine(Server.MapPath("~/Images/diary"), file.FileName);
                            file.SaveAs(path);
                        }

                        List<string> oldImgs =
                            (model.Images ?? "").Split(';')
                                .Where(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s)).ToList();

                        oldImgs.AddRange(normFiles.Select(file => file.FileName));

                        model.Images = string.Join(";", oldImgs.Distinct());
                    }
                }

                model.CopyTo(target);
                this.ServiceManager.DiaryService.Update(target);
                return this.RedirectToAction("Index", "Diary");
            }

            return this.View();
        }
    }
}
