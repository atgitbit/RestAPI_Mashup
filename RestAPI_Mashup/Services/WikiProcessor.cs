using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestAPI_Mashup.Services
{
    public class WikiProcessor
    {
        public async Task<string> GetWikiBandName(string bandID)
        {
            var bandTitle = "";
            string url = $"https://www.wikidata.org/w/api.php?action=wbgetentities&format=json&props=sitelinks&ids={bandID}&sitefilter=enwiki";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var wikiResponse = await response.Content.ReadAsStringAsync();
                   
                    var jo = (JObject)JsonConvert.DeserializeObject(wikiResponse);
                    var title = jo.SelectToken($"entities.{bandID}.sitelinks.enwiki.title").Value<string>();
                    bandTitle = title.Remove(title.Length - 6);  //får tillbaka Nirvana(band)
                }
                else
                {
                    bandTitle = "Could not find a band/artist title";
                }
            }
            return bandTitle;
        }
    }
}
