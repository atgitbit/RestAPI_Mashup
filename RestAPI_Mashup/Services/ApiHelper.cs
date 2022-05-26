namespace RestAPI_Mashup.Services
{
        public static class ApiHelper
        {
            // statisk för vi vill bara använda denna en per applikation
            public static HttpClient ApiClient { get; set; }
            public static void InitializeClient()
            {
                ApiClient = new HttpClient();
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                ApiClient.DefaultRequestHeaders.Add("user-agent", "MB-APITagger/1.1 (memail@example.com)"); //istället för "api-nyckel" en user agent
            }
        }
    
}
