namespace RestAPI_Mashup.Models
{
    public class UpdateArtistRequest
    {
        // för att uppdatera artisten i databasen, id på artisten kommer som inparameter i metoden 
        public string mbid { get; set; }
        public string description { get; set; }
        public List<Album> albums { get; set; } = new List<Album>();
        //first commit
    }
}
