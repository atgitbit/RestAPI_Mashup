namespace RestAPI_Mashup.Models
{
    public class AddArtistRequest
    {
        //för att kunna lägga in egna artister i 
        public string mbid { get; set; }
        public string description { get; set; }
        public List<Album> albums { get; set; } = new List<Album>();
    }
}
