using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace RestAPI_Mashup.Services
{
    public class WikipediaProcessor
    {
        public async Task<string> LoadWikipediaDescriptionAsync(string bandName)
        {
            string wikipediaDescription = "";
            string url = $"https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&redirects=true&titles={bandName}";
            //https://en.wikipedia.org/wiki/{bandName}    
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var wikiResponse = await response.Content.ReadAsStringAsync();
                    dynamic jObject = (JObject)JsonConvert.DeserializeObject(wikiResponse); // använder JToken då jag är osäker på json formatet (abstrakt klass)

                    var jResp = jObject.SelectToken($"query.pages");
                    foreach (JToken page in jResp.Children())
                    {
                        foreach (JToken info in page)
                        {
                            wikipediaDescription = info["extract"].ToString();
                        }
                    }
                }
                else
                {
                    wikipediaDescription = "Could not find any description";
                }
            }
            return wikipediaDescription;
        }

    }
}
