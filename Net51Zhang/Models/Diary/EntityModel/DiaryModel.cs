using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Net51Zhang.Models.Diary.EntityModel
{
    public class DiaryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title Required")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public string Images { get; set; }

        public HttpPostedFileBase[] Files { get; set; }

        public bool Removed { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? StarUp { get; set; }

        public int? StarDown { get; set; }

        public string Video { get; set; }

        [Required(ErrorMessage = "Content Required")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Tag Required")]
        [Display(Name = "Tag")]
        public string Tag { get; set; }

        public void CopyTo(DiaryEntity entity)
        {
            entity.Title = this.Title;
            entity.Content = this.Content;
            entity.Tag = this.Tag;
            entity.CreateTime = this.CreateTime == null ? DateTime.Today : this.CreateTime.Value;
            entity.Images = this.Images;
            entity.StarDown = this.StarDown;
            entity.StarUp = this.StarUp;
            entity.Video = this.Video;
        }
    }
}