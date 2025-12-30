namespace APIRickandMortyProject.Models
{

    public class EpisodeInfo
    {
        public int count { get; set; }
        public int pages { get; set; }
        public string next { get; set; }
        public object prev { get; set; }
    }

    public class Episode
    {
        public int id { get; set; }
        public string name { get; set; }
        public string air_date { get; set; }
        public string episode { get; set; }
        public List<string> characters { get; set; }
        public string url { get; set; }
        public DateTime created { get; set; }

        public List<Result> charDetails { get; set; }   
    }

    public class EpisodeRoot
    {
        public EpisodeInfo info { get; set; }
        public List<Episode> results { get; set; }
    }
}
