using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestAPI_Mashup.Models;

namespace RestAPI_Mashup.Services
{
    public class MBrainzProcessor
    {
        public async Task<Artist> LoadArtistAsync(string mbid)
        {
            string url = "";
            if (mbid != null)
            {
                url = $"https://musicbrainz.org/ws/2/artist/{mbid}?&fmt=json&inc=url-rels+release-groups";
            }
            else
            {
                url = "https://musicbrainz.org/ws/2/artist/?&fmt=json&inc=url-rels+release-groups";
            }
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Artist artistDetails = new Artist();
                    var data = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    var albums = GetAlbumsAsync(json);
                    var bandName = GetBandNameAsync(json);
                    var respBandName = await bandName;
                    var wikipediaProcessor = new WikipediaProcessor();
                    var getBandDescription = wikipediaProcessor.LoadWikipediaDescriptionAsync(respBandName);
                    artistDetails.mbid = mbid;
                    artistDetails.albums = await albums;
                    artistDetails.description = await getBandDescription;

                    return artistDetails;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<List<Album>> GetAlbumsAsync(JToken json)
        {
            // hämta en lista på alla album: title, id, image
            List<Task<Album>> albumTasks = new List<Task<Album>>();
            foreach (var releaseToken in json["release-groups"])
            {
                albumTasks.Add(GetAlbumAsync(releaseToken));
            }
            var results = await Task.WhenAll(albumTasks);
            return new List<Album>(results);
        }
        public async Task<Album> GetAlbumAsync(dynamic releaseToken)
        {
            //hämta enskilt album och id för att hitta bild genom Coverart
            var caProcessor = new CoverArtProcessor();
            var album = new Album();
            var albumMID = releaseToken["id"].ToString();
            album.id = albumMID;
            album.title = releaseToken["title"].ToString();
            album.image = await caProcessor.LoadAlbumCoverAsync(albumMID);
            return album;
        }
        public async Task<string> GetBandNameAsync(JToken json)
        {
            string bandName = "";
            foreach (var relation in json["relations"])
            {
                if (relation["type"].ToString() == "wikipedia") //OM typ wikipedia => hämta från wikipedia api
                {
                    var wikiUrl = relation["url"]["resource"];
                    var urlConvert = JsonConvert.SerializeObject(wikiUrl);
                    bandName = urlConvert.Substring(31);
                    bandName = bandName.Remove(bandName.Length - 7);
                }
                if (relation["type"].ToString() == "wikidata") //OM typ wikidata => anropa wikidata för att hitta title - sök på wikipedia
                {
                    var wikiUrl = relation["url"]["resource"];
                    var urlConvert = JsonConvert.SerializeObject(wikiUrl);
                    bandName = urlConvert.Substring(31); // id kommer efter char 30
                    bandName = bandName.Remove(bandName.Length - 1); // https://www.wikidata.org/wiki/Q11649
                    var wikiProcessor = new WikiProcessor();
                    bandName = await wikiProcessor.GetWikiBandName(bandName);
                }
            }
            return bandName;
        }
     
    }
}
