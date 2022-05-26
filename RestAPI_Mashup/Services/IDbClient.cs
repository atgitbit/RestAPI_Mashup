using MongoDB.Driver;
using RestAPI_Mashup.Models;

namespace RestAPI_Mashup.Services
{
    public interface IDbClient
    {
        //Interface för mongodb
        IMongoCollection<Artist> GetArtistsCollection();
    }
}
