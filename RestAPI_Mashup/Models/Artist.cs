namespace RestAPI_Mashup.Models
{
    public class Artist
    {
        public Guid Id { get; set; }
        public string mbid { get; set; }
        public string description { get; set; }
        public List<Album> albums { get; set; } = new List<Album>();
    }
}
