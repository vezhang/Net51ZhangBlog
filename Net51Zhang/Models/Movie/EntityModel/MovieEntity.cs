using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Net51Zhang.Models.Movie.EntityModel
{
    public class MovieEntity
    {
        public string Id { get; set; }
        public string Discription { get; set; }
        public List<Tuple<string, string>> Directors { get; set; }
        public List<Tuple<string, string>> Actors { get; set; }
        public List<string> Genres { get; set; } 
        public string ReleaseYear { get; set; }
        public List<string> Locations { get; set; }
        
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string SourceUrl { get; set; }
        public float Score { get; set; }

        public static MovieEntity Parse(JObject jObject)
        {
            var movie = SnapParse(jObject);

            movie.Discription = (string) jObject["summary"];
            movie.Directors = ((JArray) jObject["directors"] ?? new JArray()).Select(ParsePeople).ToList();
            movie.Actors = ((JArray)jObject["casts"] ?? new JArray()).Select(ParsePeople).ToList();
            movie.Genres = ((JArray) jObject["genres"] ?? new JArray()).Select(t => (string) t).ToList();
            movie.Locations = ((JArray)jObject["countries"] ?? new JArray()).Select(t => (string)t).ToList();
            movie.ReleaseYear = (string) jObject["year"];
            
            return movie;
        }

        public static MovieEntity SnapParse(JObject jObject)
        {
            MovieEntity movie = TitleIdParse(jObject);

            movie.ImageUrl = (string)((JObject)jObject["images"] ?? new JObject())["large"];
            movie.Score = (float) ((JObject) jObject["rating"] ?? new JObject())["average"];
            movie.SourceUrl = (string) jObject["alt"];

            return movie;
        }

        public static MovieEntity TitleIdParse(JObject jObject)
        {
            MovieEntity movie = new MovieEntity();
            movie.Id = (string)jObject["id"];
            movie.Title = (string)jObject["title"];

            return movie;
        }

        private static Tuple<string, string> ParsePeople(JToken jObject)
        {
            string name = (string) jObject["name"];
            string url = (string) jObject["alt"];

            return new Tuple<string, string>(name, url);
        }
    }
}