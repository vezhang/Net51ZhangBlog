using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common;
using Net51Zhang.Models.Diary.EntityModel;

namespace Net51Zhang.Models.Diary
{
    public class IndexModel : PageModel
    {
        public List<DiaryModel> DiaryEntities { get; set; }
        public string Tag { get; set; }
 
        public IndexModel(IServiceManager serviceManager, string tag) : base(serviceManager)
        {
            this.Tag = tag;
            this.DiaryEntities = new List<DiaryModel>();
        }

        public void Init()
        {
            this.DiaryEntities =
                this.ServiceManager.DiaryService.GetAll()
                    .Where(
                        e =>
                            string.IsNullOrEmpty(this.Tag) || this.Tag.Equals(e.Tag, StringComparison.OrdinalIgnoreCase))
                    .Select(e => e.ToModel())
                    .ToList();

            this.DiaryEntities.Sort((left, right) =>
            {
                return right.CreateTime.Value.CompareTo(left.CreateTime.Value);
            });
        }

        public string TrimContent(string input)
        {
            if(input.Length < 100)
                return input;
            else
            {
                return  input.Substring(0, 100) + "......";
            }
        }
    }
}