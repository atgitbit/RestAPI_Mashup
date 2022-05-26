using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RestAPI_Mashup.Models;

namespace RestAPI_Mashup.Services
{
    public class DbClient : IDbClient
    {
        // Mongo databas för att hämta det som finns lagrat (finns ej lagrat något än)
        private readonly IMongoCollection<Artist> _artists;
        public DbClient(IOptions<ArtistDBconfig> artistDBconfig)
        {
            var client = new MongoClient(artistDBconfig.Value.Connection_String);
            var database = client.GetDatabase(artistDBconfig.Value.Database_Name);
            _artists = database.GetCollection<Artist>(artistDBconfig.Value.Artist_Collection_Name);
        }
        public IMongoCollection<Artist> GetArtistsCollection()
        {
            return _artists;
        }
    }
}
