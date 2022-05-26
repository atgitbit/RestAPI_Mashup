using Newtonsoft.Json;

namespace RestAPI_Mashup.Services
{
    public class CoverArtProcessor
    {
        public async Task<string> LoadAlbumCoverAsync(string coverId)
        {
            string img = "";
            string url = $"http://coverartarchive.org/release-group/{coverId}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var imgResponse = await response.Content.ReadAsStringAsync();
                    dynamic respJson = JsonConvert.DeserializeObject(imgResponse);
                    img = respJson["images"][0]["image"];
                }
                else
                {
                    img = "Image not found";
                }
            }
            return img;
        }
    }
}
