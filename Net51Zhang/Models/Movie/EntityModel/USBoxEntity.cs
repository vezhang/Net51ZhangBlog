using Newtonsoft.Json.Linq;

namespace Net51Zhang.Models.Movie.EntityModel
{
    public class USBoxEntity
    {
        public int Rank { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }
        public string Box { get; set; }

        public static USBoxEntity Parse(JObject jObject)
        {
            USBoxEntity entity = new USBoxEntity();
            entity.Rank = (int) jObject["rank"];
            entity.Title = (string) jObject["subject"]["title"];
            entity.Id = (string) jObject["subject"]["id"];
            entity.Box = jObject["box"].ToString();

            return entity;
        }
    }
}