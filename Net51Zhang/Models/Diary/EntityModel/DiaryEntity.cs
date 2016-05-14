using System;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Models.Diary.EntityModel
{
    public class DiaryEntity : BaseEntity
    {
        public string Title { get; set; }

        /// <summary>
        /// store image name, split with ;
        /// </summary>
        public string Images { get; set; }

        public DateTime CreateTime { get; set; }

        public string Content { get; set; }

        public int? StarUp { get; set; }

        public int? StarDown { get; set; }

        public string Tag { get; set; }

        public string Video { get; set; }

        public DiaryModel ToModel()
        {
            var model = new DiaryModel()
            {
                Id = this.Id,
                Title = this.Title,
                Images = this.Images,
                CreateTime = this.CreateTime,
                Content = this.Content,
                Tag = this.Tag,
                StarUp = this.StarUp,
                StarDown = this.StarDown,
                Video = this.Video
            };

            return model;
        }
    }
}